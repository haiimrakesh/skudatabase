using System.Globalization;
using System.Resources;


namespace SKUApp.Common.Language
{
    public static class LanguageHelper
    {
        private static ResourceManager _resourceManager;

        static LanguageHelper()
        {
            _resourceManager = new ResourceManager("SKUApp.Common.Language.Resources", typeof(LanguageHelper).Assembly);
        }

        public static string GetString(string key)
        {
            return _resourceManager.GetString(key, CultureInfo.CurrentUICulture)??string.Empty;
        }

        public static void SetCulture(string culture)
        {
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
        }
    }
}