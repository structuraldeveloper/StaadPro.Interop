using System;
using System.Linq;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models.Loads.Wind;

namespace StaadPro.Interop.Services.Load
{
    internal sealed class WindNativePacket
    {
        private WindNativePacket() { }
        internal int StructureType { get; private set; }
        internal int[] Units { get; private set; }
        internal double[] Escarpment { get; private set; }
        internal double[] Structure { get; private set; }

        internal static WindNativePacket Capture(IASCE7StructureData structure, WindUnitsConfiguration units,
            WindEscarpmentData escarpment, ASCE7Publication publication, bool profileCalculation)
        {
            if (structure == null) throw new ArgumentNullException(nameof(structure));
            if (units == null) throw new ArgumentNullException(nameof(units));
            if (escarpment == null) throw new ArgumentNullException(nameof(escarpment));
            LoadArguments.Defined(publication, nameof(publication));
            ASCE7StructureType type = structure.StructureType;
            LoadArguments.Defined(type, nameof(structure.StructureType));
            structure.Validate();
            // The computation tables use the legacy enclosure/tank domain even in the 2016 calculation.
            double[] values = structure.ToDoubleArray(profileCalculation ? ASCE7Publication.ASCE7_05_10 : publication);
            if (values == null || values.Length != 7) throw new ArgumentException("Structure data must supply seven values.", nameof(structure));
            values = (double[])values.Clone();
            foreach (double value in values) LoadArguments.Finite(value, nameof(structure));
            // Computation packets have a different order from definition packets for these two families.
            if (profileCalculation && (type == ASCE7StructureType.OpenSign || type == ASCE7StructureType.LatticeFramework))
                values = new[] { values[0], values[1], values[2], values[3], values[5], values[6], values[4] };
            if (profileCalculation && type == ASCE7StructureType.TrussedTower)
                values = new[] { values[0], values[1], values[2], values[4], values[5], values[3], values[6] };
            return new WindNativePacket
            {
                StructureType = (int)type,
                Structure = values,
                Units = units.ToIntArray(),
                Escarpment = escarpment.ToDoubleArray()
            };
        }
    }
}
