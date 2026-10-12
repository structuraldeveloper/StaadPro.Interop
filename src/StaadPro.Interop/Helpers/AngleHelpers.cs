using System;

namespace StaadPro.Interop.Helpers
{
    public static class AngleHelpers
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='AngleHelpers.PositiveComplimentOf`0(double)']/*" />
        public static double PositiveComplimentOf(double angleInDegrees)
        {
            double positiveAngle = angleInDegrees % Constants.WholeCircleAngleInDegrees;
            if (positiveAngle < 0) positiveAngle += Constants.WholeCircleAngleInDegrees;
            return positiveAngle;
        }

        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='AngleHelpers.StandardizeToPlusOrMinus180Degrees`0(double)']/*" />
        public static double StandardizeToPlusOrMinus180Degrees(double angleInDegrees)
        {
            double angle = angleInDegrees % 360.0;
            if (angle > 180.0) angle -= 360.0;
            else if (angle < -180.0) angle += 360.0;
            return angle;
        }
    }
}
