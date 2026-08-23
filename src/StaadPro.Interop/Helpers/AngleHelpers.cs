using System;

namespace StaadPro.Interop.Helpers
{
    public static class AngleHelpers
    {
        public static double PositiveComplimentOf(double angleInDegrees)
        {
            double positiveAngle = angleInDegrees % Constants.WholeCircleAngleInDegrees;
            if (positiveAngle < 0) positiveAngle += Constants.WholeCircleAngleInDegrees;
            return positiveAngle;
        }

        public static double StandardizeToPlusOrMinus180Degrees(double angleInDegrees)
        {
            double angle = angleInDegrees % 360.0;
            if (angle > 180.0) angle -= 360.0;
            else if (angle < -180.0) angle += 360.0;
            return angle;
        }
    }
}
