
namespace dBosque.Stub.Services.Types
{
    public static class ContentTypes
    {
        public static string TextPlain = "text/plain";

        public static string TextHtml = "text/html";

        public static string TextXml = "text/xml";

        public static string ApplicationJson = "application/json";

        public static string ApplicationOctetStream = "application/octet-stream";

        public static string ApplicationXml = "application/xml";

        public static string ApplicationSoapXml = "application/soap+xml";

        public static string All = "*/*";

        public static string First(string value)
        {
            return value?.Split(';')[0];
        }
    }
}
