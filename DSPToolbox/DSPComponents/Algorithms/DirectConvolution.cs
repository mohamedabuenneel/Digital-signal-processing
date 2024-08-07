using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }
        List<float> sum = new List<float>();
        List<int> indxs = new List<int>();
        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            int N1 = InputSignal1.Samples.Count;
            int N2 = InputSignal2.Samples.Count;
            int min1 = InputSignal1.SamplesIndices.Min();
            int min2 = InputSignal2.SamplesIndices.Min();
            int max1= InputSignal1.SamplesIndices.Max();
            int max2 = InputSignal2.SamplesIndices.Max();
            int min=min1+min2;
            int max = max1 + max2;
            int id = -1;
            
            for(int i=min;i<=max;i++)
            {
                float temp = 0;
                for (int  k= 0; k < N1; k++)
                {
                    for (int j = 0; j < N2; j++)
                    {
                        if (InputSignal1.SamplesIndices[k] + InputSignal2.SamplesIndices[j]== i)
                        {
                            id++;
                            temp += (InputSignal1.Samples[k] * InputSignal2.Samples[j]);
                            break;
                        }

                    }

                }
                if (id >= 0)
                {
                    indxs.Add(i);
                    sum.Add(temp);
                }
              
            }
            if (sum[indxs.Count - 1]==0)
            {
                indxs.RemoveAt(indxs.Count()- 1);
                sum.RemoveAt(sum.Count - 1);
            }
            Signal s = new Signal(sum,indxs, false);
            OutputConvolvedSignal = s;
        }
    }
}
