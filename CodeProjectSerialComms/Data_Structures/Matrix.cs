using System;

namespace CodeProjectSerialComms
{
    public static class Matrix
    {
        public static Vector MultiplyMatrix(this float[,] a, Vector point)
        {
            Vector res = new Vector();
            res.valid = false;
            float[,] c = null;
            float[,] b = new float[,]
            {
                { point.x },
                { point.y },
                { point.z }
            };

            if (a.GetLength(1) == b.GetLength(0))
            {
                c = new float[a.GetLength(0), b.GetLength(1)];
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < a.GetLength(1); k++) // OR k<b.GetLength(0)
                            c[i, j] = c[i, j] + a[i, k] * b[k, j];
                    }
                }
            }
            else
            {
                Console.WriteLine("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Console.WriteLine("\n Please re-enter correct dimensions.");
            }

            if (c != null)
            {
                res.x = c[0, 0];
                res.y = c[1, 0];
                res.z = c[2, 0];
                res.valid = true;
            }

            return res;
        }
    }
}
