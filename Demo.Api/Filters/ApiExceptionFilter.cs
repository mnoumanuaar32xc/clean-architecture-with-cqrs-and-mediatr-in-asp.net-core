using CommonLibrary.Services.LoggerService;
using Demo.Api.ApiFramework.Tools;
using DemoDomain.Enums.DemoApp.Authentication.Jwt;
using DemoDomain.Enums.DemoApp.Exception;
using DemoDomain.Exceptions;
using DemoDomain.Exceptions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedModels;
using SharedModels.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Demo.Api.Filters
{
    public class ApiExceptionFilter: ExceptionFilterAttribute
    {
        #region Private Members
        private readonly IDictionary<Type, Func<ExceptionContext, Task>> _exceptionHandlers;
        private readonly IEnumerable<Type> _exceptionsToBeLogged;
        private ILoggerService<ApiExceptionFilter>? _logger;
        private readonly IWebHostEnvironment _environment;

        #endregion
        public ApiExceptionFilter(IWebHostEnvironment environment)
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Func<ExceptionContext, Task>>
            {
                { typeof(UnauthorizedException), HandleUnauthorizedException },
                { typeof(DemoDomain.Exceptions.InvalidCredentialException), HandleInvalidCredentialException },
                { typeof(ForbiddenException), HandleForbiddenException },
                { typeof(BussinessValidationException), HandleBussinessValidationException },
                { typeof(ParametersValidationException), HandleParametersValidationException },
                { typeof(ProcessValidationException), HandleProccessValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(ExistingRecordException), HandleExistingRecordException },
                { typeof(ArgumentNullException), HandleArgumentsNullException },
                { typeof(InvalidNullInputException), HandleInvalidNullInputException },
                { typeof(BadParameterException), HandleBadParameterException },
                { typeof(DownloadException), HandleDownloadException },
                { typeof(NoContentException), HandleNoContentException },
                { typeof(ServiceUnavailableException), HandleServiceUnavailableException },
                { typeof(UploadException), HandleUploadException },
                { typeof(SqlErrorException), HandleSqlErrorException },
                { typeof(SmsException), HandleSmsException },
                { typeof(DecryptionException), HandleDecryptionException }
            };
            _environment = environment;
            _exceptionsToBeLogged = new List<Type>()
            {
                typeof(DemoDomain.Exceptions.InvalidCredentialException),
                typeof(BussinessValidationException),
                typeof(ParametersValidationException),
                typeof(ProcessValidationException),
                typeof(NotFoundException),
                typeof(ExistingRecordException),
                typeof(ArgumentNullException),
                typeof(InvalidNullInputException),
                typeof(BadParameterException),
                typeof(DownloadException),
                typeof(NoContentException),
                typeof(ServiceUnavailableException),
                typeof(UploadException),
                typeof(SqlErrorException),
                typeof(SmsException),
                // TODO:: this two for monitoring the system if it stop anyone has the rights to acces, we can remove them later.
                typeof(UnauthorizedException),
                typeof(ForbiddenException)
            };
        }

        #region Exception Handlers Methods
        private async Task HandleExceptionAsync(ExceptionContext context)
        {
            // Check for Exception Type
            Type type = context.Exception.GetType();

            // Check if exception is registered to be handled
            if (_exceptionHandlers.ContainsKey(type))
            {
                await _exceptionHandlers[type].Invoke(context);

                context.HttpContext.Response.StatusCode = ((ApiResult<int>)((ObjectResult)context.Result).Value).StatusCode.Value;

                return;
            }

            await HandleUnknownExceptionAsync(context, type);

            await Task.CompletedTask;
        }
        private List<ApiExceptionErrorModel> CreateErrorResponse(List<ApiExceptionErrorModel> apiExceptionList, string errorLogMessage, string logMessageId)
        {
            var errors = new List<ApiExceptionErrorModel>();

            if (_environment.IsProduction())
            {
                // Add the Exception General Message to return it to the client
                errors.AddRange(apiExceptionList);
            }
            else
            {
                errors.Add(new ApiExceptionErrorModel
                {
                    Messages = new()
                        {
                            new() { LangCode = "en", Message = $"{logMessageId} - {errorLogMessage}" },
                            new() { LangCode = "ar", Message = $"{logMessageId} - {errorLogMessage}" },
                        }
                });
            }

            return errors;
        }

        private async Task HandleUnknownExceptionAsync(ExceptionContext context, Type exceptionType)
        {
            var exception = (exceptionType == typeof(BaseException)) ? context.Exception as BaseException : context.Exception;

            // Get enum for the error message for UnknownException
            ApiExceptionErrorModel unknownExceptionEnum = GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.UnknownException.Value);

            // Get the error log message
            string errorLogMessage = exception != null ? exception.Message : string.Join(" & ", unknownExceptionEnum.Messages.Where(message => message.LangCode == "en").Select(message => message.Message));

            // Log the request body and insert the error message
            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            // Replace the $LogNumber$ with the log message id
            unknownExceptionEnum.Messages.ForEach(e => e.Message = new StringBuilder(e.Message).Replace("$LogNumber$", _logMessageId).ToString());

            var apiErrors = new List<ApiExceptionErrorModel>() { unknownExceptionEnum };

            var errors = CreateErrorResponse(apiErrors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            // Return the result to the client
            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status500InternalServerError, errors: errors));

            // Set the status code to 500
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Mark the exception as handled
            context.ExceptionHandled = true;

            await Task.CompletedTask;
        }

        private async Task HandleProccessValidationException(ExceptionContext context)
        {
            // Get the exception
            var exception = context.Exception as ProcessValidationException;

            // Get the error messages (Specific Message or Default)
            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.ProcessValidationException.Value) // Default error message
            ];

            // Get the error log message
            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            // Log the request body and insert the error message
            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            // Replace the $LogNumber$ with the log message id
            errors.ForEach(e => e.Messages.ForEach(m => m.Message = new StringBuilder(m.Message).Replace("$LogNumber$", _logMessageId).ToString()));

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            // Return the result to the client
            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status422UnprocessableEntity, errors: errors));

            // mark the exception as handled
            context.ExceptionHandled = true;
        }
        private async Task HandleNotFoundException(ExceptionContext context)
        {
            // Get the exception
            var exception = context.Exception as NotFoundException;

            // Get the error messages (Specific Message or Default)
            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.NotFoundException.Value) // Default error message
            ];

            // Get the error log message
            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            // Log the request body and insert the error message
            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            // Replace the $LogNumber$ with the log message id
            errors.ForEach(e => e.Messages.ForEach(m => m.Message = new StringBuilder(m.Message).Replace("$LogNumber$", _logMessageId).ToString()));

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            // Return the result to the client
            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status404NotFound, errors: errors));

            // mark the exception as handled
            context.ExceptionHandled = true;
        }
        private async Task HandleArgumentsNullException(ExceptionContext context)
        {
            var exception = context.Exception as ArgumentNullException;

            // Get Argument Name
            string argumentName = (exception is not null && !string.IsNullOrEmpty(exception.ParamName)) ? exception.ParamName : string.Empty;

            // Get enum for the error message for ArgumentNullException
            var argumentNullExceptionEnum = GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.ArgumentNullException.Value);

            // Replace the @ArgumentName with the argument name
            argumentNullExceptionEnum.Messages.ForEach(e => e.Message = new StringBuilder(e.Message).Replace("@ArgumentName", argumentName).ToString());

            // Get the error log message
            string errorLogMessage = string.Join(" & ", argumentNullExceptionEnum.Messages.Where(message => message.LangCode == "en").Select(message => message.Message));

            // Log the request body and insert the error message
            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            // Get enum for the error message for UnknownException
            var generalExceptionErrorEnum = GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.UnknownException.Value);

            // Replace the $LogNumber$ with the log message id
            generalExceptionErrorEnum.Messages.ForEach(e => e.Message = new StringBuilder(e.Message).Replace("$LogNumber$", _logMessageId).ToString());

            var apiErrors = new List<ApiExceptionErrorModel>() { generalExceptionErrorEnum };

            // Get Exception General Message to return it to the client
            List<ApiExceptionErrorModel> errors = CreateErrorResponse(apiErrors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            // Return the result to the client
            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status500InternalServerError, errors: errors));

            // mark the exception as handled
            context.ExceptionHandled = true;
        }
        private async Task HandleInvalidNullInputException(ExceptionContext context)
        {
            var exception = context.Exception as InvalidNullInputException;

            // Get property Name
            string propertyName = (exception is not null && !string.IsNullOrEmpty(exception.PropertyName)) ? exception.PropertyName : string.Empty;

            // Get enum for the error message for InvalidNullInputException
            var invalidNullInputExceptionEnum = GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.InvalidNullInputException.Value);

            // Replace the @PropertyName with the property name
            invalidNullInputExceptionEnum.Messages.ForEach(e => e.Message = new StringBuilder(e.Message).Replace("@PropertyName", propertyName).ToString());

            // Get the error log message
            string errorLogMessage = string.Join(" & ", invalidNullInputExceptionEnum.Messages.Where(message => message.LangCode == "en").Select(message => message.Message));

            // Log the request body and insert the error message
            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            // Get enum for the error message for UnknownException
            var generalExceptionErrorEnum = GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.UnknownException.Value);

            // Replace the $LogNumber$ with the log message id
            generalExceptionErrorEnum.Messages.ForEach(e => e.Message = new StringBuilder(e.Message).Replace("$LogNumber$", _logMessageId).ToString());

            var apiErrors = new List<ApiExceptionErrorModel>() { generalExceptionErrorEnum };

            // Get Exception General Message to return it to the client
            List<ApiExceptionErrorModel> errors = CreateErrorResponse(apiErrors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            // Return the result to the client
            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status500InternalServerError, errors: errors));

            // mark the exception as handled
            context.ExceptionHandled = true;
        }



        private async Task HandleBussinessValidationException(ExceptionContext context)
        {
            var exception = context.Exception as BussinessValidationException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.BussinessValidationException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status422UnprocessableEntity, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleInvalidCredentialException(ExceptionContext context)
        {
            var exception = context.Exception as DemoDomain.Exceptions.InvalidCredentialException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.InvalidCredentialException.Value) // Default error message
            ];
            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status401Unauthorized, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleUnauthorizedException(ExceptionContext context)
        {
            var exception = context.Exception as UnauthorizedException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.UnauthorizedException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status401Unauthorized, errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleForbiddenException(ExceptionContext context)
        {
            var exception = context.Exception as ForbiddenException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.ForbiddenException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status403Forbidden, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleParametersValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ParametersValidationException;

            List<ApiExceptionErrorModel> errors = new List<ApiExceptionErrorModel>();

            if (exception != null && exception.ValidationExceptionErrors != null && exception.ValidationExceptionErrors.Count > 0)
            {
                errors = exception.ValidationExceptionErrors;
            }
            else
            {
                errors =
                 [
                     (exception != null && exception.ApiErrorKey != null)
                         ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                         : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.ParametersValidationException.Value) // Default error message
                 ];
            }

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status400BadRequest, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleExistingRecordException(ExceptionContext context)
        {
            var exception = context.Exception as ExistingRecordException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.ExistingRecordException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status422UnprocessableEntity, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleBadParameterException(ExceptionContext context)
        {
            var exception = context.Exception as BadParameterException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.BadParameterException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status400BadRequest, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleDownloadException(ExceptionContext context)
        {
            var exception = context.Exception as DownloadException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.DownloadException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status500InternalServerError, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleNoContentException(ExceptionContext context)
        {
            var exception = context.Exception as NoContentException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.NoContentException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status404NotFound, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleServiceUnavailableException(ExceptionContext context)
        {
            var exception = context.Exception as ServiceUnavailableException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.ServiceUnavailableException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status503ServiceUnavailable, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleUploadException(ExceptionContext context)
        {
            var exception = context.Exception as UploadException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.UploadException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status503ServiceUnavailable, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleSqlErrorException(ExceptionContext context)
        {
            var exception = context.Exception as SqlErrorException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.SqlErrorException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status415UnsupportedMediaType, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleSmsException(ExceptionContext context)
        {
            var exception = context.Exception as SmsException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.SmsException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status500InternalServerError, errors: errors));

            context.ExceptionHandled = true;
        }
        private async Task HandleDecryptionException(ExceptionContext context)
        {
            var exception = context.Exception as DecryptionException;

            List<ApiExceptionErrorModel> errors =
            [
                (exception != null && exception.ApiErrorKey != null)
                    ? GetApiExceptionErrorsByApiErrorKey(exception.ApiErrorKey.Value)
                    : GetApiExceptionErrorsByApiErrorKey(EnumExceptionErrorMessages.DecryptionException.Value) // Default error message
            ];

            string errorLogMessage = string.Join(" & ", errors.SelectMany(error => error.Messages).Where(message => message.LangCode == "en").Select(message => message.Message));

            await LogRequestBodyAsync(context, errorLogMessage);

            // Get log error message id to return it to the client
            string _logMessageId = _logger is not null ? _logger.LogScopeId.Split('-')[0] : string.Empty;

            errors = CreateErrorResponse(errors, errorLogMessage ?? "- errorLogMessage", _logMessageId);

            context.Result = new ObjectResult(new ApiResult<int>(-1, StatusCodes.Status500InternalServerError, errors: errors));

            context.ExceptionHandled = true;
        }

        #endregion
        #region Log Methods
        private async Task<string> ReadRequestBodyAsync(HttpRequest request, Encoding encoding = null)
        {
            if (request.Body == null)
                return string.Empty;

            encoding ??= Encoding.UTF8;

            // Check if the body can seek and reset position if necessary
            if (request.Body.CanSeek)
            {
                request.Body.Position = 0;
            }
            else
            {
                // Create a MemoryStream to copy the non-seekable stream for reading
                using var memoryStream = new MemoryStream();
                await request.Body.CopyToAsync(memoryStream).ConfigureAwait(false);
                memoryStream.Position = 0;
                request.Body = memoryStream; // Optionally replace the request body
            }

            // Read the body using the encoding provided or default to UTF-8
            using var reader = new StreamReader(request.Body, encoding, leaveOpen: true);
            var body = await reader.ReadToEndAsync().ConfigureAwait(false);

            // Reset the position if the stream is seekable
            if (request.Body.CanSeek)
            {
                request.Body.Position = 0;
            }

            return body;
        }
        private long? GetCurrentUserId(HttpRequest request)
        {
            long? _id = null;
            var authorizationHeader = request.Headers["Authorization"].ToString();
            string? tokenString = null;

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                return _id;

            tokenString = authorizationHeader["Bearer ".Length..].Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var readToken = tokenHandler.ReadJwtToken(tokenString);
                if (readToken != null)
                {
                    var userIdClaim = readToken.Claims.FirstOrDefault(c => c.Type == EnumStandardJwtClaimTypes.NameId);
                    if (userIdClaim != null)
                    {
                        _id = Convert.ToInt64(userIdClaim.Value);
                    }
                }
            }
            catch
            {
                // ignored
            }

            return _id;
        }
        private async Task LogRequestBodyAsync(ExceptionContext context, string? message = null)
        {
            var _httpContext = context.HttpContext;
            var _ex = context.Exception;

            var endpoint = _httpContext.Request.Path; // Get the endpoint
            var userIp = (_httpContext.Connection.RemoteIpAddress)?.ToString();

            var requestBody = await ReadRequestBodyAsync(_httpContext.Request);
            var queryStringParams = _httpContext.Request.QueryString.ToString();

            long? userId = GetCurrentUserId(_httpContext.Request);

            // Get all request headers dynamically
            var requestHeaders = _httpContext.Request.Headers
                .ToDictionary(kv => kv.Key, kv => kv.Value.ToString());

            var _errorInfo = new
            {
                HttpScheme = _httpContext.Request.Scheme,
                Endpoint = endpoint,
                UserId = userId,
                RequestMethod = _httpContext.Request.Method,
                UserIP = userIp,
                RequestBody = requestBody,
                RequestQuery = queryStringParams,
                RequestHeaders = requestHeaders, // Include all request headers
            };
            try
            {
                string errorLogMessage = string.IsNullOrEmpty(message) ? _ex.Message : message;

                if (_ex is not System.Data.SqlClient.SqlException && _ex is not SqlErrorException)
                    _logger.Error(errorLogMessage, _ex, _errorInfo.AsDictionary(), userId, endpoint);
                else
                    _logger.DBError(errorLogMessage, _ex, _errorInfo.AsDictionary(), userId, endpoint);

            }
            catch (Exception ex)
            {
                // Todo => will handle exception using log into file
            }

            await Task.CompletedTask;
        }
        #endregion
        #region Helper Methods
        private List<ApiExceptionErrorModel> GetApiExceptionErrorsByApiErrorKeys(List<int> apiErrorKeys)
        {
            // Create a list to store the ApiExceptionError objects
            var apiExceptionErrors = new List<ApiExceptionErrorModel>();

            // Create a list to store the EnumExceptions objects
            var enumExceptions = Enumeration.GetAllByEnumValues<EnumExceptionErrorMessages>(apiErrorKeys).ToList();

            // Iterate over each error key provided
            foreach (var key in enumExceptions)
            {
                var apiExceptionError = new ApiExceptionErrorModel
                {
                    AppErrorKey = key.Value,
                    Messages = key.Messages
                };

                // Add the ApiExceptionError object to the list
                apiExceptionErrors.Add(apiExceptionError);
            }

            // Return the list of ApiExceptionError objects
            return apiExceptionErrors;

        }
        private ApiExceptionErrorModel GetApiExceptionErrorsByApiErrorKey(int apiErrorKey)
        {
            // Create a list to store the ApiExceptionError objects
            var apiExceptionError = new ApiExceptionErrorModel();

            // Create a list to store the EnumExceptions objects
            var enumExceptions = Enumeration.FromValue<EnumExceptionErrorMessages>(apiErrorKey);

            if (enumExceptions != null)
            {
                apiExceptionError.AppErrorKey = enumExceptions.Value;
                apiExceptionError.Messages = enumExceptions.Messages;
            }

            // Return the ApiExceptionError object
            return apiExceptionError;
        }
        #endregion
    }
}
