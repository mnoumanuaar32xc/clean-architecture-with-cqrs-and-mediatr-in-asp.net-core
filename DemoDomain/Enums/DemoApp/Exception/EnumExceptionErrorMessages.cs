using DemoDomain.Enums.General.Language;
using DemoDomain.Exceptions.Models;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDomain.Enums.DemoApp.Exception
{
    public class EnumExceptionErrorMessages : Enumeration
    {
        public List<ErrorsLang> Messages { get; set; }

        private EnumExceptionErrorMessages(BaseEnumExceptionErrorMessages appErrorKeyId, string appErrorKeyName, List<ErrorsLang> messages) : base((int)appErrorKeyId, appErrorKeyName)
        {
            Messages = messages;
        }

        #region Default Handled Exceptions (Code : 1 => 100)
        public static readonly EnumExceptionErrorMessages UnknownException = new(BaseEnumExceptionErrorMessages.UnknownException, BaseEnumExceptionErrorMessages.UnknownException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "An unexpected error occurred. Please contact the system administrator and provide the following log number for further assistance: {$LogNumber$}." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "حدث خطأ غير متوقع. يرجى التواصل مع مسؤول النظام وتزويده برقم السجل التالي لمزيد من المساعدة: {$LogNumber$}" }
                });

        public static readonly EnumExceptionErrorMessages UnauthorizedException = new(BaseEnumExceptionErrorMessages.UnauthorizedException, BaseEnumExceptionErrorMessages.UnauthorizedException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Authentication required: You must be logged in to access this resource. Please log in and try again." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تتطلب المصادقة: يجب تسجيل الدخول للوصول إلى هذا المورد. يرجى تسجيل الدخول والمحاولة مرة أخرى" }
                });

        public static readonly EnumExceptionErrorMessages InvalidCredentialException = new(BaseEnumExceptionErrorMessages.InvalidCredentialException, BaseEnumExceptionErrorMessages.InvalidCredentialException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The login attempt was unsuccessful due to invalid credentials. Ensure your credentials are correct and try again." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "فشلت محاولة تسجيل الدخول بسبب بيانات اعتماد غير صحيحة. تأكد من صحة بياناتك وحاول مرة أخرى" }
                });

        public static readonly EnumExceptionErrorMessages ForbiddenException = new(BaseEnumExceptionErrorMessages.ForbiddenException, BaseEnumExceptionErrorMessages.ForbiddenException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "You do not have permission to access this resource. Please contact your administrator if you believe this is an error." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لا تمتلك الإذن للوصول إلى هذا المورد. يرجى الاتصال بالمسؤول إذا كنت تعتقد أن هذا خطأ" }
                });

        public static readonly EnumExceptionErrorMessages BussinessValidationException = new(BaseEnumExceptionErrorMessages.BussinessValidationException, BaseEnumExceptionErrorMessages.BussinessValidationException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Validation error: The request does not meet the required business rules. Please check the conditions and resubmit." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "خطأ في التحقق: الطلب لا يتوافق مع قواعد العمل المطلوبة. يرجى التحقق من الشروط وإعادة التقديم" }
                });

        public static readonly EnumExceptionErrorMessages ParametersValidationException = new(BaseEnumExceptionErrorMessages.ParametersValidationException, BaseEnumExceptionErrorMessages.ParametersValidationException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Inputs validation error: Some of the provided inputs are incorrect or incomplete. Please correct the inputs and retry." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "خطأ في التحقق من المدخلات: بعض المدخلات المقدمة غير صحيحة أو غير مكتملة. يرجى تصحيح المدخلات وإعادة المحاولة" }
                });

        public static readonly EnumExceptionErrorMessages ProcessValidationException = new(BaseEnumExceptionErrorMessages.ProcessValidationException, BaseEnumExceptionErrorMessages.ProcessValidationException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The operation could not be completed due to issues in the process. Please contact the system administrator and provide the following log number for further assistance: {$LogNumber$}." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لم تتمكن من الإكمال بسبب مشاكل في العملية. يرجى التواصل مع مسؤول النظام وتزويده برقم السجل التالي لمزيد من المساعدة: {$LogNumber$}" }
                });

        public static readonly EnumExceptionErrorMessages NotFoundException = new(BaseEnumExceptionErrorMessages.NotFoundException, BaseEnumExceptionErrorMessages.NotFoundException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The item you are looking for does not exist. Verify the request and try again. Please contact the system administrator and provide the following log number for further assistance: {$LogNumber$}." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "العنصر الذي تبحث عنه غير موجود. تحقق من الطلب وحاول مرة أخرى. يرجى التواصل مع مسؤول النظام وتزويده برقم السجل التالي لمزيد من المساعدة: {$LogNumber$}" }
                });

        public static readonly EnumExceptionErrorMessages ExistingRecordException = new(BaseEnumExceptionErrorMessages.ExistingRecordException, BaseEnumExceptionErrorMessages.ExistingRecordException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The item you are trying to create or update already exists in the system. Please verify the details and try again." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "العنصر الذي تحاول إنشاؤه أو تحديثه موجود بالفعل في النظام. يرجى التحقق من التفاصيل والمحاولة مرة أخرى" }
                });

        public static readonly EnumExceptionErrorMessages ArgumentNullException = new(BaseEnumExceptionErrorMessages.ArgumentNullException, BaseEnumExceptionErrorMessages.ArgumentNullException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Something went wrong when we tried to initialize @ArgumentName, please contact the administrator and try again later." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "حدث خطأ ما عند محاولتنا تهيئة @ArgumentName، يرجى الاتصال بالمسؤول والمحاولة مرة أخرى لاحقًا" }
                });

        public static readonly EnumExceptionErrorMessages InvalidNullInputException = new(BaseEnumExceptionErrorMessages.InvalidNullInputException, BaseEnumExceptionErrorMessages.InvalidNullInputException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Something went wrong when we tried to get the value of @PropertyName, please contact the administrator and try again later." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "حدث خطأ ما عند محاولتنا تهيئة @PropertyName، يرجى الاتصال بالمسؤول والمحاولة مرة أخرى لاحقًا" }
                });

        public static readonly EnumExceptionErrorMessages BadParameterException = new(BaseEnumExceptionErrorMessages.BadParameterException, BaseEnumExceptionErrorMessages.BadParameterException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Invalid parameter(s) provided. Please check your input." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تم تقديم معلمة غير صالحة. يرجى التحقق من الإدخال." }
                });

        public static readonly EnumExceptionErrorMessages DownloadException = new(BaseEnumExceptionErrorMessages.DownloadException, BaseEnumExceptionErrorMessages.DownloadException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Failed to download. Please try again later" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "فشل في التنزيل. يرجى المحاولة مرة أخرى لاحقاً" }
                });

        public static readonly EnumExceptionErrorMessages NoContentException = new(BaseEnumExceptionErrorMessages.NoContentException, BaseEnumExceptionErrorMessages.NoContentException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The request was successful, but there is no content to display." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تمت معالجة الطلب بنجاح، ولكن لا توجد محتويات لعرضها" }
                });

        public static readonly EnumExceptionErrorMessages ServiceUnavailableException = new(BaseEnumExceptionErrorMessages.ServiceUnavailableException, BaseEnumExceptionErrorMessages.ServiceUnavailableException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The service is temporarily unavailable. Please try again later" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "الخدمة غير متاحة مؤقتاً. يرجى المحاولة مرة أخرى لاحقاً" }
                });

        public static readonly EnumExceptionErrorMessages UploadException = new(BaseEnumExceptionErrorMessages.UploadException, BaseEnumExceptionErrorMessages.UploadException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "File upload failed due to an unexpected issue. Please contact support if the problem persists" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "فشل رفع الملف بسبب مشكلة غير متوقعة. يرجى الاتصال بالدعم إذا استمر المشكلة" }
                });

        public static readonly EnumExceptionErrorMessages SqlErrorException = new(BaseEnumExceptionErrorMessages.SqlErrorException, BaseEnumExceptionErrorMessages.SqlErrorException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Database operation failed due to an unexpected issue. Please contact support if the problem persists" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "فشلت عملية قاعدة البيانات بسبب مشكلة غير متوقعة. يرجى الاتصال بالدعم إذا استمرت المشكلة" }
                });

        public static readonly EnumExceptionErrorMessages SmsException = new(BaseEnumExceptionErrorMessages.SmsException, BaseEnumExceptionErrorMessages.SmsException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Failed to send the SMS due to an unexpected issue, May be the service not available now. Please contact support if the problem persists" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "فشل في إرسال الرسالة النصية بسبب مشكلة غير متوقعة ربما تكون الخدمة غير متوفرة الأن. يرجى الاتصال بالدعم إذا استمرت المشكلة" }
                });
        public static readonly EnumExceptionErrorMessages DecryptionException = new(BaseEnumExceptionErrorMessages.DecryptionException, BaseEnumExceptionErrorMessages.DecryptionException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "An error occurred during the decryption process. Please contact support if the problem persists" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "حدث خطأ أثناء عملية فك التشفير. يرجى الاتصال بالدعم إذا استمرت المشكلة" }
                });
        #endregion

        #region Fluent Validation Exceptions (Code : 101 => 200)

        public static readonly EnumExceptionErrorMessages InputMustBeNotNullOrNotEmpty = new(BaseEnumExceptionErrorMessages.InputMustBeNotNullOrNotEmpty, BaseEnumExceptionErrorMessages.InputMustBeNotNullOrNotEmpty.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "$$PropertyName$$ مطلوب" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "$$PropertyName$$ is Required" }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeBetweenValues = new(BaseEnumExceptionErrorMessages.InputMustBeBetweenValues, BaseEnumExceptionErrorMessages.InputMustBeBetweenValues.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "$$PropertyName$$ لابد ان تكون بين @MinVal و @MaxVal" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "$$PropertyName$$ Must be between @MinVal and @MaxVal" }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeNullOrEmpty = new(BaseEnumExceptionErrorMessages.InputMustBeNullOrEmpty, BaseEnumExceptionErrorMessages.InputMustBeNullOrEmpty.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "$$PropertyName$$ يجب ان تحتوى علي قيمة فارغة" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "$$PropertyName$$ field must be either null or empty" }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeNumericValue = new(BaseEnumExceptionErrorMessages.InputMustBeNumericValue, BaseEnumExceptionErrorMessages.InputMustBeNumericValue.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "$$PropertyName$$ يجب ان تحتوى علي قيمة رقمية صالحة" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "$$PropertyName$$ field must be valid number" }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeLessThanExactValue = new(BaseEnumExceptionErrorMessages.InputMustBeLessThanExactValue, BaseEnumExceptionErrorMessages.InputMustBeLessThanExactValue.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن تكون قيمة $$PropertyName$$ أقل من @ExactValue" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The value of $$PropertyName$$ must be less than @ExactValue." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeLessThanOrEqualExactValue = new(BaseEnumExceptionErrorMessages.InputMustBeLessThanOrEqualExactValue, BaseEnumExceptionErrorMessages.InputMustBeLessThanOrEqualExactValue.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن تكون قيمة $$PropertyName$$ أقل من أو تساوى @ExactValue" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The value of $$PropertyName$$ must be less than or equal @ExactValue." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeGreaterThanExactValue = new(BaseEnumExceptionErrorMessages.InputMustBeGreaterThanExactValue, BaseEnumExceptionErrorMessages.InputMustBeGreaterThanExactValue.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن تكون قيمة $$PropertyName$$ أكبر من @ExactValue" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The value of $$PropertyName$$ must be Greater than @ExactValue." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeGreaterThanOrEqualExactValue = new(BaseEnumExceptionErrorMessages.InputMustBeGreaterThanOrEqualExactValue, BaseEnumExceptionErrorMessages.InputMustBeGreaterThanOrEqualExactValue.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن تكون قيمة $$PropertyName$$ أكبر من أو تساوى @ExactValue" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The value of $$PropertyName$$ must be Greater than or equal @ExactValue." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeHasExactLength = new(BaseEnumExceptionErrorMessages.InputMustBeHasExactLength, BaseEnumExceptionErrorMessages.InputMustBeHasExactLength.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن يكون $$PropertyName$$ بطول @Length حرفاً" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The input $$PropertyName$$ must be exactly @Length characters long." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeHasMinLength = new(BaseEnumExceptionErrorMessages.InputMustBeHasMinLength, BaseEnumExceptionErrorMessages.InputMustBeHasMinLength.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن يكون $$PropertyName$$ بطول @Length حرفاً أدنى أقصى" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The input $$PropertyName$$ must be @Length characters minimum." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeHasMaxLength = new(BaseEnumExceptionErrorMessages.InputMustBeHasMaxLength, BaseEnumExceptionErrorMessages.InputMustBeHasMaxLength.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن يكون $$PropertyName$$ بطول @Length حرفاً كحدى أقصى" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The input $$PropertyName$$ must be  @Length characters maximum." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeEqualExactValue = new(BaseEnumExceptionErrorMessages.InputMustBeEqualExactValue, BaseEnumExceptionErrorMessages.InputMustBeEqualExactValue.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن تكون قيمة $$PropertyName$$ تساوى @ExactValue" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The value of $$PropertyName$$ must be equal @ExactValue." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeNotEqualExactValue = new(BaseEnumExceptionErrorMessages.InputMustBeNotEqualExactValue, BaseEnumExceptionErrorMessages.InputMustBeNotEqualExactValue.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن تكون قيمة $$PropertyName$$ لا تساوى @ExactValue" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The value of $$PropertyName$$ must be not equal @ExactValue." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeAfterTargetDate = new(BaseEnumExceptionErrorMessages.InputMustBeAfterTargetDate, BaseEnumExceptionErrorMessages.InputMustBeAfterTargetDate.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يرجى إدخال تاريخ لـ $$PropertyName$$ بعد @TargetDate" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Please enter a date for $$PropertyName$$ that is after @TargetDate" }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeValidIsoDateFormat = new(BaseEnumExceptionErrorMessages.InputMustBeValidIsoDateFormat, BaseEnumExceptionErrorMessages.InputMustBeValidIsoDateFormat.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن يكون تاريخ $$PropertyName$$ بتنسيق ISO صالح." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The date of $$PropertyName$$ must be in a valid ISO format." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeValidCreditCard = new(BaseEnumExceptionErrorMessages.InputMustBeValidCreditCard, BaseEnumExceptionErrorMessages.InputMustBeValidCreditCard.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "$$PropertyName$$ يجب أن تكون قيمة رقم بطاقة ائتمان صالحاً" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The input $$PropertyName$$ must be a valid credit card number." }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeValidEmail = new(BaseEnumExceptionErrorMessages.InputMustBeValidEmail, BaseEnumExceptionErrorMessages.InputMustBeValidEmail.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "$$PropertyName$$ يجب أن تكون قيمة الايميل صالحاً" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The input $$PropertyName$$ must be a valid email" }
                });

        public static readonly EnumExceptionErrorMessages InputMustBeNotExistBefore = new(BaseEnumExceptionErrorMessages.InputMustBeNotExistBefore, BaseEnumExceptionErrorMessages.InputMustBeNotExistBefore.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "The value of $$PropertyName$$ already exist." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "يجب ألا يكون قيمة $$PropertyName$$ موجودة مسبقاً." }
                });

        public static readonly EnumExceptionErrorMessages InputValueMustBeExistsInEnum = new(BaseEnumExceptionErrorMessages.InputValueMustBeExistsInEnum, BaseEnumExceptionErrorMessages.InputValueMustBeExistsInEnum.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "'$$PropertyName$$ has a range of values which does not include entered value" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "$$PropertyName$$ يحتوي على مجموعة من القيم التي لا تتضمن القيمة المدخلة" }
                });

        public static readonly EnumExceptionErrorMessages HaveAtLeastOneValue = new(BaseEnumExceptionErrorMessages.HaveAtLeastOneValue, BaseEnumExceptionErrorMessages.HaveAtLeastOneValue.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "At least one property must have a value." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "يجب أن يحتوي حقل واحد على الأقل على قيمة." }
                });
        public static readonly EnumExceptionErrorMessages AtLeastTwoRecordsRequired = new(BaseEnumExceptionErrorMessages.InputListHaveAtLeastTwoRecordsRequired, BaseEnumExceptionErrorMessages.InputListHaveAtLeastTwoRecordsRequired.ToString(), new List<ErrorsLang>()
                {
                new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب أن تحتوي القائمة على سجلين على الأقل إما لموظف أو شركة أو وكالة محلية." },
                new () { LangCode = EnumLanguages.English.LangCode, Message = "The list must contain at least two records of either Employee, Company, or Domestic Agency." }
                });
        public static readonly EnumExceptionErrorMessages AtLeastOneInputValidation = new(BaseEnumExceptionErrorMessages.InputValuesHaveAtLeastOneInput, BaseEnumExceptionErrorMessages.InputValuesHaveAtLeastOneInput.ToString(), messages: new List<ErrorsLang>()
        {
            new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب توفير قيمة لواحد على الأقل من الخصائص المدخلة." },
            new () { LangCode = EnumLanguages.English.LangCode, Message = "At least one of the input properties must be provided." }
        });

        public static readonly EnumExceptionErrorMessages InvalidCompanySignature = new(BaseEnumExceptionErrorMessages.CompanySignatureAreInvalid, BaseEnumExceptionErrorMessages.CompanySignatureAreInvalid.ToString(), messages: new List<ErrorsLang>()
        {
            new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "معرّف الشركة غير صالح." },
            new () { LangCode = EnumLanguages.English.LangCode, Message = "Company identification code is invalid." }
        });

        public static readonly EnumExceptionErrorMessages InvalidPersonSignature = new(BaseEnumExceptionErrorMessages.PersonSignatureAreInvalid, appErrorKeyName: "PersonSignatureAreInvalid", messages: new List<ErrorsLang>()
        {
            new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "معرّف المستخدم غير صالح." },
            new () { LangCode = EnumLanguages.English.LangCode, Message = "Person identification code is invalid." }
        });

        public static readonly EnumExceptionErrorMessages EmployeePersonCodeNotExists = new(BaseEnumExceptionErrorMessages.EmployeePersonCodeNotExists, appErrorKeyName: "EmployeePersonCodeNotExists", messages: new List<ErrorsLang>()
        {
            new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "رمز الموظف الشخصي غير موجود." },
            new () { LangCode = EnumLanguages.English.LangCode, Message = "Employee Person Code Not Exists." }
        });
        public static readonly EnumExceptionErrorMessages RepresentatorPersonCodeNotExists = new(BaseEnumExceptionErrorMessages.RepresentatorPersonCodeNotExists, appErrorKeyName: "RepresentatorPersonCodeNotExists", messages: new List<ErrorsLang>()
        {
            new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "رمز شخص الممثل غير موجود."},
            new () { LangCode = EnumLanguages.English.LangCode, Message = "Representator Person Code Not Exists." }
        });
        public static readonly EnumExceptionErrorMessages CompanyCodeNotExists = new(BaseEnumExceptionErrorMessages.CompanyCodeNotExists, appErrorKeyName: "CompanyCodeNotExists", messages: new List<ErrorsLang>()
        {
            new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "رمز الشركة غير موجود."},
            new () { LangCode = EnumLanguages.English.LangCode, Message = "Company Code Not Exists." }
        });
        public static readonly EnumExceptionErrorMessages ApplicantMustBeEitherEmployeeOrCompany = new(BaseEnumExceptionErrorMessages.InputValueMustBeExistsInEnum, BaseEnumExceptionErrorMessages.InputValueMustBeExistsInEnum.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "'$$PropertyName$$ must be either an Employee or Company" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "$$PropertyName$$ يجب أن يكون إما موظفًا أو شركة" }
                });
        #endregion

        #region Authentication / UaePass / Access Token / Redis Server / General Services Exceptions (Code : 201 => 300)

        public static readonly EnumExceptionErrorMessages AuthenticationFailedException = new(BaseEnumExceptionErrorMessages.AuthenticationFailedException, BaseEnumExceptionErrorMessages.AuthenticationFailedException.ToString(), new List<ErrorsLang>
            {
                new () { LangCode = EnumLanguages.English.LangCode, Message = "Authentication failed: The credentials you provided are incorrect or your account may be locked. Please verify your credentials and try again. If the issue persists, contact support." },
                new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "فشلت المصادقة: بيانات الاعتماد التي قدمتها غير صحيحة أو قد يكون حسابك مقفلاً. يرجى التحقق من بياناتك والمحاولة مرة أخرى. إذا استمرت المشكلة، يرجى الاتصال بالدعم." }
            });

        public static readonly EnumExceptionErrorMessages UaePassSop1UserException = new(BaseEnumExceptionErrorMessages.UaePassSop1UserException, BaseEnumExceptionErrorMessages.UaePassSop1UserException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "You are not eligible to access this service. Your account is either not upgraded or you have a visitor account. Please contact 'Demo Test App' to access the services." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "أنت غير مؤهل للوصول إلى هذه الخدمة. إما أن حسابك لم تتم ترقيته أو لديك حساب زائر. يرجى الاتصال بوزارة الموارد البشرية والتوطين لتتمكن من الوصول إلى الخدمة" }
                });

        public static readonly EnumExceptionErrorMessages UAEPassGeneralMessageException = new(BaseEnumExceptionErrorMessages.UAEPassGeneralMessageException, BaseEnumExceptionErrorMessages.UAEPassGeneralMessageException.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "An error occurred during login, please try again later!" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "حدث خطأ ما أثناء تسجيل الدخول، يرجى المحاولة مرة أخرى لاحقًا!" }
                });

        public static readonly EnumExceptionErrorMessages AccessTokenNotValid = new(BaseEnumExceptionErrorMessages.AccessTokenNotValid, BaseEnumExceptionErrorMessages.AccessTokenNotValid.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Access Token is not valid." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "رمز الوصول غير صالح" }
                });

        public static readonly EnumExceptionErrorMessages RefreshTokenExpired = new(BaseEnumExceptionErrorMessages.RefreshTokenExpired, BaseEnumExceptionErrorMessages.RefreshTokenExpired.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Refresh Token is Expired." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "انتهت صلاحية رمز التحديث" }
                });

        public static readonly EnumExceptionErrorMessages RedisConnectionNotAvailable = new(BaseEnumExceptionErrorMessages.RedisConnectionNotAvailable, BaseEnumExceptionErrorMessages.RedisConnectionNotAvailable.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Redis connection failed due to an unexpected issue. Please contact support if the problem persists." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "فشل الاتصال بـ Redis بسبب مشكلة غير متوقعة. يرجى الاتصال بالدعم إذا استمرت المشكلة" }
                });

        #endregion

        #region App User Exceptions (Code : 301 => 800)

        public static readonly EnumExceptionErrorMessages AppUserNotExistInSystem = new(BaseEnumExceptionErrorMessages.AppUserNotExistInSystem, BaseEnumExceptionErrorMessages.AppUserNotExistInSystem.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "User data is not presented in the system." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "بيانات المستخدم غير موجودة بالنظام" }
                });

        public static readonly EnumExceptionErrorMessages AppUserExistInSystem = new(BaseEnumExceptionErrorMessages.AppUserExistInSystem, BaseEnumExceptionErrorMessages.AppUserExistInSystem.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "User already exists in the system." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "المستخدم موجود بالفعل في النظام" }
                });

        public static readonly EnumExceptionErrorMessages AppUserMissingInformation = new(BaseEnumExceptionErrorMessages.AppUserMissingInformation, BaseEnumExceptionErrorMessages.AppUserMissingInformation.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "User profile is missing important and mandatory information. Please ask the admin to update your profile." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "ملف تعريف المستخدم يفتقد إلى معلومات مهمة وإلزامية. يرجى مطالبة المسؤول بتحديث ملفك الشخصي" }
                });

        public static readonly EnumExceptionErrorMessages AppuserInvalidCredentials = new(BaseEnumExceptionErrorMessages.AppuserInvalidCredentials, BaseEnumExceptionErrorMessages.AppuserInvalidCredentials.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Invalid login credentials." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "بيانات الدخول غير صحيحة" }
                });

        public static readonly EnumExceptionErrorMessages AppuserDuplicatedInSystem = new(BaseEnumExceptionErrorMessages.AppuserDuplicatedInSystem, BaseEnumExceptionErrorMessages.AppuserDuplicatedInSystem.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Invalid login credentials." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "بيانات الدخول غير صحيحة" }
                });

        public static readonly EnumExceptionErrorMessages CanNotUpsertAppUserEmirateAccess = new(BaseEnumExceptionErrorMessages.CanNotUpsertAppUserEmirateAccess, BaseEnumExceptionErrorMessages.CanNotUpsertAppUserEmirateAccess.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Ops, Something went wrong, Can't Update Application User Emirate Access" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "عفواً، حدث خطأ ما، لا يمكن تحديث صلاحية وصول المستخدم للامارة" }
                });

        #endregion

        #region Notifications Exceptions (Code : 801 => 1000)
        public static readonly EnumExceptionErrorMessages NotificationTemplateNotExist = new(BaseEnumExceptionErrorMessages.NotificationTemplateNotExist, BaseEnumExceptionErrorMessages.NotificationTemplateNotExist.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Notification Template Not Exist in the system" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "الإشعار غير موجود بالنظام" }
                });

        public static readonly EnumExceptionErrorMessages OTPGeneratedAndActive = new(BaseEnumExceptionErrorMessages.OTPGeneratedAndActive, BaseEnumExceptionErrorMessages.OTPGeneratedAndActive.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "An OTP has already been generated and is currently active. Please wait until it expires before requesting a new one" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تم توليد رمز التحقق بالفعل وهو نشط حالياً. يرجى الانتظار حتى انتهاء صلاحيته قبل طلب رمز جديد" }
                });

        public static readonly EnumExceptionErrorMessages OTPIsInvalid = new(BaseEnumExceptionErrorMessages.OTPIsInvalid, BaseEnumExceptionErrorMessages.OTPIsInvalid.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The OTP you entered is incorrect. Please check and try again." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "رمز التحقق الذي أدخلته غير صحيح. يرجى التحقق والمحاولة مرة أخرى" }
                });

        public static readonly EnumExceptionErrorMessages OTPIsExpired = new(BaseEnumExceptionErrorMessages.OTPIsExpired, BaseEnumExceptionErrorMessages.OTPIsExpired.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Your OTP has expired. Please generate a new OTP and try again" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "رمز التحقق الخاص بك منتهي الصلاحية. يرجى توليد رمز جديد والمحاولة مرة أخرى" }
                });

        #endregion

        #region Multiple Dependents Validations error message (Code : 1000 => 1200)
        public static readonly EnumExceptionErrorMessages MultipleDepenedentsInputValidation = new(BaseEnumExceptionErrorMessages.MultipleDepenedentsInputValidation, appErrorKeyName: "MultipleDepenedentsInputValidation", messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب توفير رقم موحد، أو رقم جواز السفر والجنسية، أو الاسم والجنس والجنسية وتاريخ الميلاد." },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "Either Unified Number must be provided, or both Passport No and Nationality, or all of Name, Gender, Nationality, and DOB must be provided." }
          });
        public static readonly EnumExceptionErrorMessages BothInputsAreMandatoryValidation = new(BaseEnumExceptionErrorMessages.BothInputsAreMandatoryValidation, appErrorKeyName: "MultipleDepenedentsInputValidation", messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message ="يجب توفير كلا خصائص الإدخال." },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "Both input properties must be provided." }
          });
        public static readonly EnumExceptionErrorMessages MultipleDepenedentsEmployeeSearchInputValidation = new(BaseEnumExceptionErrorMessages.MultipleDepenedentsEmployeeSearchInputValidation, appErrorKeyName: "MultipleDepenedentsInputValidation", messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يجب توفير رقم الشركة، أو يجب توفير رمز الشخص، أو يجب توفير رقم بطاقة العمل، أو يجب توفير كل من رقم جواز السفر والجنسية، أو يجب توفير الاسم والجنس والجنسية وتاريخ الميلاد بالكامل" },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "Either Company Number must be provided, or person code must be provided, or Labour Card Number must be provided, or both Passport No and Nationality, or all of Name, Gender, Nationality, and DOB must be provided." }
          });
        public static readonly EnumExceptionErrorMessages AllInputsAreMandatoryValidation = new(BaseEnumExceptionErrorMessages.AllInputsAreMandatoryValidation, appErrorKeyName: "MultipleDepenedentsInputValidation", messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message ="يجب توفير كافة خصائص الإدخال." },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "All input properties must be provided." }
          });
        #endregion

        #region Person Validation Exceptions (Code : 1000 => 1100)

        public static readonly EnumExceptionErrorMessages InvalidLabourEmployeeSearchCriteria = new(BaseEnumExceptionErrorMessages.InvalidLabourEmployeeSearchCriteria, BaseEnumExceptionErrorMessages.InvalidLabourEmployeeSearchCriteria.ToString(), messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يمكن البحث بـ : الرقم الموحد أو رقم الهوية الرقمية أو رقم بطاقة العمل أو رمز الشخص أو رقم الجواز + الجنسية أو اسم الشخص + تاريخ الميلاد + النوع + الجنسية" },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "You can search using : Unified Number OR Emirates ID Number OR Labour Card Number OR Person Code OR Passport Number + Nationality OR Person Name + Date of Birth + Gender + Nationality" }
         });

        public static readonly EnumExceptionErrorMessages InvalidDomesticEmployeeSearchCriteria = new(BaseEnumExceptionErrorMessages.InvalidDomesticEmployeeSearchCriteria, BaseEnumExceptionErrorMessages.InvalidDomesticEmployeeSearchCriteria.ToString(), messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يمكن البحث بـ : الرقم الموحد أو رقم الهوية الرقمية أو رقم الجواز + الجنسية أو اسم الشخص + تاريخ الميلاد + النوع + الجنسية" },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "You can search using : Unified Number OR Emirates ID Number OR Passport Number + Nationality OR Person Name + Date of Birth + Gender + Nationality" }
         });

        public static readonly EnumExceptionErrorMessages InvalidDomesticSponsorSearchCriteria = new(BaseEnumExceptionErrorMessages.InvalidDomesticSponsorSearchCriteria, BaseEnumExceptionErrorMessages.InvalidDomesticSponsorSearchCriteria.ToString(), messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يمكن البحث بـ : الرقم الموحد أو رقم الهوية الرقمية أو رقم الجواز + الجنسية أو اسم الشخص + تاريخ الميلاد + النوع + الجنسية" },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "You can search using : Unified Number OR Emirates ID Number OR Passport Number + Nationality OR Person Name + Date of Birth + Gender + Nationality" }
         });

        public static readonly EnumExceptionErrorMessages InvalidPassportSearchCriteria = new(BaseEnumExceptionErrorMessages.InvalidPassportSearchCriteria, BaseEnumExceptionErrorMessages.InvalidPassportSearchCriteria.ToString(), messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لابد من توفير رقم الجواز + الجنسية" },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "You must provide Passport Number + Nationality" }
         });

        public static readonly EnumExceptionErrorMessages InvalidPersonalInformationSearchCriteria = new(BaseEnumExceptionErrorMessages.InvalidPersonalInformationSearchCriteria, BaseEnumExceptionErrorMessages.InvalidPersonalInformationSearchCriteria.ToString(), messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لابد من توفير اسم الشخص + تاريخ الميلاد + النوع + الجنسية" },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "You must provide Person Name + Date of Birth + Gender + Nationality" }
         });

        public static readonly EnumExceptionErrorMessages InvalidICPSearchCriteria = new(BaseEnumExceptionErrorMessages.InvalidICPSearchCriteria, BaseEnumExceptionErrorMessages.InvalidICPSearchCriteria.ToString(), messages: new List<ErrorsLang>()
         {
              new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لابد من توفير الرقم الموحد أو رقم الهوية الرقمية" },
              new () { LangCode = EnumLanguages.English.LangCode, Message = "You must provide Unified Number OR Emirates ID Number" }
         });
        #endregion

        #region Demo Validation Exceptions (Code: 1101 => 1200)
        public static readonly EnumExceptionErrorMessages LabourOfficeNotAllowed = new(BaseEnumExceptionErrorMessages.LabourOfficeNotAllowed, BaseEnumExceptionErrorMessages.LabourOfficeNotAllowed.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = ".لا يمكن تسجيل الشكاوى من مكتب العمل هذا عبر نظام مركز الاتصال" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Demos from this labour office cannot be registered through Call Center." }
                });
        public static readonly EnumExceptionErrorMessages DemoAlreadyRegisteredAgainstEmployer = new(BaseEnumExceptionErrorMessages.DemoAlreadyRegisteredAgainstEmployer, BaseEnumExceptionErrorMessages.DemoAlreadyRegisteredAgainstEmployer.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = ".لقد تم تسجيل شكوى  ضد هذه المنشأة مسبقاً, يجب انهاء الشكوى التي قيد الاجراء قبل تسجيل شكوى جديدة" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Already a Demo is registered against this employer which is in process.You are not allowed to register new Demo until the Demo is closed by ministry." }
                });
        public static readonly EnumExceptionErrorMessages DemoAlreadyRegisteredAgainstEmployee = new(BaseEnumExceptionErrorMessages.DemoAlreadyRegisteredAgainstEmployee, BaseEnumExceptionErrorMessages.DemoAlreadyRegisteredAgainstEmployee.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = ".لقد تم تسجيل شكوى  ضد هذه الموظف مسبقاً, يجب انهاء الشكوى التي قيد الاجراء قبل تسجيل شكوى جديدة" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Already a Demo is registered against this employee which is in process.You are not allowed to register new Demo until the Demo is closed by ministry." }
                });
        public static readonly EnumExceptionErrorMessages DemoAmnestyExemption = new(BaseEnumExceptionErrorMessages.DemoAmnestyExemption, BaseEnumExceptionErrorMessages.DemoAmnestyExemption.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "في حال رغبتك في تعديل وضعك بالدولة يتوجب على صاحب العمل الجديد تقديم تصريح عمل جديد وسيتم الغاء شكوى الانقطاع عن العمل بشكل الي. اما في حال رغبتك بمغادرة الدولة يمكنك التقدم بطلب تصريح مغادرة من خلال قنوات الخدمة في الهيئة الاتحادية للهوية والجنسية والجمارك وامن المنافذ. خلال فترة مهلة تسوية الاوضاع" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "If you wish to change your status in the country, the new employer must submit a new work permit, and the Demo about interruption from work will be canceled automatically. If you wish to leave the country, you can apply for an exit permit through the service channels of the Federal Authority for Identity and Citizenship, Customs, and Port Security during the status settlement period." }
                });
        public static readonly EnumExceptionErrorMessages UnableToRegisterLaborDemo = new(BaseEnumExceptionErrorMessages.UnableToRegisterLaborDemo, BaseEnumExceptionErrorMessages.UnableToRegisterLaborDemo.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لا يمكن التسجيل" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Unable to register" }
                });

        public static readonly EnumExceptionErrorMessages UnableToRegisterDemo = new(BaseEnumExceptionErrorMessages.UnableToRegisterDemo, BaseEnumExceptionErrorMessages.UnableToRegisterDemo.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لا يمكن التسجيل" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Unable to register" }
                });
        public static readonly EnumExceptionErrorMessages UnableToRegisterDemoHistory = new(BaseEnumExceptionErrorMessages.UnableToRegisterDemoHistory, BaseEnumExceptionErrorMessages.UnableToRegisterDemoHistory.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "غير قادر على تسجيل سجل الشكوى." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Unable to register Demo history." }
                });
        public static readonly EnumExceptionErrorMessages UnableToCreateDemoReferenceNumber = new(BaseEnumExceptionErrorMessages.UnableToCreateDemoReferenceNumber, BaseEnumExceptionErrorMessages.UnableToCreateDemoReferenceNumber.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "غير قادر على إنشاء رقم مرجعي للشكوى" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Unable to Create Demo Reference Number" }
                });

        public static readonly EnumExceptionErrorMessages UnableToRegisterDemoDetails = new(BaseEnumExceptionErrorMessages.UnableToRegisterDemoDetails, BaseEnumExceptionErrorMessages.UnableToRegisterDemoDetails.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يرجى التحقق من تفاصيل شكواك، غير قادر على التسجيل" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Please verify your Demo Details, Unable to register" }
                });
        //
        public static readonly EnumExceptionErrorMessages UnableToRegisterDemoDueToDemoContractDetails = new(BaseEnumExceptionErrorMessages.UnableToRegisterDemoDueToDemoContractDetails, BaseEnumExceptionErrorMessages.UnableToRegisterDemoDueToDemoContractDetails.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يرجى التحقق من تفاصيل عقد الشكوى، غير قادر على التسجيل" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Please verify your Demo Contract Details, Unable to register" }
                });
        public static readonly EnumExceptionErrorMessages UnableToRegisterDemoDueToClaims = new(BaseEnumExceptionErrorMessages.UnableToRegisterDemoDueToClaims, BaseEnumExceptionErrorMessages.UnableToRegisterDemoDueToClaims.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يرجى التحقق من مطالباتك، غير قادر على التسجيل" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Please verify your claims, Unable to register " }
                });
        public static readonly EnumExceptionErrorMessages UnableToRegisterDemoDueToDemoPartyData = new(BaseEnumExceptionErrorMessages.UnableToRegisterDemoDueToDemoParties, BaseEnumExceptionErrorMessages.UnableToRegisterDemoDueToDemoParties.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يرجى التحقق من معلومات الأطراف الخاصة بك، غير قادر على التسجيل" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Please verify your parties information , Unable to register" }
                });
        public static readonly EnumExceptionErrorMessages InvalidIdentitySignature = new(BaseEnumExceptionErrorMessages.InvalidIdentitySignature, BaseEnumExceptionErrorMessages.InvalidIdentitySignature.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "التوقيع غير صالح" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Invalid Signature" }
                });

        public static readonly EnumExceptionErrorMessages UnableToRegisterDomesticWorkerDemo = new(BaseEnumExceptionErrorMessages.UnableToRegisterDomesticWorkerDemo, BaseEnumExceptionErrorMessages.UnableToRegisterDomesticWorkerDemo.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "غير قادر على تقديم شكوى بخصوص عاملة منزلية." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Unable to submit a Demo for a Domestic Worker." }
                });
        //
        public static readonly EnumExceptionErrorMessages AnotherDemoExistsForSameParty = new(BaseEnumExceptionErrorMessages.AnotherDemoExistsForSameParty, BaseEnumExceptionErrorMessages.AnotherDemoExistsForSameParty.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "توجد شكوى  على نفس المشكو ضده وسيتم إعطاء الشاكي نفس الموعد المحدد مسبقاً توجد شكوى على نفس المشكو ضده وسيتم إعطاء الشاكي نفس الموعد المحدد مسبقاً للشاكين" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "There is another Demo against the same party, So same appointment will be given to complainer as being assigned previously." }
                });
        public static readonly EnumExceptionErrorMessages EmployeeNotExists = new(BaseEnumExceptionErrorMessages.EmployeeNotExists, BaseEnumExceptionErrorMessages.EmployeeNotExists.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = ".الموظف غير موجود مقابل رقم بطاقة الموظف المقدم" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Employee not exists for provided employee card number." }
                });
        public static readonly EnumExceptionErrorMessages SponsorNotExists = new(BaseEnumExceptionErrorMessages.SponsorNotExists, BaseEnumExceptionErrorMessages.EmployeeNotExists.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "التأكد من إدخال الرقم الموحد للكفيل بشكل صحيح دون أي أخطاء" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Ensure that the sponsor unified number is entered correctly without any mistakes." }
                });
        public static readonly EnumExceptionErrorMessages RepresentatorNotExists = new(BaseEnumExceptionErrorMessages.RepresentatorNotExists, BaseEnumExceptionErrorMessages.RepresentatorNotExists.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "الممثل غير موجود."},
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Representator Not Exists." }
                });
        public static readonly EnumExceptionErrorMessages DomesticWorkerNotExists = new(BaseEnumExceptionErrorMessages.DomesticWorkerNotExists, BaseEnumExceptionErrorMessages.EmployeeNotExists.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "التأكد من إدخال الرقم الموحد للعمالة المنزلية بشكل صحيح دون أي أخطاء" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Ensure that the Domestic Worker unified number is entered correctly without any mistakes." }
                });

        public static readonly EnumExceptionErrorMessages ServiceCentredNotExists = new(BaseEnumExceptionErrorMessages.ServiceCentredNotExists, BaseEnumExceptionErrorMessages.EmployeeNotExists.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تأكد من صحة عنوان هوية الإمارات ومعرف المدينة لمعرف مركز الخدمة." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Verify that the Address emirates ID address and City ID are correct for ServiceCentred." }
                });
        public static readonly EnumExceptionErrorMessages CompanyNotExists = new(BaseEnumExceptionErrorMessages.CompanyNotExists, BaseEnumExceptionErrorMessages.CompanyNotExists.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = ".الشركة غير موجودة وفقًا لرمز الشركة المقدم" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Company not exists company code." }
                });
        public static readonly EnumExceptionErrorMessages UnifiedNumberRequired = new(BaseEnumExceptionErrorMessages.UnifiedNumberRequired, BaseEnumExceptionErrorMessages.UnifiedNumberRequired.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "الرقم الموحد مطلوب" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Unified Number is required." }
                });
        public static readonly EnumExceptionErrorMessages InvalidComplaintPartiesCount = new(BaseEnumExceptionErrorMessages.InvalidComplaintPartiesCount, BaseEnumExceptionErrorMessages.InvalidComplaintPartiesCount.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "Invalid complaint parties count. The number of complaint parties must be greater than or equal to Two." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "عدد الأطراف المرتبطة بالشكوى غير صالح. يجب أن يكون عدد الأطراف أكبر من أو يساوي اثنان " }
                });
        public static readonly EnumExceptionErrorMessages InvalidComplaintClaimsCount = new(BaseEnumExceptionErrorMessages.InvalidComplaintClaimsCount, BaseEnumExceptionErrorMessages.InvalidComplaintClaimsCount.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "Invalid complaint claims count. The number of claims must be greater than or equal to One." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "عدد المطالبات في الشكوى غير صالح. يجب أن يكون عدد المطالبات أكبر من أو يساوي واحد" }
                });

        public static readonly EnumExceptionErrorMessages ParseIdentitySignatureIsNull = new(BaseEnumExceptionErrorMessages.ParseIdentitySignatureIsNull, BaseEnumExceptionErrorMessages.ParseIdentitySignatureIsNull.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "توقيع الهوية الذي تم تحليله فارغة" },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "The parsed identity signature is null." }
                });
        public static readonly EnumExceptionErrorMessages InvalidDemoPartiesCount = new(BaseEnumExceptionErrorMessages.InvalidDemoPartiesCount, BaseEnumExceptionErrorMessages.InvalidDemoPartiesCount.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "Invalid Demo parties count. The number of Demo parties must be greater than or equal to Two." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "عدد الأطراف المرتبطة بالشكوى غير صالح. يجب أن يكون عدد الأطراف أكبر من أو يساوي اثنان " }
                });
        public static readonly EnumExceptionErrorMessages InvalidDemoPartyType = new(BaseEnumExceptionErrorMessages.InvalidDemoPartyType, BaseEnumExceptionErrorMessages.InvalidDemoPartyType.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "Invalid or Missing Demo Party Types." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "انواع الأطراف المرتبطة بالشكوى او غير مكتمل غير صحيح." }
                });
        public static readonly EnumExceptionErrorMessages InvalidDemoClaimsCount = new(BaseEnumExceptionErrorMessages.InvalidDemoClaimsCount, BaseEnumExceptionErrorMessages.InvalidDemoClaimsCount.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "Invalid Demo claims count. The number of claims must be greater than or equal to One." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "عدد المطالبات في الشكوى غير صالح. يجب أن يكون عدد المطالبات أكبر من أو يساوي واحد" }
                });
        public static readonly EnumExceptionErrorMessages InvalidDemoType = new(BaseEnumExceptionErrorMessages.InvalidDemoType, BaseEnumExceptionErrorMessages.InvalidDemoType.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "The Demo type provided is invalid. Please select a valid Demo type." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "نوع الشكوى المحدد غير صالح. يرجى اختيار نوع شكوى صحيح" }
                });
        public static readonly EnumExceptionErrorMessages FileUploadLinkExpired = new(BaseEnumExceptionErrorMessages.FileUploadLinkExpired, BaseEnumExceptionErrorMessages.FileUploadLinkExpired.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "File upload link is expired." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "رابط رفع الملف منتهي الصلاحية." }
                });
        public static readonly EnumExceptionErrorMessages UnableToUploadFile = new(BaseEnumExceptionErrorMessages.UnableToUploadFile, BaseEnumExceptionErrorMessages.UnableToUploadFile.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Error occurred in uploading file." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "حدث خطأ أثناء تحميل الملف." }
                });
        public static readonly EnumExceptionErrorMessages DemoReferenceIsRequired = new(BaseEnumExceptionErrorMessages.DemoReferenceIsRequired, BaseEnumExceptionErrorMessages.DemoReferenceIsRequired.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Demo Reference is required." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "الرجاء إدخال مرجع الشكوى." }
                });
        public static readonly EnumExceptionErrorMessages RecordNotExistsForFileUrlGuid = new(BaseEnumExceptionErrorMessages.RecordNotExistsForFileUrlGuid, BaseEnumExceptionErrorMessages.RecordNotExistsForFileUrlGuid.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "No record exists for provided file url guid." },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لا يوجد سجل للمعرف GUID لعنوان URL الملف المقدم." }
                });
        public static readonly EnumExceptionErrorMessages AlreadyCaseInCourtEmployee = new(BaseEnumExceptionErrorMessages.AlreadyCaseInCourtEmployee, BaseEnumExceptionErrorMessages.AlreadyCaseInCourtEmployee.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Already a Demo is transferred to the court against this employer.You cannot register the new Demo until the case is closed in court" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تم بالفعل تحويل شكوى إلى المحكمة ضد هذا صاحب العمل. لا يمكنك تسجيل الشكوى الجديدة حتى يتم إغلاق القضية في المحكمة" }
                });
        public static readonly EnumExceptionErrorMessages AlreadyFinalDecisionEmployee = new(BaseEnumExceptionErrorMessages.AlreadyFinalDecisionEmployee, BaseEnumExceptionErrorMessages.AlreadyFinalDecisionEmployee.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Already a Demo is in Final Decision against this employer.You cannot register the new Demo" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "هناك بالفعل شكوى في القرار النهائي ضد صاحب العمل هذا. لا يمكنك تسجيل شكوى جديدة" }
                });
        public static readonly EnumExceptionErrorMessages DemoAlreadyClosedAgainstEmployer = new(BaseEnumExceptionErrorMessages.DemoAlreadyClosedAgainstEmployer, BaseEnumExceptionErrorMessages.DemoAlreadyClosedAgainstEmployer.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لقد تم بالفعل إغلاق الشكوى ضد هذا صاحب العمل." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Already a Demo has been closed against this employer." }
                });
        public static readonly EnumExceptionErrorMessages DemoUnderProcessByOtherUser = new(BaseEnumExceptionErrorMessages.DemoUnderProcessByOtherUser, BaseEnumExceptionErrorMessages.DemoUnderProcessByOtherUser.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تم بالفعل تسجيل شكوى ضد صاحب العمل هذا والتي جاري معالجتها من قبل مستخدم آخر. لا يُسمح لك بالتسجيل أو إعادة تنشيط الشكوى حتى يتم إغلاق الشكوى." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Already a Demo is registered against this employer which is in process by other user.You are not allowed to register or reactivate the Demo until the Demo is closed." }
                });
        public static readonly EnumExceptionErrorMessages ProvideUAENationalDetails = new(BaseEnumExceptionErrorMessages.ProvideUAENationalDetails, BaseEnumExceptionErrorMessages.ProvideUAENationalDetails.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يرجى تقديم التفاصيل المتعلقة بالمواطن الإماراتي." },
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Please provide the details related to the UAE National." }
                });
        public static readonly EnumExceptionErrorMessages AlreadyDemoInDraft = new(BaseEnumExceptionErrorMessages.AlreadyDemoInDraft, BaseEnumExceptionErrorMessages.AlreadyDemoInDraft.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "يوجد بالفعل مسودة شكوى ضد هذا الشخص في مركز الاتصال، يرجى استكمال مسودة الشكوى"},
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "There is already a draft Demo against this person in Call Centre, Please complete the draft Demo" }
                });
        public static readonly EnumExceptionErrorMessages AlreadyRegisteredUAEPublic = new(BaseEnumExceptionErrorMessages.AlreadyRegisteredUAEPublic, BaseEnumExceptionErrorMessages.AlreadyRegisteredUAEPublic.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Already a Demo is registered for this EIDA number which is in progress. You are not allowed to register a new Demo  until it is closed" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تم تسجيل شكوى بالفعل لهذا الرقم EIDA وهو قيد المعالجة. لا يجوز لك تسجيل شكوى جديدة حتى يتم إغلاقها" }
                });
        public static readonly EnumExceptionErrorMessages AlreadyCaseInCourtForPerson = new(BaseEnumExceptionErrorMessages.AlreadyCaseInCourtForPerson, BaseEnumExceptionErrorMessages.AlreadyCaseInCourtForPerson.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Already a court case registered against this person. You cannot register new Demo until the case was settled in the court" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "تم تسجيل دعوى قضائية ضد هذا الشخص بالفعل. لا يمكنك تسجيل شكوى جديدة حتى يتم تسوية القضية في المحكمة" }
                });
        public static readonly EnumExceptionErrorMessages InitiatorAndRespondentMobileNumberCannotBeSame = new(BaseEnumExceptionErrorMessages.InitiatorAndRespondentMobileNumberCannotBeSame, BaseEnumExceptionErrorMessages.InitiatorAndRespondentMobileNumberCannotBeSame.ToString(), new List<ErrorsLang>()
                {
                    new () { LangCode = EnumLanguages.English.LangCode, Message = "Initiator and Respondent party mobile number cannot be same" },
                    new () { LangCode = EnumLanguages.Arabic.LangCode, Message = "لا يمكن أن يكون رقم الهاتف المحمول للمبادر والمستجيب هو نفسه" }
                });
        #endregion

        public static IReadOnlyList<EnumExceptionErrorMessages> GetList()
        {
            return GetAll<EnumExceptionErrorMessages>().ToList();
        }
    }
}

