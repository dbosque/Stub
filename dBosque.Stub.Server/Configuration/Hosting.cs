namespace dBosque.Stub.Server.Configuration
{
    public class Hosting
    {
        public string Uri { get; set; }

        public Endpoint WebApi { get; set; }

        public Endpoint ConfigurationApi { get; set; }

        public Endpoint SoapApi { get; set; }
    }


}
