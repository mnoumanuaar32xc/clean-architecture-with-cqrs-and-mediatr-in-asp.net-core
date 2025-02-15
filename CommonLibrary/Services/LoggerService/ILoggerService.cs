using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Services.LoggerService
{
    /// <summary>
    /// Logger class contract.
    /// </summary>
    public interface ILoggerService
    {

        //"Log Scope ID" is a unique identifier generated for grouping and associating log entries with specific operations, tasks, or requests, facilitating easier tracking and analysis of logs for those activities, so it is better to inject the service as scoped
        public string LogScopeId { get; }


        /// <summary>
        /// Log a debug message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Debug(object message, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Info(object message, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Warn(object message, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Error(object message, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log a fatal message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Fatal(object message, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log a debug message with an associated exception.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Debug(object message, Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log an informational message with an associated exception.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Info(object message, Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log a warning message with an associated exception.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Warn(object message, Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log an error message with an associated exception.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Error(object message, Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log a database error message with an associated exception.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void DBError(object message, Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log a fatal message with an associated exception.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Fatal(object message, Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log a database error message with an associated exception.
        /// Log an exception including the stack trace of exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void DBError(Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log an error message with an associated exception.
        /// Log an exception including the stack trace of exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Error(Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);

        /// <summary>
        /// Log a fatal message with an associated exception.
        /// Log an exception including the stack trace of exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="metaData">Optional additional metadata to include in the log entry.</param>
        /// <param name="userId">Optional user ID associated with the log entry.</param>
        /// <param name="requestUri">Optional request URI associated with the log entry.</param>
        void Fatal(Exception exception, IDictionary<string, object>? metaData = null, long? userId = null, string? requestUri = null);
    }



    public interface ILoggerService<T> : ILoggerService
    {
        // Optionally add methods that specifically use T
        // this T will hold the object type the logger linked to it.
    }
}
