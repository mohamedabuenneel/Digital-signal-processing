using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }

        public override void Run()
        {
            int N = InputSignal1.Samples.Count;
            float max = int.MinValue;
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
                    if (multiplied_value > max)
                        max = multiplied_value;
                }

            }
            OutputTimeDelay = max * InputSamplingPeriod;//what is J?need test cases :(
        }
    }
}
