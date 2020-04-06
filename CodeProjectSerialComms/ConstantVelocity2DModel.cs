using System;
using Accord.Math;

namespace CodeProjectSerialComms
{
    public class ConstantVelocity2DModel : ICloneable
    {
        public const int Dimension = 4;
        public Vector Position;
        public Vector Velocity;

        public ConstantVelocity2DModel()
        {
            this.Position = default(Vector);
            this.Velocity = default(Vector);
        }

        /// <summary>
        /// Evaluates the model by using the provided transition matrix.
        /// </summary>
        /// <param name="transitionMat">Transition matrix.</param>
        /// <returns>New model state.</returns>
        public ConstantVelocity2DModel Evaluate(double[,] transitionMat)
        {
            var stateVector = transitionMat.Dot(ToArray(this));
            return ConstantVelocity2DModel.FromArray(stateVector);
        }

        /// <summary>
        /// Gets the state transition matrix [4 x 4].
        /// </summary>
        /// <param name="timeInterval">Time interval.</param>
        /// <returns>State transition matrix.</returns>
        public static double[,] GetTransitionMatrix(double timeInterval = 1)
        {
            var t = timeInterval;

            return new double[,]
                {
                    {1, t, 0, 0},
                    {0, 1, 0, 0},
                    {0, 0, 1, t},
                    {0, 0, 0, 1}
                };
        }

        /// <summary>
        /// Gets the position measurement matrix [2 x 4] used in Kalman filtering.
        /// </summary>
        /// <returns>Position measurement matrix.</returns>
        public static double[,] GetPositionMeasurementMatrix()
        {
            return new double[,] //just pick point coordinates for an observation [2 x 6] (look at used state model)
                { 
                   //X,  vX, Y,  vY   (look at ConstantAcceleration2DModel)
                    {1,  0,  0,  0}, //picks X
                    {0,  0,  1,  0}  //picks Y
                };
        }

        /// <summary>
        /// Gets process-noise matrix [4 x 2] where the location is affected by (dt * dt) / 2 and velocity with the factor of dt - integrals of dt. 
        /// Factor 'dt' represents time interval.
        /// </summary>
        /// <param name="accelerationNoise">Acceleration noise.</param>
        /// <param name="timeInterval">Time interval.</param>
        /// <returns>Process noise matrix.</returns>
        public static double[,] GetProcessNoise(double accelerationNoise, double timeInterval = 1)
        {
            var dt = timeInterval;
            var G = new double[,]
            {
                {(dt*dt) / 2, 0},
                {dt, 0},
                {0, (dt*dt) / 2},
                {0, dt}
            };

            var Q = Accord.Math.Matrix.Diagonal<double>(G.GetLength(1), accelerationNoise); //TODO - check: noise * noise ?
            var gmultq = Accord.Math.Matrix.Multiply(G, Q);
            var gpose = G.Transpose();
            var arch = Accord.Math.Matrix.Multiply(gmultq, gpose);
            //var processNoise = Accord.Math.Matrix.Multiply(G, Q).Accord.Math.Matrix.Multiply(G.Transpose());
            return arch;
        }

        #region Array conversion

        /// <summary>
        /// Converts the array to the model.
        /// </summary>
        /// <param name="arr">Array to convert from.</param>
        /// <returns>Model.</returns>
        public static ConstantVelocity2DModel FromArray(double[] arr)
        {
            return new ConstantVelocity2DModel
            {
                Position = new Vector((float)arr[0], (float)arr[2]),
                Velocity = new Vector((float)arr[1], (float)arr[3]),
            };
        }

        /// <summary>
        /// Converts the model to the array.
        /// </summary>
        /// <param name="modelState">Model to convert.</param>
        /// <returns>Array.</returns>
        public static double[] ToArray(ConstantVelocity2DModel modelState)
        {
            return new double[]
                {
                    modelState.Position.x,
                    modelState.Velocity.x,

                    modelState.Position.y,
                    modelState.Velocity.y,
                };
        }

        #endregion

        /// <summary>
        /// Clones the model.
        /// </summary>
        /// <returns>The copy of the model.</returns>
        public object Clone()
        {
            return new ConstantVelocity2DModel
            {
                Position = this.Position,
                Velocity = this.Velocity
            };
        }
    }
}
