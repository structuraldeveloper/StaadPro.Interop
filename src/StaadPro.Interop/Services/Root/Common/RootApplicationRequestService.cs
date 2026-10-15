namespace StaadPro.Interop.Services.Root.Common
{
    internal static class RootApplicationRequestService
    {
        internal static void Quit(object root) => ((dynamic)root).Quit();
    }
}
