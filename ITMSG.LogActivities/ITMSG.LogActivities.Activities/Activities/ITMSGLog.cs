using System;
using System.Activities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITMSG.LogActivities.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;

namespace ITMSG.LogActivities.Activities
{
    [LocalizedDisplayName(nameof(Resources.ITMSGLog_DisplayName))]
    [LocalizedDescription(nameof(Resources.ITMSGLog_Description))]
    public class ITMSGLog : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.ITMSGLog_Message_DisplayName))]
        [LocalizedDescription(nameof(Resources.ITMSGLog_Message_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<string> Message { get; set; }

        #endregion
        public enum selectLogLevel
        {
            Info,
            Warn,
            Error
        }

        [LocalizedDisplayName(nameof(Resources.ITMSGLog_LogLevel_DisplayName))]
        [LocalizedDescription(nameof(Resources.ITMSGLog_LogLevel_Description))]
        public selectLogLevel LogLevel { get; set; }

        [LocalizedDisplayName(nameof(Resources.ITMSGLog_WriteConsole_DisplayName))]
        [LocalizedDescription(nameof(Resources.ITMSGLog_WriteConsole_Description))]
        public bool WriteConsole { get; set; } = true;


        #region Constructors

        public ITMSGLog()
        {
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (Message == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Message)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Inputs
            var logMessage = Message.Get(context);
            var logLevel = this.LogLevel.ToString();
            var write_console = this.WriteConsole;

            string refinedLogLevel = "[" + logLevel + "] ";

            // 로그 메세지
            string logMsg = DateTime.Now.ToString("HH:mm:ss") + " => " + refinedLogLevel + logMessage + "\r";

            // 로그 경로 파일 읽기
            string logConfigPath = Environment.CurrentDirectory + "\\User\\Config\\LogConfig.txt";
            string logPath = System.IO.File.ReadAllText(logConfigPath);

            // 텍스트 쓰기
            System.IO.File.AppendAllText(logPath, logMsg, Encoding.Default);

            if (write_console)
            {
                Console.WriteLine(logMsg);
            }

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

