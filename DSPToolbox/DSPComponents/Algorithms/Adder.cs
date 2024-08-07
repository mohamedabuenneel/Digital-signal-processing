using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> new_out = new List<float>();
            for (int i = 0; i < InputSignals[0].Samples.Count; i++)
            {
                float sum_of_signals = 0;
                for (int j = 0; j < InputSignals.Count; j++)
                {
                    sum_of_signals += InputSignals[j].Samples[i];
                }
                new_out.Add(sum_of_signals);
            }
            Signal s = new Signal(new_out, false);

            OutputSignal = s;
        }
    }
}