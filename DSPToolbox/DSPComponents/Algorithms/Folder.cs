using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            List<float> temp = new List<float>();
            //temp = InputSignal.Samples;
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {

                temp.Add(InputSignal.Samples[InputSignal.Samples.Count - i - 1]);
            }
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {

                InputSignal.Samples[i] = temp[i];
            }

            OutputFoldedSignal = InputSignal;
        }
    }
}
