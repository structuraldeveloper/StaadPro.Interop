using System;

namespace StaadPro.Interop.Models.Property
{
    /// <summary>Contains six explicit immutable native degree-of-freedom flags.</summary>
    /// <remarks>Native order is FX, FY, FZ, MX, MY, MZ. Release creators interpret selection as release; control/dependent relations interpret selection as rigidity. No axis transformation or partial-mode conversion occurs.</remarks>
    public sealed class DegreeOfFreedomSelection
    {
        /// <summary>Validates and copies the complete explicit input packet.</summary>
        /// <param name="xTranslation">Selects native FX translation; true is sent as integral one, false as zero.</param>
        /// <param name="yTranslation">Selects native FY translation; true is sent as integral one, false as zero.</param>
        /// <param name="zTranslation">Selects native FZ translation; true is sent as integral one, false as zero.</param>
        /// <param name="xRotation">Selects native MX rotation; true is sent as integral one, false as zero.</param>
        /// <param name="yRotation">Selects native MY rotation; true is sent as integral one, false as zero.</param>
        /// <param name="zRotation">Selects native MZ rotation; true is sent as integral one, false as zero.</param>
        /// <exception cref="ArgumentOutOfRangeException">A numeric input violates the documented managed preconditions.</exception>
        public DegreeOfFreedomSelection(bool xTranslation, bool yTranslation, bool zTranslation, bool xRotation, bool yRotation, bool zRotation)
        {
            Fx = xTranslation;
            Fy = yTranslation;
            Fz = zTranslation;
            Mx = xRotation;
            My = yRotation;
            Mz = zRotation;
        }
        /// <summary>Gets the explicit selection for native FX translation. Its release/rigidity meaning belongs to the called operation.</summary>
        public bool Fx { get; }
        /// <summary>Gets the explicit selection for native FY translation. Its release/rigidity meaning belongs to the called operation.</summary>
        public bool Fy { get; }
        /// <summary>Gets the explicit selection for native FZ translation. Its release/rigidity meaning belongs to the called operation.</summary>
        public bool Fz { get; }
        /// <summary>Gets the explicit selection for native MX rotation. Its release/rigidity meaning belongs to the called operation.</summary>
        public bool Mx { get; }
        /// <summary>Gets the explicit selection for native MY rotation. Its release/rigidity meaning belongs to the called operation.</summary>
        public bool My { get; }
        /// <summary>Gets the explicit selection for native MZ rotation. Its release/rigidity meaning belongs to the called operation.</summary>
        public bool Mz { get; }
    }
}
