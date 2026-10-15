using System;
using StaadPro.Interop.Enums;

namespace StaadPro.Interop.Services.Root.Common
{
    // Each operation validates before one explicit native mutation. No retries or implicit persistence.
    internal static class RootModelEditService
    {
        internal static void SetInputUnitForLength(object root, LengthInputUnit lengthUnit)
        {
            int code = (int)lengthUnit;
            if (code < 0 || code > 7) throw new ArgumentOutOfRangeException(nameof(lengthUnit));
            ((dynamic)root).SetInputUnitForLength(code);
        }

        internal static void SetInputUnitForForce(object root, ForceInputUnit forceUnit)
        {
            if (!Enum.IsDefined(typeof(ForceInputUnit), forceUnit))
                throw new ArgumentOutOfRangeException(nameof(forceUnit));
            ((dynamic)root).SetInputUnitForForce((int)forceUnit);
        }

        internal static void SetShortJobInfo(object root, string jobName, string jobClient, string engineerName)
        {
            if (jobName == null) throw new ArgumentNullException(nameof(jobName));
            if (jobClient == null) throw new ArgumentNullException(nameof(jobClient));
            if (engineerName == null) throw new ArgumentNullException(nameof(engineerName));
            ((dynamic)root).SetShortJobInfo(jobName, jobClient, engineerName);
        }

        internal static void UpdateStructure(object root)
        {
            ((dynamic)root).UpdateStructure();
        }
    }
}
