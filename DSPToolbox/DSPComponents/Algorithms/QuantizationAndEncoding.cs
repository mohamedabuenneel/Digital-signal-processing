using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }
        //from the input (il or inb get the other)
        //get the delta from (amp_max_amp_min)/levels
        //add levels by calc their midpoints
        //loop for each x(n) and find the nearest midpoint to it by finding the diff between it and each midpoint
        //
        public override void Run()
        {

            if (InputLevel <= 0)
                InputLevel = (int)Math.Pow(2, InputNumBits);
            if (InputNumBits <= 0)
            {
                double temp = Math.Ceiling(Math.Log(InputLevel, 2));
                InputNumBits = (int)temp;
            }

            float delta = (InputSignal.Samples.Max() - InputSignal.Samples.Min()) / InputLevel;
            List<float> My_OutputSamplesError = new List<float>();
            List<int> My_OutputIntervalIndices = new List<int>();
            List<string> My_OutputEncodedSignal = new List<string>();
            List<float> levels = new List<float>();
            List<float> quantized = new List<float>();

            for (float lvl = InputSignal.Samples.Min(); lvl < InputSignal.Samples.Max(); lvl += delta)
            {
                float midpoint = (lvl + (lvl + delta)) / 2;
                levels.Add(midpoint);
            }
            //midpoint are clear
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                //ngeb l errors
                float error = float.MaxValue;
                int level_index = 0;
                for (int idx = 0; idx < InputLevel; idx++)
                {
                    if (error > Math.Abs(levels[idx] - InputSignal.Samples[i]))
                    {
                        error = Math.Abs(levels[idx] - InputSignal.Samples[i]);
                        level_index = idx;
                    }

                }
                //indexing done
                quantized.Add(levels[level_index]);//adding midpoints to xq(column)
                My_OutputSamplesError.Add(levels[level_index] - InputSignal.Samples[i]);//sample error
                My_OutputIntervalIndices.Add(level_index + 1);//indices 1 based
                My_OutputEncodedSignal.Add(Convert.ToString(level_index, 2).PadLeft(InputNumBits, '0'));//encoding
            }
            OutputQuantizedSignal = new Signal(quantized, false);
            OutputEncodedSignal = My_OutputEncodedSignal;
            OutputIntervalIndices = My_OutputIntervalIndices;
            OutputSamplesError = My_OutputSamplesError;
        }
    }
}