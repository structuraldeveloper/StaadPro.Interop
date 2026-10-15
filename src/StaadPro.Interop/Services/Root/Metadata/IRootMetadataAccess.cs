using System;
using StaadPro.Interop.Enums;
using StaadPro.Interop.Models;

namespace StaadPro.Interop.Services.Root.Metadata
{
    internal interface IRootMetadataAccess
    {
        ApplicationVersion GetApplicationVersion();
        BaseUnitSystem GetBaseUnit();
        ForceInputUnit GetInputUnitForForce();
        LengthInputUnit GetInputUnitForLength();
        IntPtr GetMainWindowHandle();
    }
}
