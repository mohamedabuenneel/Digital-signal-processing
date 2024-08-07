using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }
        public override void Run()
        {
            List<float> list_of_amp = new List<float>();
            List<float> list_of_phase_shift = new List<float>();
            List<float> List_of_freq = new List<float>();
            //double Amplitude;
            //double phase_shift;
            int N = InputTimeDomainSignal.Samples.Count;
            for (int i = 0; i < N; i++)
            {
                Complex X_of_k = new Complex(0, 0);
                //X_of_k = 0;
                for (int j = 0; j < N; j++)
                {
                    Complex C = new Complex(0, (Math.Sin((2 * Math.PI / N) * i * j)));
                    X_of_k += InputTimeDomainSignal.Samples[j] * ((Math.Cos(2 * Math.PI / N * i * j)) - C);
                }

                //Amplitude = Math.Sqrt(Math.Pow(X_of_k.Real, 2) + Math.Pow(X_of_k.Imaginary, 2));
                //phase_shift = Math.Atan(X_of_k.Imaginary / X_of_k.Real);
                list_of_amp.Add((float)X_of_k.Magnitude);
                list_of_phase_shift.Add((float)X_of_k.Phase);
            }
            //double freq = 0;
            //for (int i = 0; i < N; i++)
            //{
            //    freq += (2 * Math.PI * InputSamplingFrequency / N);
            //    List_of_freq.Add((float)freq);
            //}
            OutputFreqDomainSignal = new Signal(false,null, list_of_amp, list_of_phase_shift);

        }
    }

}

