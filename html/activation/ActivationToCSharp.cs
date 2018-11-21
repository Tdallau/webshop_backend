using System.IO;
using Microsoft.Extensions.Options;
using Models;
using Models.DB;

namespace webshop_backend.html.activation
{
    public class ActivationToCSharp
    {
        public static string Activation(User user, IOptions<Urls> urlSettings) {
            var body = "";
            using (var reader = File.OpenText(@"html/activation/activation.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{{url}}", urlSettings.Value.BackendUrl);
            body = body.Replace("{{id}}", user.id.ToString());
            body = body.Replace("{{name}}", user.name);
            body = body.Replace("{{title}}", user.approach != "" ? user.approach : "");

            return body;
        }
    }
}