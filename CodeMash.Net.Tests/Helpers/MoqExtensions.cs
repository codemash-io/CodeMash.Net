using Microsoft.Practices.Unity;
using Moq;

namespace CodeMash.Net.Tests
{
    public static class MoqExtensions
    {
        public static Mock<T> RegisterMock<T>(this IUnityContainer container) where T : class
        {
            var mock = new Mock<T>();

            container.RegisterInstance(mock);
            container.RegisterInstance(mock.Object);

            return mock;
        }

        /// <summary>
        /// Use this to add additional setups for a mock that is already registered
        /// </summary>
        public static Mock<T> ConfigureMockFor<T>(this IUnityContainer container) where T : class
        {
            return container.Resolve<Mock<T>>();
        }

        public static void VerifyMockFor<T>(this IUnityContainer container) where T : class
        {
            container.Resolve<Mock<T>>().VerifyAll();
        }
    }
}