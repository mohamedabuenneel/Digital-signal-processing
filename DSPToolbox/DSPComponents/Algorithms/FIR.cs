using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float InputF1 { get; set; }
        public float InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }
        public Signal F_Domain;
        public List<float> h_d = new List<float>();
        public List<float> w_n = new List<float>();
        public List<float> h_n = new List<float>();
        public List<int> indxs = new List<int>();
        double N;
        public override void Run()
        {
            DiscreteFourierTransform dft = new DiscreteFourierTransform();
            dft.InputSamplingFrequency = InputFS;
            dft.InputTimeDomainSignal = InputTimeDomainSignal;
            dft.Run();
            F_Domain = dft.OutputFreqDomainSignal;
            
            w_n = window_method();//check data type(casting)
            int indx = (int)(N - 1) / 2;


            if (InputFilterType == FILTER_TYPES.LOW)
            {
                float fc = ((InputTransitionBand / 2) + (float)InputCutOffFrequency) / InputFS;
                {

                    for (int i = -indx; i <= indx; i++)
                    {
                        if (i == 0)
                            h_d.Add(2 * fc);
                        else
                        {
                            double temp = 2 * fc * (Math.Sin(i * 2 * Math.PI * fc) / (i * 2 * Math.PI * fc));
                            h_d.Add((float)temp);
                        }
                    }
                    for (int i = 0; i < N; i++)
                    {

                        h_n.Add(h_d[i] * w_n[i]);
                    }
                    for (int i = -indx; i <= indx; i++)
                    {
                        indxs.Add(i);
                    }


                }
                OutputHn = new Signal(h_n, indxs, false);
            }


            else if (InputFilterType == FILTER_TYPES.HIGH)
            {
                float fc = ((InputTransitionBand / 2) + (float)InputCutOffFrequency) / InputFS;
                for (int i = -indx; i <= indx; i++)
                {
                    if (i == 0)
                        h_d.Add(1 - (2 * fc));
                    else
                    {
                        double temp = -2 * fc * (Math.Sin(i * 2 * Math.PI * fc) / (i * 2 * Math.PI * fc));
                        h_d.Add((float)temp);
                    }
                }
                for (int i = 0; i < N; i++)
                {
                    h_n.Add(h_d[i] * w_n[i]);
                }
                for (int i = -indx; i <= indx; i++)
                {
                    indxs.Add(i);
                }
            OutputHn = new Signal(h_n, indxs, false);
            }
            else if(InputFilterType == FILTER_TYPES.BAND_PASS)
            {
                float f1= (-(InputTransitionBand / 2) + (float)InputF1) / InputFS;
                float f2 = ((InputTransitionBand / 2) + (float)InputF2) / InputFS;
                double temp1;
                
                for(int i=-indx;i<=indx;i++)
                {
                    temp1 = ((2 * f2) * (Math.Sin(i * 2 * Math.PI * f2) / (i * 2 * Math.PI * f2))) - ((2 * f1) * (Math.Sin(i * 2 * Math.PI * f1) / (i * 2 * Math.PI * f1)));
                    
                    if (i == 0)
                        h_d.Add(2 * (f2 - f1));
                    else
                    {
                        h_d.Add((float)temp1);
                    }
                }
                for (int i = 0; i < N; i++)
                {
                    h_n.Add(h_d[i] * w_n[i]);
                }
                for (int i = -indx; i <= indx; i++)
                {
                    indxs.Add(i);
                }
                OutputHn = new Signal(h_n, indxs, false);

            }
            else if (InputFilterType == FILTER_TYPES.BAND_STOP)
            {
                float f1 = (-(InputTransitionBand / 2) + (float)InputF1) / InputFS;
                float f2 = ((InputTransitionBand / 2) + (float)InputF2) / InputFS;
                double temp1;

                for (int i = -indx; i <= indx; i++)
                {
                    temp1 = ((2 * f1) * (Math.Sin(i * 2 * Math.PI * f1) / (i * 2 * Math.PI * f1))) - ((2 * f2) * (Math.Sin(i * 2 * Math.PI * f2) / (i * 2 * Math.PI * f2)));

                    if (i == 0)
                        h_d.Add(1-(2 * (f2 - f1)));
                    else
                    {
                        h_d.Add((float)temp1);
                    }
                }
                for (int i = 0; i < N; i++)
                {
                    h_n.Add(h_d[i] * w_n[i]);
                }
                for (int i = -indx; i <= indx; i++)
                {
                    indxs.Add(i);
                }
                OutputHn = new Signal(h_n, indxs, false);

            }
            DirectConvolution conv = new DirectConvolution();
            conv.InputSignal1 = OutputHn;
            conv.InputSignal2 = InputTimeDomainSignal;
            conv.Run();
            OutputYn = conv.OutputConvolvedSignal;


        }
                
        
        
        List<float>window_method()
        {
            List<float> w = new List<float>();
            int window_indx = 0;
            float wn;
            float delta_f = InputTransitionBand / InputFS;
            if (InputStopBandAttenuation <= 21)
            {
                window_indx = 1;
                N = Math.Ceiling((double)(0.9 * InputFS) / InputTransitionBand);
                if (N % 2 == 0)
                    N++;
            }
            else if (InputStopBandAttenuation <= 44)
            {
                window_indx = 2;
                N = Math.Ceiling((double)((3.1 * InputFS) / InputTransitionBand));
                if (N % 2 == 0)
                    N++;
            }
            else if (InputStopBandAttenuation <= 53)
            {
                window_indx = 3;
                N = Math.Ceiling((double)((3.3 * InputFS) / InputTransitionBand));
                if (N % 2 == 0)
                    N++;
            }
            else if (InputStopBandAttenuation <= 74)
            {
                window_indx = 4;
                N = Math.Ceiling(((5.5 * InputFS) / InputTransitionBand));
                if (N % 2 == 0)
                    N++;
            }
            int indx = (int)(N-1) / 2;
            for(int i=-indx;i<=indx;i++)
            {
                wn = which_window(i, window_indx);
                w.Add(wn);
            }

            return w;
        }

        float which_window(int i,int window)
        {
            float ans=0;
            if(window==1)
            {
                ans = 1;
            }
            else if(window==2)
            {
                ans= (float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N));
            }
            else if(window==3)
            {
                ans=(float)(0.54 + 0.46 * Math.Cos((2 * Math.PI * i) / N));
            }
            else if(window==4)
            {
                ans=(float) (0.42 + 0.5 * Math.Cos((2 * Math.PI * i) / (N - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N - 1)));
            }
            return ans;
        }

    }
}
