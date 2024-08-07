using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        List<float> answers = new List<float>();
        public override void Run()
        {
            
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float ans = 0;
                float au0 = 1 / (float)Math.Sqrt(InputSignal.Samples.Count);
                float au1 = (float)Math.Sqrt(2) / (float)Math.Sqrt(InputSignal.Samples.Count);
               for (int j = 0; j < InputSignal.Samples.Count; j++)
               {
                    ans += (InputSignal.Samples[j]) * (float)Math.Cos((2 * j + 1) * i * Math.PI / (2 * InputSignal.Samples.Count));
                    
               }
                if (i == 0)
                {
                    ans *= au0;
                }
                else
                {
                    ans *= au1;
                }

                answers.Add((float)ans);
            }
                OutputSignal = new Signal(answers, false);
        }
    }
}
