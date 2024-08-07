using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }
        List<float> multiplied_values = new List<float>();

        public override void Run()
        {
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float multiplied_ans = InputSignal.Samples[i] * InputConstant;
                multiplied_values.Add(multiplied_ans);
            }
            Signal s = new Signal(multiplied_values, false);
            OutputMultipliedSignal = s;
        }
    }
}
