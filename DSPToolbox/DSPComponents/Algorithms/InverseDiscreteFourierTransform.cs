using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
using System.IO;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            List<float> output = new List<float>();
            List<Complex> complex_form = new List<Complex>();
            Complex polar_to_complex = new Complex();
            int N = InputFreqDomainSignal.FrequenciesAmplitudes.Count;
            for (int i = 0; i < N; i++)
            {
                polar_to_complex = Complex.FromPolarCoordinates(InputFreqDomainSignal.FrequenciesAmplitudes[i], InputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                complex_form.Add(polar_to_complex);
            }

            for (int i = 0; i < N; i++)
            {

                Complex sum_of_real = new Complex();
                for (int j = 0; j < N; j++)
                {
                    double power = ((j * 2 * Math.PI * i) / N);
                    sum_of_real += (complex_form[j] * new Complex(Math.Cos(Math.Abs(power)), Math.Sin(Math.Abs(power))));
                }
                output.Add((float)(sum_of_real.Real / N));

            }
            OutputTimeDomainSignal = new Signal(output, false);
            //File.WriteAllText(@"C:\Desktop\csc.txt",InputFreqDomainSignal.Samples.Count.ToString());

        }
    }
}