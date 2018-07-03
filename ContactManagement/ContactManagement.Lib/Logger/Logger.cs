using System;
using System.IO;

namespace ContactManagement.Lib
{
    public class Logger
    {
        string _commonLogFileName;
        string _errorLogFileName;


        //static Lazy<Logger> _loggerObject;
        private static readonly Lazy<Logger> _loggerObject = new Lazy<Logger>(() => new Logger());

        private Logger()
        {
            _commonLogFileName = String.Empty;
            _errorLogFileName = String.Empty;


        }

        public static Logger GetLoggerObject(string logFileName, string errorLogFileName)
        {
            ResetValues(logFileName, errorLogFileName);
            return (_loggerObject.Value);
        }

        public static void ResetValues(string logFileName, string errorLogFileName)
        {
            _loggerObject.Value._commonLogFileName = logFileName;
            _loggerObject.Value._errorLogFileName = errorLogFileName;
        }

        public static Logger GetLoggerObject()
        {
            if (_loggerObject == null || _loggerObject.Value == null)
                throw new Exception("Initialization of Logger is failed , please use GetLoggerObject method with three parameter to initialize it.");
            return (_loggerObject.Value);
        }

        public void WriteLog(string message)
        {
            try
            {
                if (!String.IsNullOrEmpty(message))
                    WriteTextInFile(message);
            }
            catch
            {

            }
        }

        public void WriteError(Exception ex)
        {
            try
            {
                if (ex != null)
                    WriteTextInErrorFile(ex.ToString());
            }
            catch
            {

            }
        }

        public void WriteError(String error)
        {
            try
            {
                if (!String.IsNullOrEmpty(error))
                    WriteTextInErrorFile(error);
            }
            catch
            {

            }
        }

        private void WriteTextInErrorFile(string text)
        {
            if (!File.Exists(_errorLogFileName))
                File.Create(_errorLogFileName);
            if (!String.IsNullOrEmpty(_errorLogFileName) && File.Exists(_errorLogFileName))
            {
                File.WriteAllText(_errorLogFileName, text);
            }

        }

        private void WriteTextInFile(string text)
        {
            if (!File.Exists(_commonLogFileName))
            {
                try
                {
                    File.Create(_commonLogFileName);
                }
                catch(Exception ex)
                {

                }
            }
            if (!String.IsNullOrEmpty(_commonLogFileName) && File.Exists(_commonLogFileName))
            {
                File.WriteAllText(_commonLogFileName, text);
            }

        }

    }
}
