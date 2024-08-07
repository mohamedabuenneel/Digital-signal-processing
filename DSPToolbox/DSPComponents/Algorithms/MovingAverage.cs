using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
        List<float> Avgx = new List<float>();
        public override void Run()
        {
            int x = InputWindowSize;
            for (int i = 0; i < InputSignal.Samples.Count - x + 1; i++)
            {
                float temp = 0;
                for (int j = i; j < i + x; j++)
                {
                    temp += InputSignal.Samples[j];
                }
                Avgx.Add(temp / InputWindowSize);
            }

            Signal S = new Signal(Avgx, false);
            OutputAverageSignal = S;
        }

    }
}
