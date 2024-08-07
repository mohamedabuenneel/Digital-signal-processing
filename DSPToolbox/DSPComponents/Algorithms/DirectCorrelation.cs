using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }
        List<float> corr_out = new List<float>();
        List<float> Ncorr_out = new List<float>();
        public override void Run()
        {
            int N = InputSignal1.Samples.Count;
            float den = 0;
            for (int i = 0; i < N; i++)
            {
                float temp = InputSignal1.Samples[i];
                den += (float)Math.Pow(temp, 2);
            }
            den = den * den;
            double z = Math.Sqrt(den);
            z = z / N;

            for (int i = 0; i < N; i++)
            {
                float multiplied_value = 0;
                for (int j = 0; j < N; j++)
                {
                    if (InputSignal1.Periodic == true)
                    {
                        float temp1 = InputSignal1.Samples[j];
                        float temp2 = InputSignal1.Samples[(i + j) % N];
                        multiplied_value += temp1 * temp2;

                    }
                    else
                    {
                        if (i + j < N)
                        {
                            float temp1 = InputSignal1.Samples[j];
                            float temp2 = InputSignal1.Samples[(i + j)];
                            multiplied_value += temp1 * temp2;
                        }
                        else
                            break;
                    }
                }


                corr_out.Add(multiplied_value / N);
                Ncorr_out.Add((multiplied_value / N) / (float)z);
            }
            OutputNonNormalizedCorrelation = corr_out;
            OutputNormalizedCorrelation = Ncorr_out;
        }
    }
}