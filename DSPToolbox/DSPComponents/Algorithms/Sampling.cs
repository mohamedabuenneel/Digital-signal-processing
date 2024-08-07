using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor 1 0 0 2 0 0 3 0 0 4 0  5
        public int M { get; set; } //downsampling factor 1 2 3 4 5
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public List<float> sampled_signal=new List<float>();
        public List<float> out_put = new List<float>();
        public List<float> out_put2 = new List<float>();
        Signal hn;
        Signal yn;
   

   
       
        public override void Run()
        {
            FIR fir = new FIR();
            fir.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            fir.InputFS = 8000;
            fir.InputStopBandAttenuation = 50;
            fir.InputCutOffFrequency = 1500;
            fir.InputTransitionBand = 500;
            
          
            if (L == 0 && M == 0)
            {
                //error
            }
            if (L != 0 && M == 0)
            {

                for (int i = 0; i < InputSignal.Samples.Count; i ++)
                {
                    out_put.Add(InputSignal.Samples[i]);
                    for(int j=0;j<L-1;j++)
                    {
                        out_put.Add(0);
                    }

                }
                    fir.InputTimeDomainSignal = new Signal(out_put,false);
                    fir.Run();
                    OutputSignal = new Signal(fir.OutputYn.Samples, false);
            }
            if (L == 0 && M != 0)
            {
                fir.InputTimeDomainSignal = InputSignal;
                fir.Run();
                yn = fir.OutputYn;
                for(int i=0;i<yn.Samples.Count;i+=M)
                {
                    out_put2.Add(yn.Samples[i]);                   
                }
                OutputSignal = new Signal(out_put2, false);


            }
            if (L != 0 && M != 0)
            {
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    out_put.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L-1 ; j++)
                    {
                        out_put.Add(0);
                    }
                }
                 fir.InputTimeDomainSignal = new Signal(out_put, false);                
                fir.Run();
                yn = fir.OutputYn;
                for (int i = 0; i < yn.Samples.Count; i += M)
                {
                    out_put2.Add(yn.Samples[i]);
                }

                OutputSignal = new Signal(out_put2,false);
            }

        }

    }

}
