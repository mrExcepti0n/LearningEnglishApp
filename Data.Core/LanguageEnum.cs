using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Core
{
    public enum LanguageEnum
    {
        English = 1,
        Russian = 2
    }

    public static class LanguageEnumExtensions
    {
        public static CultureInfo GetCulture(this LanguageEnum language)
        {
            switch (language)
            {
                case LanguageEnum.English:
                    return CultureInfo.GetCultureInfo("en-us");
                case LanguageEnum.Russian:
                    return CultureInfo.GetCultureInfo("ru");
                default:
                    throw new NotImplementedException();

            }
        }
    }
   
}
