using System.IO;
using Models.DB;

namespace webshop_backend.html.activation
{
    public class ActivationToCSharp
    {
        public static string Activation(User user) {
            var body = "";
            using (var reader = File.OpenText(@"html/activation/activation.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{id}", user.id.ToString());
            body = body.Replace("{name}", user.name);
            body = body.Replace("{title}", user.approach != "" ? user.approach : "");

            return body;
        }
    }
}