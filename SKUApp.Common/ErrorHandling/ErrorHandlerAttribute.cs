using System;
using System;
using System.Reflection;

namespace SKUApp.Common.ErrorHandling
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ErrorHandlerAttribute : Attribute
    {
        public void Handle(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                // Log the exception (you can replace this with your own logging mechanism)
                Console.WriteLine(ex);

                // Handle the exception (you can customize this as needed)
            }
        }
    }

    public static class ErrorHandlerExtensions
    {
        public static void ExecuteWithErrorHandler(this MethodInfo methodInfo, object obj, params object[] parameters)
        {
            var attribute = methodInfo.GetCustomAttribute<ErrorHandlerAttribute>();
            if (attribute != null)
            {
                attribute.Handle(() => methodInfo.Invoke(obj, parameters));
            }
            else
            {
                methodInfo.Invoke(obj, parameters);
            }
        }
    }
}
