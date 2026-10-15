using System;
using StaadPro.Interop.Enums.Root;
using StaadPro.Interop.Models.Root;
using StaadPro.Interop.Services.Interop;
namespace StaadPro.Interop.Services.Root.Common
{
    internal static class RootAdinaExportService
    {
        internal static void Validate(AdinaAxisSystem axisSystem, AdinaLoadCaseHandling loadCaseHandling)
        {
            if (!Enum.IsDefined(typeof(AdinaAxisSystem), axisSystem)) throw new ArgumentOutOfRangeException(nameof(axisSystem));
            if (!Enum.IsDefined(typeof(AdinaLoadCaseHandling), loadCaseHandling)) throw new ArgumentOutOfRangeException(nameof(loadCaseHandling));
        }
        internal static AdinaExportResult Export(object root, AdinaAxisSystem axisSystem, AdinaLoadCaseHandling loadCaseHandling)
        {
            Validate(axisSystem, loadCaseHandling);
            object status = ((dynamic)root).ExportModelToADINA((int)axisSystem, (int)loadCaseHandling);
            return new AdinaExportResult(NativeValues.ReadInt32(status, "ADINA export status"), axisSystem, loadCaseHandling);
        }
    }
}
