using dBosque.Stub.WCF.Middleware;
using System.ServiceModel.Channels;

namespace Microsoft.AspNetCore.Builder
{
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SOAPEndpointExtensions
    {
        public static IApplicationBuilder UseSOAPEndpoint(this IApplicationBuilder builder, Binding binding)
        {
            var encoder = binding.CreateBindingElements().Find<MessageEncodingBindingElement>()?.CreateMessageEncoderFactory().Encoder;
            return builder.UseMiddleware<SoapEndpointMiddleware>(encoder);
        }
    }
}