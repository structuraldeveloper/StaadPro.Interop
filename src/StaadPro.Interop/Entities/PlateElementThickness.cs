using System;
using System.Collections.Generic;
using StaadPro.Interop.Common;

namespace StaadPro.Interop.Entities
{
    /// <summary>Carries four native plate-corner thicknesses and optional assignment candidates.</summary>
    /// <remarks>Values retain native length units and incidence order A/B/C/D. Id identifies a thickness definition, never a plate. No triangular fourth-corner convention or unit conversion is inferred. This port omits the private units-library conversion helper.</remarks>
    public sealed class PlateElementThickness : IEntity
    {
        /// <summary>Initializes zero values and an empty assignment set.</summary>
        public PlateElementThickness() { AssignedPlates = new HashSet<Plate>(); }
        /// <summary>Initializes all four corner values to one thickness.</summary>
        /// <param name="tUniform">Thickness in the native creation/query length convention.</param>
        public PlateElementThickness(double tUniform) : this(tUniform, tUniform, tUniform, tUniform) { }
        /// <summary>Initializes the four independent corner values.</summary>
        /// <param name="a">First incidence-corner thickness.</param>
        /// <param name="b">Second incidence-corner thickness.</param>
        /// <param name="c">Third incidence-corner thickness.</param>
        /// <param name="d">Fourth native slot, retained even for triangular elements.</param>
        public PlateElementThickness(double a, double b, double c, double d) : this() { A = a; B = b; C = c; D = d; }
        /// <summary>Gets or sets the native thickness property ID; zero means not resolved or created.</summary>
        public int Id { get; set; }
        /// <summary>Gets or sets the first incidence-corner thickness in native length units.</summary>
        public double A { get; set; }
        /// <summary>Gets or sets the second incidence-corner thickness in native length units.</summary>
        public double B { get; set; }
        /// <summary>Gets or sets the third incidence-corner thickness in native length units.</summary>
        public double C { get; set; }
        /// <summary>Gets or sets the fourth native thickness slot without triangular-element substitution.</summary>
        public double D { get; set; }
        /// <summary>Gets the mutable set of candidate plate references used for explicit assignment.</summary>
        /// <remarks>Changing this set alone does not change STAAD. Retrieval populates it only in the assignment-aware methods.</remarks>
        public HashSet<Plate> AssignedPlates { get; private set; }
        /// <summary>Copies the four values in native incidence order.</summary>
        /// <returns>A new array containing A, B, C and D.</returns>
        public double[] ToArray() => new[] { A, B, C, D };
        /// <summary>Creates an unnumbered thickness packet from four corner values.</summary>
        /// <param name="thkArray">Exactly four values; copied into the packet.</param>
        /// <returns>A new packet with Id zero and no assigned plates.</returns>
        /// <exception cref="ArgumentException">The array is null or does not have four entries.</exception>
        public static PlateElementThickness Create(double[] thkArray) => Create(0, thkArray);
        /// <summary>Creates a numbered thickness packet from four corner values.</summary>
        /// <param name="petId">Thickness property ID, or zero for an unresolved definition.</param>
        /// <param name="thkArray">Exactly four values; copied into the packet.</param>
        /// <returns>A new packet with the supplied ID and no assigned plates.</returns>
        /// <exception cref="ArgumentException">The array is null or does not have four entries.</exception>
        public static PlateElementThickness Create(int petId, double[] thkArray)
        {
            if (thkArray == null || thkArray.Length != 4) throw new ArgumentException("Provide exactly four thickness slots.", nameof(thkArray));
            return new PlateElementThickness(thkArray[0], thkArray[1], thkArray[2], thkArray[3]) { Id = petId };
        }
    }
}
