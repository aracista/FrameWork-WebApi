using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Util.Logger
{
    public class OperationLog : CommonLog
    {
        public void CreateStartLog(String EndPoint, string Level)
        {
            logExecutionTime(Level, "operation", "\"" + EndPoint + "\"", "START");
        }

        public void CreateEndLog(String EndPoint, string Level, string Message)
        {
            logExecutionTime(Level, "operation", "\"" + EndPoint + "\"", "END", Message);
        }
    }
}
