namespace CodeProjectSerialComms
{
    public class Kalman
    {
        private float[] Pc;
        private float[] G;
        private float[] P;
        private float[] Xp;
        private float[] Zp;
        private float[] Xe;

        public float VarDistance { get; set; }
        public float VarProcess { get; set; }

        public Kalman()
        {
            Pc = new float[3];
            G  = new float[3];
            P  = new float[3];
            Xp = new float[3];
            Zp = new float[3];
            Xe = new float[3];
        }

        public Vector Filter(float[] measuredValues)
        {
            for (int i = 0; i < measuredValues.Length; i++)
            {
                Pc[i] = P[i] + VarProcess;
                G[i] = Pc[i] / (Pc[i] + VarDistance);
                P[i] = (1 - G[i]) * Pc[i];
                Xp[i] = Xe[i];
                Zp[i] = Xp[i];
                Xe[i] = G[i] * (measuredValues[i] - Zp[i]) + Xp[i];
            }

            return new Vector(Xe[0], Xe[1], Xe[2]);
        }
    }
}
