namespace dBosque.Stub.Services.ExternalReferenceResolvers
{
    /// <summary>
    /// External resolver factory
    /// </summary>
    public class ExternalResolverFactory
    {
        /// <summary>
        /// Create a new instance of an external resolver
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ExternalResolverBase Create(string name)
        {
            switch (name?.ToLower())
            {
                case "wsdl":
                    return new WSDLResolver();
                default:
                    return new SwaggerResolver();
            }
        }
    }
}
