using Open.Common;
using Open.Common.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Open.Common.Configuration;

namespace Open.Common.Utility
{
    public class TraceUtility
    {
        public const string __defaultApplicationLogSource = "SkillsInventoryTool";

        private static string _applicationLogSource = null;
        public static string ApplicationLogSource
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_applicationLogSource))
                {
                    // First use general AppSettings value that is available to all apps
                    _applicationLogSource = CommonConfiguration.Item(CommonConstants.AppSettings.ApplicationLogSource);

                    // Then use a default value
                    if (String.IsNullOrWhiteSpace(_applicationLogSource))
                        _applicationLogSource = __defaultApplicationLogSource;
                }

                return _applicationLogSource;
            }
        }

        public static void WriteInformationTrace(Type sender, string methodName, TraceType traceType)
        {
            WriteInformationTrace(sender, methodName, null, null, traceType);
        }

        public static void WriteInformationTrace(Type sender, string methodName, string wrappingMethod, string detail, TraceType traceType)
        {
            StringBuilder sbMessage = new StringBuilder();
            if (sender != null)
            {
                sbMessage.Append(sender.FullName);
            }
            else 
            { 
                sbMessage.Append("UnknownClass"); 
            }
            sbMessage.Append(".");

            if (!String.IsNullOrEmpty(methodName))
            {
                sbMessage.Append(methodName);
                if (!methodName.EndsWith(")"))
                {
                    sbMessage.Append("()");
                }
            }
            else
            {
                sbMessage.Append("UnknownMethod()");
            }

            if (!String.IsNullOrEmpty(wrappingMethod))
            {
                sbMessage.Append(" wrapping ");
                sbMessage.Append(wrappingMethod);
                if (!wrappingMethod.EndsWith(")"))
                {
                    sbMessage.Append("()");
                }
            }

            sbMessage.Append(".");

            if (!String.IsNullOrEmpty(detail))
            {
                sbMessage.Append(" ");
                sbMessage.Append(detail);
            }

            System.Diagnostics.Trace.WriteLine(sbMessage.ToString(), "APPTRACE - " + traceType.ToString());
        }

        private static void WriteMessageToTrace(string message, TraceEventType eventType)
        {
            TraceSource traceSource = new TraceSource(ApplicationLogSource);
            traceSource.TraceEvent(eventType, 0, message);
            traceSource.Flush();
            traceSource.Close();
        }

        public static Guid LogCriticalException(System.Exception ex)
        {
            Guid messageId = Guid.NewGuid();
            if (ex != null)
                WriteMessageToTrace(FormatExceptionMessage(ex, messageId), TraceEventType.Critical);

            return messageId;
        }

        public static Guid LogException(System.Exception ex)
        {
            Guid messageId = Guid.NewGuid();
            if (ex != null)
                WriteMessageToTrace(FormatExceptionMessage(ex, messageId), TraceEventType.Error);

            return messageId;
        }

        public static void LogInformationMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message)) return;
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.AppendLine();
            AppendBanner(sbMessage, "Informational Message", '-');
            sbMessage.Append(message);
            WriteMessageToTrace(sbMessage.ToString(), TraceEventType.Information);
        }

        public static void LogWarningMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message)) return;
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.AppendLine();
            AppendBanner(sbMessage, "Warning Message", '*');
            sbMessage.Append(message);
            WriteMessageToTrace(sbMessage.ToString(), TraceEventType.Warning);
        }

        public static void LogCriticalErrorMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message)) return;
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.AppendLine();
            AppendBanner(sbMessage, "Critical Error Message", '*');
            sbMessage.Append(message);
            WriteMessageToTrace(sbMessage.ToString(), TraceEventType.Critical);
        }

        public static void LogErrorMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message)) return;
            StringBuilder sbMessage = new StringBuilder();
            sbMessage.AppendLine();
            AppendBanner(sbMessage, "Error Message", '*');
            sbMessage.Append(message);
            WriteMessageToTrace(sbMessage.ToString(), TraceEventType.Error);
        }

        public static void LogDiagnosticMessage(string conciseMessage, string verboseMessage)
        {
            if ((DiagnosticLoggingLevel == DiagnosticLoggingOption.Verbose) && (!String.IsNullOrWhiteSpace(verboseMessage)))
            {
                WriteMessageToTrace(verboseMessage, TraceEventType.Verbose);
            }
            else if (!String.IsNullOrWhiteSpace(conciseMessage))
            {
                WriteMessageToTrace(conciseMessage, TraceEventType.Information);
            }
        }

        public static void LogDiagnosticMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message)) return;
            if (DiagnosticLoggingLevel == DiagnosticLoggingOption.Verbose)
            {
                WriteMessageToTrace(message, TraceEventType.Verbose);
            }
            else
            {
                WriteMessageToTrace(message, TraceEventType.Information);
            }
        }

        public static string FormatExceptionMessage(System.Exception ex, Guid? messageId = null)
        {
            StringBuilder sbExceptionMessage = new StringBuilder();

            AppendBanner(sbExceptionMessage, String.Format("Exception Raised: {0}, Component: {1}", ex.GetType().FullName, ApplicationLogSource), '*');  //'='
            if (messageId.HasValue)
                AppendLabelledLine(sbExceptionMessage, "MessageId", messageId.Value.ToString());
            AppendLabelledLine(sbExceptionMessage, "Message", ex.Message);
            AppendLabelledLine(sbExceptionMessage, "Source", ex.Source);
            sbExceptionMessage.Append(ex.StackTrace);

            FormatInnerExceptionMessage(ex.InnerException, sbExceptionMessage);

            return sbExceptionMessage.ToString();
        }

        private static void FormatInnerExceptionMessage(System.Exception ex, StringBuilder sbExceptionMessage)
        {
            if (ex == null)
                return;

            sbExceptionMessage.AppendLine();
            AppendBanner(sbExceptionMessage, String.Format("Inner Exception: {0}", ex.GetType().FullName), '-');
            AppendLabelledLine(sbExceptionMessage, "Message", ex.Message);
            AppendLabelledLine(sbExceptionMessage, "Source", ex.Source);
            sbExceptionMessage.Append(ex.StackTrace);

            FormatInnerExceptionMessage(ex.InnerException, sbExceptionMessage);
        }

        private static DiagnosticLoggingOption? _diagnosticLoggingLevel = null;
        public static DiagnosticLoggingOption DiagnosticLoggingLevel
        {
            get
            {
                if (!_diagnosticLoggingLevel.HasValue)
                {
                    _diagnosticLoggingLevel = DiagnosticLoggingOption.Disabled;
                    try
                    {
                        string logDiagnosticString = CommonConfiguration.Item(CommonConstants.AppSettings.DiagnosticLoggingLevel);
                        if (!String.IsNullOrWhiteSpace(logDiagnosticString))
                            _diagnosticLoggingLevel = (DiagnosticLoggingOption)System.Enum.Parse(typeof(DiagnosticLoggingOption), logDiagnosticString, true);
                    }
                    catch (Exception) { } // ignore errors
                }

                return _diagnosticLoggingLevel.Value;
            }
        }

        public static void AppendLabelledLine(StringBuilder builder, string label, string message)
        {
            builder.AppendFormat("{0}: {1}", label, message);
            builder.AppendLine();
        }

        private const int __fullWidth = 80;
        private static void AppendBanner(StringBuilder builder, string message, char bannerCharacter)
        {
            const char oneSpace = ' ';
            if (String.IsNullOrWhiteSpace(message))
            {
                builder.Append(bannerCharacter, __fullWidth);
            }
            else
            {
                int messageLength = message.Length + 2;
                int bannerFirstSegmentLength = (__fullWidth - messageLength) / 2;
                int bannerSecondSegnmentLength = __fullWidth - bannerFirstSegmentLength - messageLength;
                if (bannerFirstSegmentLength > 0)
                {
                    builder.Append(bannerCharacter, bannerFirstSegmentLength);
                    builder.Append(oneSpace);
                }
                builder.Append(message);
                if (bannerSecondSegnmentLength > 0)
                {
                    builder.Append(oneSpace);
                    builder.Append(bannerCharacter, bannerSecondSegnmentLength);
                }
            }
            builder.AppendLine();
        }

        public enum TraceType
        {
            Begin,
            End,
            Watch,
            Warning
        }
    }
}
