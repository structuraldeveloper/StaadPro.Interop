using System.ComponentModel;

namespace StaadPro.Interop.Enums
{
    /// <summary>
    /// Specifies the engineering load type classification for a STAAD.Pro load case.
    /// </summary>
    public enum LoadType
    {
        Dead = 0,
        Live = 1,
        [Description("Roof Live")]
        RoofLive = 2,
        Wind = 3,
        [Description("Seismic Horizontal")]
        SeismicH = 4,
        [Description("Seismic Vertical")]
        SeismicV = 5,
        Snow = 6,
        Fluids = 7,
        Soil = 8,
        Rain = 9,
        Ponding = 10,
        Dust = 11,
        Traffic = 12,
        Temp = 13,
        Imperfection = 14,
        Accidental = 15,
        Flood = 16,
        Ice = 17,
        [Description("Wind on Ice")]
        WindIce = 18,
        [Description("Crane Hook")]
        CraneHook = 19,
        Mass = 20,
        Gravity = 21,
        Push = 22,
        None = 23
    }
}
