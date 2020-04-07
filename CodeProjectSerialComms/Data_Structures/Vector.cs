using System;

namespace CodeProjectSerialComms
{
    public struct Vector
    {
        public float x;
        public float y;
        public float z;
        public bool valid;
        public bool final;
        public string reason;

        public static Vector zero
        {
            get
            {
                return new Vector()
                {
                    x = 0,
                    y = 0,
                    z = 0,
                    valid = true
                };
            }
        }

        public Vector(float x, float y, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            valid = true;
            final = false;
            reason = string.Empty;
        }

        public Vector(string vecstring)
        {
            string[] split = vecstring.Split(',');
            float.TryParse(split[0], out x);
            float.TryParse(split[1], out y);
            float.TryParse(split[2], out z);
            valid = true;
            final = false;
            reason = string.Empty;
        }

        public bool IsNanOrInfinity()
        {
            return float.IsNaN(x) || float.IsInfinity(x) ||
                   float.IsNaN(y) || float.IsInfinity(y) ||
                   float.IsNaN(z) || float.IsInfinity(z);
        }

        public float norm()
        {
            float mag = (float)Math.Sqrt(x * x + y * y + z * z);

            return mag;
        }

        public float[] ToFloatArray()
        {
            return new float[]
            {
                x,
                y,
                z
            };
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", x, y, z);
        }

        public string ToString(string precision)
        {
            return string.Format("{0},{1},{2}", x.ToString(precision), y.ToString(precision), z.ToString(precision));
        }

        public static float dot(Vector vectA, Vector vectB)
        {
            float product;

            product = vectA.x * vectB.x + vectA.y * vectB.y + vectA.z * vectB.z;

            return product;
        }

        public static bool operator ==(Vector vectA, Vector vectB)
        {
            return vectA.x == vectB.x && vectA.y == vectB.y && vectA.z == vectB.z;
        }

        public static bool operator !=(Vector vectA, Vector vectB)
        {
            return vectA.x != vectB.x || vectA.y != vectB.y || vectA.z != vectB.z;
        }

        public static Vector operator *(Vector vectA, Vector vectB)
        {
            Vector crossP = new Vector();

            crossP.x = vectA.y * vectB.z - vectA.z * vectB.y;
            crossP.y = vectA.z * vectB.x - vectA.x * vectB.z;
            crossP.z = vectA.x * vectB.y - vectA.y * vectB.x;

            return crossP;
        }

        public static Vector operator *(Vector vec, float multiplier)
        {
            Vector res = new Vector();

            res.x = vec.x * multiplier;
            res.y = vec.y * multiplier;
            res.z = vec.z * multiplier;

            return res;
        }

        public static Vector operator /(Vector vec, float divisor)
        {
            Vector res = new Vector();

            res.x = vec.x / divisor;
            res.y = vec.y / divisor;
            res.z = vec.z / divisor;

            return res;
        }

        public static Vector operator +(Vector vectA, Vector vectB)
        {
            Vector res = new Vector();

            res.x = vectA.x + vectB.x;
            res.y = vectA.y + vectB.y;
            res.z = vectA.z + vectB.z;

            return res;
        }

        public static Vector operator -(Vector vectA, Vector vectB)
        {
            Vector res = new Vector();

            res.x = vectA.x - vectB.x;
            res.y = vectA.y - vectB.y;
            res.z = vectA.z - vectB.z;

            return res;
        }

        public static Vector operator -(Vector vectA)
        {
            Vector res = new Vector();

            res.x = -vectA.x;
            res.y = -vectA.y;
            res.z = -vectA.z;

            return res;
        }
    }

    public static class VectorExtensions
    {
        public static Vector RoundToNearestInt(this Vector v)
        {
            return new Vector(Convert.ToInt32(v.x), Convert.ToInt32(v.y), Convert.ToInt32(v.z));
        }

        public static int GetMaxIntComponenet(this Vector v)
        {
            return Convert.ToInt32(Math.Max(v.x, Math.Max(v.y, v.z)));
        }
    }
}
