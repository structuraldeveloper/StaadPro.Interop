using StaadPro.Interop.Services.Load;
using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Models.Loads.Wind
{
    /// <summary>
    /// Encapsulates hill and escarpment topographic data for wind speed-up calculations (Kzt factor) in OpenSTAAD.
    /// </summary>
    public class WindEscarpmentData
    {
        /// <summary>
        /// Gets or sets the topographic feature type (2D Ridge, 2D Escarpment, 3D Escarpment).
        /// </summary>
        public EscarpmentTopographyType TopographyType { get; set; } = EscarpmentTopographyType.TwoDimensionalEscarpment;

        /// <summary>
        /// Gets or sets the height of the hill or escarpment relative to the upwind terrain (H).
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the distance upwind of crest to where the difference in ground elevation is half the height of the hill/escarpment (Lh).
        /// </summary>
        public double DistanceUpwindOfCrest { get; set; }

        /// <summary>
        /// Gets or sets the distance from the crest to the building / structure site (x).
        /// </summary>
        public double DistanceFromCrestToBuilding { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindEscarpmentData"/> class.
        /// </summary>
        public WindEscarpmentData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindEscarpmentData"/> class with specified geometric parameters.
        /// </summary>
        /// <param name="topographyType">The topographic feature type.</param>
        /// <param name="height">Height of the hill or escarpment (H).</param>
        /// <param name="distanceUpwindOfCrest">Distance upwind of crest (Lh).</param>
        /// <param name="distanceFromCrestToBuilding">Distance from crest to building (x).</param>
        public WindEscarpmentData(
            EscarpmentTopographyType topographyType,
            double height,
            double distanceUpwindOfCrest,
            double distanceFromCrestToBuilding)
        {
            TopographyType = topographyType;
            Height = height;
            DistanceUpwindOfCrest = distanceUpwindOfCrest;
            DistanceFromCrestToBuilding = distanceFromCrestToBuilding;
        }

        /// <summary>
        /// Validates the escarpment parameters.
        /// </summary>
        /// <remarks>Rejects nonfinite numeric values and undefined classifications before native use. Does not certify engineering suitability.</remarks>
        /// <exception cref="ArgumentOutOfRangeException">A scalar or classification is invalid.</exception>
        public void Validate()
        {
            LoadArguments.Defined(TopographyType, nameof(TopographyType));
            LoadArguments.Finite(Height, nameof(Height));
            LoadArguments.Finite(DistanceUpwindOfCrest, nameof(DistanceUpwindOfCrest));
            LoadArguments.Finite(DistanceFromCrestToBuilding, nameof(DistanceFromCrestToBuilding));
            if (Height < 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(Height), "Escarpment height H cannot be negative.");
            }

            if (DistanceUpwindOfCrest < 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(DistanceUpwindOfCrest), "Upwind crest distance Lh cannot be negative.");
            }
        }

        /// <summary>
        /// Marshals the escarpment data into the exact 4-element double array expected by OpenSTAAD COM.
        /// </summary>
        /// <returns>A double array of size 4: [Type, H, Lh, x].</returns>
        public double[] ToDoubleArray()
        {
            Validate();
            return new double[4]
            {
                (double)(int)TopographyType,
                Height,
                DistanceUpwindOfCrest,
                DistanceFromCrestToBuilding
            };
        }
    }
}
