using SharedModels;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Enums.General.Language
{
    public class EnumLanguages : Enumeration
    {
        public string LangCode { get; }

        private EnumLanguages(BaseEnum_Languages langId, string langName, string langCode) : base(langId.ToInt(), langName)
        {
            LangCode = langCode;
        }

        public static readonly EnumLanguages English = new(BaseEnum_Languages.English, "English", "en");
        public static readonly EnumLanguages Arabic = new(BaseEnum_Languages.Arabic, "Arabic", "ar");

        public static IReadOnlyList<EnumLanguages> GetList()
        {
            return GetAll<EnumLanguages>().ToList();
        }
    }
    public enum BaseEnum_Languages
    {
        English = 1,
        Arabic = 2
    }
}
