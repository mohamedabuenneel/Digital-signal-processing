using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        List<float> acc = new List<float>();

        public override void Run()
        {
            for (int i = 1; i < InputSignal.Samples.Count + 1; i++)
            {
                float temp = 0;
                for (int j = 0; j < i; j++)
                {
                    temp += InputSignal.Samples[j];
                }
                acc.Add(temp);
            }
            Signal S = new Signal(acc, false);
            OutputSignal = S;
        }
    }
}
