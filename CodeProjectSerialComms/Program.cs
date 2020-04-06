using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CodeProjectSerialComms
{
    static class Program
    {
        // set true if you wish to run live reading from the sensors
        private static bool isLive = true;
        private static readonly int NUMBER_OF_INPUT_DISTANCES = 100;
        
        [STAThread]
        static void Main()
        {
            if (isLive)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                Trilateration trilateration = new Trilateration();

                Vector p1 = new Vector(0, 0, 0);
                Vector p2 = new Vector(200, 0, 0);
                Vector p3 = new Vector(0, 0, 200);

                Kalman kalman = new Kalman();
                kalman.VarDistance = 7f;
                kalman.VarProcess = 1f;

                Vector seed = new Vector(510, 508, 510);
                generateReadings(seed);
                var readings = getReadings();
                Vector sR;

                // checkout the output window since it's not a console app it won't print there either..
                // ---------------------------->hit Ctrl+Alt+O
                // I'm also writing the output to "output.txt" next to data..

                using (StreamWriter writer = new StreamWriter("output.txt"))
                {
                    string line = string.Empty;

                    foreach (var r in readings)
                    {
                        sR = kalman.Filter(r.ToFloatArray());
                        var position = trilateration.GetIntersectionPoint(p1.ToString(), p2.ToString(), p3.ToString(), sR.ToString());

                        if (position.valid)
                        {
                            line = position.ToString("F0");
                        }
                        else
                        {
                            line = "Nan";
                        }

                        Console.WriteLine(line += " at " + sR.ToString("F0"));
                        writer.WriteLine(line);
                    }
                }
            }
        }

        private static List<Vector> getMovingAverage(List<Vector> readings, int size)
        {
            List<Vector> res = new List<Vector>();

            for (int i = size - 1; i < readings.Count; i += size)
            {
                Vector sum = Vector.zero;

                for (int j = size - 1; j >= 0; j--)
                {
                    sum += readings[i - j];
                }

                res.Add(sum / size);
            }

            return res;
        }

        private static void generateReadings(Vector seed)
        {
            Random rand = new Random();
            string line = string.Empty;

            using (StreamWriter wr = new StreamWriter("data.txt"))
            {
                for (int i = 0; i < NUMBER_OF_INPUT_DISTANCES; i++)
                {
                    line = new Vector(rand.NextDoubleRange(seed.x, seed.x * 1.02),
                                      rand.NextDoubleRange(seed.y, seed.y * 1.02),
                                      rand.NextDoubleRange(seed.z, seed.z * 1.02)).ToString("F0");

                    wr.WriteLine(line);
                }
            }
        }

        private static void generateReadingsAveraged(Vector seed, int size)
        {
            Random rand = new Random();
            List<Vector> readings = new List<Vector>(NUMBER_OF_INPUT_DISTANCES);
            string line = string.Empty;


            for (int i = 0; i < NUMBER_OF_INPUT_DISTANCES; i++)
            {
                readings.Add(new Vector(rand.NextDoubleRange(seed.x, seed.x * 1.02),
                                        rand.NextDoubleRange(seed.y, seed.y * 1.02),
                                        rand.NextDoubleRange(seed.z, seed.z * 1.02)));

            }
            using (StreamWriter wr = new StreamWriter("data.txt"))
            {
                var avg = getMovingAverage(readings, size);

                foreach(Vector v in avg)
                {
                    wr.WriteLine(v.ToString("F0"));
                }
            }
        }

        private static List<Vector> getReadings()
        {
            List<Vector> readings = new List<Vector>(100);

            using (StreamReader reader = new StreamReader("data.txt"))
            {
                while (!reader.EndOfStream)
                {
                    readings.Add(new Vector(reader.ReadLine()));
                }
            }

            return readings;
        }
    }

    public static class RandomExtensionMethods
    {
        public static float NextDoubleRange(this System.Random random, double minNumber, double maxNumber)
        {
            return (float)(random.NextDouble() * (maxNumber - minNumber) + minNumber);
        }
    }
}
