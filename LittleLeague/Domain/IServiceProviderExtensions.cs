using Ninject;

namespace System
{
    public static class IServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider provider) where T : class
        {
            return provider.GetService(typeof(T)) as T;
        }

        //If you want the name resolution you get with dependency injection you can check the
        //provider or throw an error.
        public static T GetService<T>(this IServiceProvider provider, string name) where T : class
        {
            if (!(provider is IKernel))
            {
                throw new InvalidOperationException("The provider does not provide required functionality.");
            }

            return ((IKernel)provider).Get<T>(name);
        }
    }
}
