using System;
using System.Globalization;

namespace StaadPro.Interop.Models
{
    /// <summary>Stores a four-part STAAD or host-application version.</summary>
    /// <remarks>Major, Update, Revision and Build follow the main project's version model.
    /// This mutable value does not identify an application instance by itself.</remarks>
    public class ApplicationVersion
    {
        /// <summary>Gets or sets the major version component.</summary>
        public int Major { get; set; }
        /// <summary>Gets or sets the update component (System.Version.Minor).</summary>
        public int Update { get; set; }
        /// <summary>Gets or sets the revision component (System.Version.Build).</summary>
        public int Revision { get; set; }
        /// <summary>Gets or sets the build component (System.Version.Revision).</summary>
        public int Build { get; set; }
        /// <summary>Gets the version prefixed with v, with minimum component widths of 2, 2, 2 and 3.</summary>
        public string Display => string.Format(CultureInfo.InvariantCulture, "v{0:D2}.{1:D2}.{2:D2}.{3:D3}", Major, Update, Revision, Build);

        /// <summary>Parses four nonnegative, dot-separated integer version components.</summary>
        /// <param name="versionInTextFormat">Major.Update.Revision.Build, without a v prefix.</param>
        /// <returns>A new independent version value.</returns>
        /// <exception cref="ArgumentNullException">The text is null.</exception>
        /// <exception cref="ArgumentException">The text does not contain exactly four components or a component is negative.</exception>
        /// <exception cref="FormatException">A component is not an integer.</exception>
        /// <exception cref="OverflowException">A component exceeds Int32 range.</exception>
        public static ApplicationVersion Parse(string versionInTextFormat)
        {
            if (versionInTextFormat == null) throw new ArgumentNullException(nameof(versionInTextFormat));
            string[] parts = versionInTextFormat.Split('.');
            if (parts.Length != 4) throw new ArgumentException("The version must contain four components.", nameof(versionInTextFormat));
            var values = new int[4];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = int.Parse(parts[i], NumberStyles.Integer, CultureInfo.InvariantCulture);
                if (values[i] < 0) throw new ArgumentException("Version components cannot be negative.", nameof(versionInTextFormat));
            }
            return new ApplicationVersion { Major = values[0], Update = values[1], Revision = values[2], Build = values[3] };
        }

        /// <summary>Maps a CLR assembly version into the four-part application version model.</summary>
        /// <param name="version">The CLR version; unspecified Build/Revision components remain -1.</param>
        /// <returns>A new value mapping Major, Minor, Build and Revision in that order.</returns>
        /// <exception cref="ArgumentNullException">The version is null.</exception>
        public static ApplicationVersion Parse(Version version)
        {
            if (version == null) throw new ArgumentNullException(nameof(version));
            return new ApplicationVersion { Major = version.Major, Update = version.Minor, Revision = version.Build, Build = version.Revision };
        }
    }
}
