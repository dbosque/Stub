using dBosque.Stub.Plugin.Runner.Properties;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace dBosque.Stub.Plugin.Runner
{
    public class LogMessage
    {
        
        private static Image ErrorImage => new Bitmap(Resources.ic_highlight_off_black_24dp, 16, 16);
        private static Image InfoImage => new Bitmap(Resources.ic_info_outline_black_24dp, 16, 16);
        private static Image WarningImage => new Bitmap(Resources.ic_report_problem_black_24dp, 16, 16);
        private static Image MessageImage => new Bitmap(Resources.ic_mail_black_24dp, 16, 16);
        public LogLevel Severity { get; set; }
        public string Text { get; set; }

        public Image Image
        {
            get
            {
                switch (Severity)
                {
                    case LogLevel.Error:
                        return ErrorImage;
                    case LogLevel.Information:
                        return InfoImage;
                    case LogLevel.Warning:
                        return WarningImage;                    
                    default:
                        return MessageImage;                 
                }
            }
            set
            { 
                //
            }
        }
    }
}
