using System;

namespace StaadPro.Interop.Extensions
{
    public static class MathExtensions
    {
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='MathExtensions.Radians`0(thisdouble)']/*" />
        public static double Radians(this double degrees) => degrees * (Math.PI / 180.0);
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='MathExtensions.Degrees`0(thisdouble)']/*" />
        public static double Degrees(this double radians) => radians * (180.0 / Math.PI);
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='MathExtensions.Square`0(thisdouble)']/*" />
        public static double Square(this double value) => value * value;
        /// <include file="../Documentation/ApiMembers.xml" path="doc/members/member[@name='MathExtensions.Ceiling`0(thisdouble,double)']/*" />
        public static double Ceiling(this double value, double roundTo)
        {
            if (roundTo <= 0) return Math.Ceiling(value);
            return Math.Ceiling(value / roundTo) * roundTo;
        }
    }
}
