using DemoDomain.Enums.General.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Constants
{
    public static class ConstApplicationGeneral
    {
        public static long SystemAppUserId => 1;
        public static string SystemAppUserName => "Complaints System User";
        public static string ApplicationNameEn => "Complaints";
        public static string SmsNotificationTitleEn => "Complaints";
        public static string ExceptionErrorTemplate => "{\"AppErrorKey\": @AppErrorKey,\"Messages\": [{\"LangCode\": \"" + EnumLanguages.Arabic.LangCode + "\", \"Message\": \"@Message_" + EnumLanguages.Arabic.LangCode + "\"}, {\"LangCode\": \"" + EnumLanguages.English.LangCode + "\", \"Message\": \"@Message_" + EnumLanguages.English.LangCode + "\"}],\"PropertyName\": \"@PropertyName\" }";
    }
}
