using NUnit.Framework;
using StaadPro.Interop.Interfaces;
using StaadPro.Interop.Models;
using StaadPro.Interop.Services;
using StaadPro.Interop.Tests.Models;

namespace StaadPro.Interop.Tests.Services
{
    [TestFixture]
    public class OpenStaadWrapperProviderTests
    {
        public class MockResolver : IOpenStaadWrapperResolver
        {
            public OpenStaadWrapper Get(string fileFullPath = null)
            {
                return new OpenStaadWrapper(new OpenStaadWrapperTests.MockStaadRootCom(), isDedicated: !string.IsNullOrEmpty(fileFullPath));
            }
        }

        [Test]
        public void OpenStaadWrapperProvider_CustomResolver_AcquiresConfiguredWrapper()
        {
            var originalResolver = OpenStaadWrapperProvider.Resolver;
            try
            {
                OpenStaadWrapperProvider.Resolver = new MockResolver();
                var wrapper = OpenStaadWrapperProvider.Get(@"C:\Models\Structure.std");

                Assert.IsNotNull(wrapper);
                Assert.IsTrue(wrapper.IsConnected);
                Assert.IsTrue(wrapper.IsDedicated);
            }
            finally
            {
                OpenStaadWrapperProvider.Resolver = originalResolver;
            }
        }

        [Test]
        [Category("LiveIntegration")]
        [Explicit("Run only with no active STAAD session; this test reads the real Windows ROT.")]
        public void OpenStaadWrapperProvider_DefaultGetRunning_ReturnsNullWhenNoStaad()
        {
            var wrapper = OpenStaadWrapperProvider.GetRunning();
            // In headless CI runner without STAAD installed, it safely returns null
            Assert.IsNull(wrapper);
        }
    }
}
