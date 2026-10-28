# StaadPro.Interop branding

The two supplied root PNG files are the package's adopted logo assets. Preserve the original artwork and reuse these assets for future package and documentation updates.

| Asset | Purpose | Original dimensions | File size |
| --- | --- | --- | --- |
| [symbol-only-logo.png](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/symbol-only-logo.png) | Embedded NuGet package icon, including package-list and Visual Studio display | 1254 × 1254 | 873,573 bytes |
| [logo-with-text.png](https://github.com/structuraldeveloper/StaadPro.Interop/blob/main/logo-with-text.png) | Repository and NuGet README banner; wider documentation placements | 2172 × 724 | 430,180 bytes |

The [400-pixel README rendition](images/logo-with-text-400.png) is a 400 × 133 PNG derived from the original wordmark solely for compact display. Both root PNG originals remain unchanged.

The project sets `PackageIcon` to `images/symbol-only-logo.png` and packs both originals plus the compact wordmark rendition under `images/`. These settings apply to future packages without changing their identity, publisher or version scheme.

NuGet supports embedded PNG/JPEG icons up to 1 MB and recommends 128 × 128 pixels. The supplied square symbol is within the size limit and retains its proportions in small icon slots. The original is embedded unchanged; clients scale it for display. A text logo is not prohibited, but the supplied 3:1 banner makes its text much smaller when fitted into a square slot. Use that banner where sufficient horizontal space is available. See [NuGet's icon specification](https://learn.microsoft.com/en-us/nuget/reference/nuspec#icon).

The repository README references the root banner directly. The NuGet README references the compact rendition through an absolute HTTPS URL on raw.githubusercontent.com, an allowed NuGet image host. Push the root assets before publishing a package so that the banner URL is available. A packaged local image alone cannot serve as a NuGet.org README image. See [NuGet's README image requirements](https://learn.microsoft.com/en-us/nuget/nuget-org/package-readme-on-nuget-org#allowed-domains-for-images-and-badges).

The repository README uses an HTML image tag with `width="400"`, which GitHub supports. The NuGet README uses ordinary Markdown image syntax: NuGet deliberately disables inline HTML and otherwise displays a raw image tag as text. The compact PNG supplies the 400 × 133 intrinsic display size without HTML attributes. Do not switch the NuGet README back to a raw HTML image or reference the full-size wordmark there. NuGet constrains image display to the available width on narrower screens. See the [NuGet renderer implementation](https://github.com/NuGet/NuGetGallery/blob/main/src/NuGetGallery/Services/MarkdownService.cs).

The release verifier checks the embedded icon path, size, format and square dimensions, and verifies that both wordmarks are included, the compact rendition is 400 × 133 pixels, and the NuGet README uses the Markdown image with an allowed absolute URL. Preview 1.0.0-preview.20261027.1 introduced the adopted assets; preview 1.0.0-preview.20261027.2 applies the compact 400-pixel README sizing. Earlier generated archives retain their original metadata.
