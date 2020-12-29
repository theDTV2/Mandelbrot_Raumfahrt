using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;


namespace Client
{
    class Mandelbrot
    {
        public static void GenerateMandelbrotSet( float complexity, int iterations)  
        {
            int sizex = 2;
            int sizey = 2;

            Complex[] test = new Complex[90000];
            int p = 0;

            var StartTime = DateTime.Now;

            for (int i = 0; i < complexity; i++)
            {
                double ci = (sizex * (i / complexity)) - sizex;

                for (int j = 0; j < complexity; j++)
                {
                    double cj = (sizex * (j / complexity)) - sizey;

                    Complex CurrComplex = new Complex(ci, cj);

                    Complex[] Mandel = new Complex[iterations];
                    Mandel[0] = 0;
                    for (int m = 1; m < iterations; m++)
                    {
                        Mandel[m] = (Mandel[m - 1] * Mandel[m - 1]) + CurrComplex;
                    }
                    if (!Complex.IsNaN(Mandel[iterations - 1]))
                        {
                        test[p] = Mandel[iterations - 1];
                        p++;
                    }

                }
            }



            var EndTime = DateTime.Now;
            
        }


    }
}
