using System;

namespace StaadPro.Interop.Attributes
{
    /// <summary>
    /// Decorates properties of load definitions that uniquely define load characteristics.
    /// Used for structural equivalence checking and automated grouping.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class LoadDefiningPropertyAttribute : Attribute
    {
    }
}
