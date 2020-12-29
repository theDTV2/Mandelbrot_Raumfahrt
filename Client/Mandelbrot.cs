using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;

namespace Client
{
    class Mandelbrot
    {
        public static byte[] GenerateMandelbrotSet( float complexity, int iterations)  
        {
            int sizex = 2;
            int sizey = 2;
            double cj;
            double ci;
            Complex CurrComplex;
            Complex[] Mandel;

            List<Complex> calcoutput = new List<Complex>();
            var StartTime = DateTime.Now;

            for (int i = 0; i < complexity; i++)
            {
                ci = (sizex * (i / complexity)) - sizex;

                for (int j = 0; j < complexity; j++)
                {
                    cj = (sizex * (j / complexity)) - sizey;

                    CurrComplex = new Complex(ci, cj);

                    Mandel = new Complex[iterations];
                    Mandel[0] = 0;
                    for (int m = 1; m < iterations; m++)
                    {
                        Mandel[m] = (Mandel[m - 1] * Mandel[m - 1]) + CurrComplex;
                    }

                        calcoutput.Add(Mandel[iterations - 1]);

                }
            }

            var EndTime = DateTime.Now;
            System.Console.WriteLine("");
            System.Console.WriteLine("Iterations: " + complexity + " Complexity " + iterations + " Time elapsed: " + EndTime.Subtract(StartTime));
            System.Console.WriteLine("Hash: " + String.Join(", ", calcoutput).GetSHA384HashAsString());



            return String.Join(", ", calcoutput).GetSHA384Hash();
        }


    }
}
