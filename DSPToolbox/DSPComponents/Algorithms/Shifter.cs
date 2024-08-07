using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
            //Signal S = new Signal();
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                if (ShiftingValue > 0)
                    InputSignal.SamplesIndices[i] -= ShiftingValue;
                else
                    InputSignal.SamplesIndices[i] += -1 * ShiftingValue;
            }
            OutputShiftedSignal = InputSignal;
        }
    }
}
