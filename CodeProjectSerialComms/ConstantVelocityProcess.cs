using Accord.Math;
using Accord.Statistics.Distributions.Univariate;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeProjectSerialComms
{
    public class ConstantVelocityProcess
    {
        public static Size WorkingArea = new Size(100, 100);
        public static float TimeInterval = 1;

        NormalDistribution normalDistribution = new NormalDistribution(0, 0.2);
        Random rand = new Random();

        ConstantVelocity2DModel initialState;
        ConstantVelocity2DModel currentState;

        public ConstantVelocityProcess()
        {
            currentState = new ConstantVelocity2DModel
            {
                Position = new Vector(50, 1),
                Velocity = new Vector(0.3f, 0.3f)
            };

            initialState = currentState;
        }

        public void GoToNextState(out bool doneFullCycle)
        {
            Func<Vector, bool> isBorder = (point) =>
            {
                return point.x <= 0 || point.x >= WorkingArea.Width ||
                       point.y <= 0 || point.y >= WorkingArea.Height;
            };

            doneFullCycle = false;
            var prevPos = currentState.Position;
            var speed = currentState.Velocity;

            if (isBorder(currentState.Position))
            {
                var temp = speed.x;
                speed.x = -speed.y;
                speed.y = temp;

                if (speed.Equals(initialState.Velocity)) doneFullCycle = true;
            }

            var nextState = new ConstantVelocity2DModel
            {
                Position = new Vector
                {
                    x = prevPos.x + speed.x * TimeInterval,
                    y = prevPos.y + speed.y * TimeInterval
                },

                Velocity = speed
            };

            currentState = nextState;
        }

        public ConstantVelocity2DModel GetNoisyState(double accelerationNoise)
        {
            var processNoiseMat = ConstantVelocity2DModel.GetProcessNoise(accelerationNoise);
            var noise = normalDistribution.Generate(ConstantVelocity2DModel.Dimension).Multiply(processNoiseMat);

            return new ConstantVelocity2DModel
            {
                Position = new Vector
                {
                    x = currentState.Position.x + (float)noise[0],
                    y = currentState.Position.y + (float)noise[2]
                },

                Velocity = new Vector
                {
                    x = currentState.Velocity.x + (float)noise[1],
                    y = currentState.Velocity.y + (float)noise[3]
                }
            };
        }

        public PointF TryGetNoisyMeasurement(double measurementNoise, out bool isSuccess, double missingMeasurementProbability = 0.2)
        {
            isSuccess = rand.NextDouble() > missingMeasurementProbability;
            if (!isSuccess)
                return new PointF();

            return new PointF
            {
                X = currentState.Position.x + (float)normalDistribution.Generate() * (float)measurementNoise,
                Y = currentState.Position.y + (float)normalDistribution.Generate() * (float)measurementNoise
            };
        }
    }
}
