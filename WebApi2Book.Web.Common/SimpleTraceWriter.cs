using System;
using System.Net.Http;
using System.Web.Http.Tracing;
using log4net;
using WebApi2Book.Common.Logging;

namespace WebApi2Book.Web.Common
{
    public class SimpleTraceWriter : ITraceWriter
    {
        private readonly ILog _log;

        public SimpleTraceWriter(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof(SimpleTraceWriter));
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            var rec = new TraceRecord(request, category, level);
            traceAction(rec);
            WriteTrace(rec);

        }

        private void WriteTrace(TraceRecord rec)
        {
            const string traceFormat =
                "RequestId={0};{1}Kind={2};{1}Status={3};{1}Operation={4};{1}Operator={5};{1}Category={6};{1}Request={7};{1}Message={8}";

            var args = new object[]
            {
                rec.RequestId,
                Environment.NewLine,
                rec.Status,
                rec.Operation,
                rec.Operator,
                rec.Category,
                rec.Request,
                rec.Message
            };

            switch (rec.Level)
            {
                case TraceLevel.Off:

                    break;
                case TraceLevel.Debug:
                    _log.DebugFormat(traceFormat, args);
                    break;
                case TraceLevel.Info:
                    _log.InfoFormat(traceFormat, args);
                    break;
                case TraceLevel.Warn:
                    _log.WarnFormat(traceFormat, args);
                    break;
                case TraceLevel.Error:
                    _log.ErrorFormat(traceFormat, args);
                    break;
                case TraceLevel.Fatal:
                    _log.ErrorFormat(traceFormat, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}