using DigitalRSVP.WAPI.Models;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;

namespace DigitalRSVP.WAPI.Exceptions
{
    public class DatabaseResponseException : Exception
    {
        private static readonly Regex _serverMessageFormatExpected = new Regex(@"[[]{1}(?:RBError:){1}[\d][]]{1}\s{1}[-]{1}\s{1}(?:.*)");

        private ResponseCode _responseCode;
        public ResponseCode ResponseCode
        {
            get
            {
                return _responseCode;
            }
        }

        public bool ValidExceptionResponse
        {
            get
            {
                return ResponseCode == ResponseCode.UNKNOWN_ERROR ? false : true;
            }
        }

        private string _message;
        public override string Message
        {
            get
            {
                return _message;
            }
        }

        public DatabaseResponseException(HttpStatusCode statusCode, string message)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK: _responseCode = ResponseCode.SUCCESS; break;
                case HttpStatusCode.AlreadyReported: _responseCode = ResponseCode.RECORD_EXISTS; break;
                case HttpStatusCode.NoContent: _responseCode = ResponseCode.NO_RECORD; break;
                default: _responseCode = ResponseCode.UNKNOWN_ERROR; break;
            }
            this._message = message;
        }

        public static bool TryCreateServerResponseException(Exception exc, out DatabaseResponseException dbException)
        {
            dbException = null;
            if (exc.GetType() == typeof(SqlException))
            {
                if (_serverMessageFormatExpected.Match(exc.Message).Success)
                {
                    if (int.TryParse(Regex.Match(Regex.Match(exc.Message, @"[[]{1}(?:RBError:){1}[\d][]]{1}").Value, @"[\d]").Value, out int errorCode))
                    {
                        dbException = new DatabaseResponseException((HttpStatusCode)errorCode, exc.Message);
                    }
                    else
                    {
                        dbException = new DatabaseResponseException((HttpStatusCode)99, exc.Message);
                    }
                    return true;
                }
            }
            return false;
        }

        public int GetHttpStatusCode()
        {
            switch(ResponseCode)
            {
                case ResponseCode.SUCCESS: return 200;
                case ResponseCode.RECORD_EXISTS: return 208;
                case ResponseCode.NO_RECORD: return 204;
                default: return 500;
            }
        }
    }
}
