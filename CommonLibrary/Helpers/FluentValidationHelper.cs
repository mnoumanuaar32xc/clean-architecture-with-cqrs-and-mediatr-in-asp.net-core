using DemoDomain.Constants;
using DemoDomain.Enums.DemoApp.Exception;
using DemoDomain.Exceptions.Models;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Helpers
{
    public static class FluentValidationHelper
    {
        #region Common Fluent Validation Error Messages
        public static string MustBeNotNullOrNotEmptyErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeNotNullOrNotEmpty.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeNullOrEmptyErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeNullOrEmpty.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeLessThanExactValueErrorMessage(string propertyName, string exactValue)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeLessThanExactValue.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@ExactValue", exactValue).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeLessThanOrEqualExactValueErrorMessage(string propertyName, string exactValue)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeLessThanOrEqualExactValue.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@ExactValue", exactValue).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeGreaterThanExactValueErrorMessage(string propertyName, string exactValue)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeGreaterThanExactValue.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@ExactValue", exactValue).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeGreaterThanOrEqualExactValueErrorMessage(string propertyName, string exactValue)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeGreaterThanOrEqualExactValue.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@ExactValue", exactValue).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeEqualExactValueErrorMessage(string propertyName, string exactValue)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeEqualExactValue.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@ExactValue", exactValue).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeNotEqualExactValueErrorMessage(string propertyName, string exactValue)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeNotEqualExactValue.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@ExactValue", exactValue).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeMustBeNotExistBeforeErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeNotExistBefore.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        #endregion

        #region Numbers Fluent Validation Error Messages
        public static string InclusiveBetweenErrorMessage(string propertyName, string minValue, string maxValue)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeBetweenValues.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@MinVal", minValue).Replace("@MaxVal", maxValue).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string ExclusiveBetweenErrorMessage(string propertyName, string minValue, string maxValue)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeBetweenValues.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@MinVal", (minValue)).Replace("@MaxVal", (maxValue)).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeNumericValueErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeNumericValue.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        #endregion

        #region string Fluent Validation Error Messages
        public static string MustBeHasExactLengthErrorMessage(string propertyName, string length)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeHasExactLength.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@Length", length).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeHasMinLengthErrorMessage(string propertyName, string length)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeHasMinLength.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@Length", length).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeHasMaxLengthErrorMessage(string propertyName, string length)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeHasMaxLength.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@Length", length).ToString()).ToString();
            }

            return _messageTemplate;
        }

        #endregion

        #region Dates Fluent Validation Error Messages
        public static string MustBeAfterTargetDateErrorMessage(string propertyName, DateTime targetDate)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeAfterTargetDate.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).Replace("@TargetDate", targetDate.ToString()).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeValidIsoDateFormatErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeValidIsoDateFormat.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        #endregion

        #region General Fluent Validation Error Messages
        public static string MustBeValidCreditCardErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeValidCreditCard.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeValidEmailErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputMustBeValidEmail.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string MustBeValueExistsInEnumErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InputValueMustBeExistsInEnum.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string HaveAtLeastOneValueErrorMessage()
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.HaveAtLeastOneValue.Value, "");

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string HaveAtLeastTwoRecordedValueErrorMessage()
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.HaveAtLeastOneValue.Value, "");

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string AtLeastOneValueRequiredErrorMessage(params string[] propertyNames)
        {
            string propertiesList = string.Join(", ", propertyNames);
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.AtLeastOneInputValidation.Value, propertiesList);
            var _messageTemplate = _errorMessageObj.MessageTemplate;
            if (propertyNames == null || propertyNames.Length == 0)
            {
                return _messageTemplate;
            }

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                string formattedMessage = item.Message.Replace("$PropertyName$", propertiesList);
                _messageTemplate = _messageTemplate.Replace("@Message_" + _langCode, formattedMessage);
            }
            return _messageTemplate;
        }
        public static string MustBeFromEmployeeOrCompanyErrorMessage(string propertyName)
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.ApplicantMustBeEitherEmployeeOrCompany.Value, propertyName);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).Replace("$PropertyName$", propertyName).ToString()).ToString();
            }

            return _messageTemplate;
        }
        #endregion

        #region Multiple Dependents Validation Error Message 
        public static string MultipleDependentsNullOrEmptyErrorMessage(params string[] propertyNames)
        {
            string propertiesList = string.Join(", ", propertyNames);
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.BothInputsAreMandatoryValidation.Value, propertiesList);
            var _messageTemplate = _errorMessageObj.MessageTemplate;
            if (propertyNames == null || propertyNames.Length == 0)
            {
                return _messageTemplate;
            }

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                string formattedMessage = item.Message.Replace("$PropertyName$", propertiesList);
                _messageTemplate = _messageTemplate.Replace("@Message_" + _langCode, formattedMessage);
            }
            return _messageTemplate;
        }
        public static string MultipleDependentsNullOrEmptyErrorMessage(EnumExceptionErrorMessages exceptionErrorMessage)
        {
            var _errorMessageObj = GetErrorMessage(exceptionErrorMessage.Value, "");
            var _messageTemplate = _errorMessageObj.MessageTemplate;
            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = _messageTemplate.Replace("@Message_" + _langCode, item.Message.Replace("$PropertyName$", ""));
            }
            return _messageTemplate;
        }
        #endregion

        #region Complaint Fluent Validation Error Messages
        public static string InvalidComplaintPartiesCountErrorMessage()
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InvalidComplaintPartiesCount.Value, string.Empty);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).ToString()).ToString();
            }

            return _messageTemplate;
        }
        public static string InvalidComplaintClaimsCountErrorMessage()
        {
            var _errorMessageObj = GetErrorMessage(EnumExceptionErrorMessages.InvalidComplaintClaimsCount.Value, string.Empty);

            var _messageTemplate = _errorMessageObj.MessageTemplate;

            foreach (var item in _errorMessageObj.Messages)
            {
                var _langCode = item.LangCode;
                _messageTemplate = new StringBuilder(_messageTemplate).Replace("@Message_" + _langCode, new StringBuilder(item.Message).ToString()).ToString();
            }

            return _messageTemplate;
        }
        #endregion

        #region Private Methods
        private static ErrorMessageTemplate GetErrorMessage(int appErrorKey, string propertyName)
        {
            ErrorMessageTemplate _errorMessageObj = new ErrorMessageTemplate();

            var _errorExceptionEnum = Enumeration.FromValue<EnumExceptionErrorMessages>(appErrorKey);

            var _messageTemplate = ConstApplicationGeneral.ExceptionErrorTemplate;

            _errorMessageObj.MessageTemplate = new StringBuilder(_messageTemplate).Replace("@AppErrorKey", appErrorKey.ToString()).Replace("@PropertyName", propertyName).ToString();

            _errorMessageObj.Messages = _errorExceptionEnum.Messages;

            return _errorMessageObj;
        }
        #endregion


    }
}
