using CommonLibrary.Helpers;
using DemoDomain.Enums.DemoApp.Validation;
using DemoDomain.Exceptions;
using DemoDomain.Exceptions.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Newtonsoft.Json;

namespace CommonLibrary.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    List<ApiExceptionErrorModel> errors = new List<ApiExceptionErrorModel>();

                    List<string> validationMessages = GetValidationErrorsCustomMessages(failures);

                    foreach (var error in validationMessages)
                    {
                        try
                        {
                            var item = JsonConvert.DeserializeObject<ApiExceptionErrorModel>(error);

                            if (item != null)
                                errors.Add(item);
                        }
                        catch (Exception ex)
                        {
                            // Complete without errors and no actions
                        }
                    }
                    throw new ParametersValidationException(errors);
                }
            }

            return await next();
        }
        private static List<string> GetValidationErrorsCustomMessages(List<ValidationFailure> validationFailures)
        {
            List<string> errors = new List<string>();

            foreach (var error in validationFailures)
            {
                if (error.ErrorCode == EnumValidationErrorTypes.NotEmptyValidator.ToString() || error.ErrorCode == EnumValidationErrorTypes.NotNullValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeNotNullOrNotEmptyErrorMessage(error.PropertyName));

                else if (error.ErrorCode == EnumValidationErrorTypes.EmptyValidator.ToString() || error.ErrorCode == EnumValidationErrorTypes.NullValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeNullOrEmptyErrorMessage(error.PropertyName));

                else if (error.ErrorCode == EnumValidationErrorTypes.LessThanValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeLessThanExactValueErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["ComparisonValue"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.LessThanOrEqualValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeLessThanOrEqualExactValueErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["ComparisonValue"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.GreaterThanValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeGreaterThanExactValueErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["ComparisonValue"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.GreaterThanOrEqualValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeGreaterThanOrEqualExactValueErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["ComparisonValue"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.EqualValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeEqualExactValueErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["ComparisonValue"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.NotEqualValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeNotEqualExactValueErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["ComparisonValue"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.ExclusiveBetweenValidator.ToString())
                    errors.Add(FluentValidationHelper.ExclusiveBetweenErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["From"].ToString(), error.FormattedMessagePlaceholderValues["To"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.InclusiveBetweenValidator.ToString())
                    errors.Add(FluentValidationHelper.InclusiveBetweenErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["From"].ToString(), error.FormattedMessagePlaceholderValues["To"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.IsNumericTypeValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeNumericValueErrorMessage(error.PropertyName));

                else if (error.ErrorCode == EnumValidationErrorTypes.ExactLengthValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeHasExactLengthErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["MinLength"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.MaximumLengthValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeHasMaxLengthErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["MaxLength"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.MinimumLengthValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeHasMinLengthErrorMessage(error.PropertyName, error.FormattedMessagePlaceholderValues["MinLength"].ToString()));

                else if (error.ErrorCode == EnumValidationErrorTypes.CreditCardValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeValidCreditCardErrorMessage(error.PropertyName));

                else if (error.ErrorCode == EnumValidationErrorTypes.EmailValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeValidEmailErrorMessage(error.PropertyName));

                else if (error.ErrorCode == EnumValidationErrorTypes.EnumValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeValueExistsInEnumErrorMessage(error.PropertyName));

                else if (error.ErrorCode == EnumValidationErrorTypes.HaveAtLeastOneValue.ToString())
                    errors.Add(FluentValidationHelper.HaveAtLeastOneValueErrorMessage());

                else if (error.ErrorCode == EnumValidationErrorTypes.IsoDateFormatValidator.ToString())
                    errors.Add(FluentValidationHelper.MustBeValidIsoDateFormatErrorMessage(error.PropertyName));

            }
            return errors;
        }
    }
}
