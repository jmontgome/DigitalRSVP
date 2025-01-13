using DigitalRSVP.WAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DigitalRSVP.WAPI.Handlers.Exceptions
{
    public class ExceptionHandler: IExceptionHandler, IDisposable
    {
        private bool disposedValue;

        private string GenerateNetworkDiagnostics(HttpContext context)
        {
            return $"{context.Request.Path} called by {context.Connection.RemoteIpAddress}.";
        }

        public async Task<StatusCodeResult> HandleExceptionAndReturnHTTPStatusCode<T>(Exception exc, ILogger<T> logger, Guid requestId, HttpContext httpContext)
        {
            string? parsedBody = null;
            if (httpContext.Request.Body != null)
            {
                using (StreamReader reader = new StreamReader(httpContext.Request.Body))
                {
                    parsedBody = await reader.ReadToEndAsync();
                }
            }
            if (DatabaseResponseException.TryCreateServerResponseException(exc, out DatabaseResponseException dbException))
            {
                if (parsedBody != null)
                    logger.LogError($"[Request ID: {requestId}] {dbException}. \r\n\tClient: {GenerateNetworkDiagnostics(httpContext)}.");
                else
                    logger.LogError($"[Request ID: {requestId}] {dbException}. \r\n\tClient: {GenerateNetworkDiagnostics(httpContext)}.");
                return new StatusCodeResult(dbException.GetHttpStatusCode());
            }
            else
            {
                if (parsedBody != null)
                {
                    logger.LogError($"[Request ID: {requestId}] {exc}. \r\n\tClient: {GenerateNetworkDiagnostics(httpContext)}.");
                }
                else
                {
                    logger.LogError($"[Request ID: {requestId}] {exc}. \r\n\tClient: {GenerateNetworkDiagnostics(httpContext)}.");
                }
                return new StatusCodeResult(200);
            }
        }

        #region Dispose Pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ExceptionHandler()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
