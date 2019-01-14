using System.IO;
using Microsoft.Extensions.Options;
using Models;
using Models.DB;

namespace webshop_backend.html.activation
{
    public class ActivationToCSharp
    {
        public static string Activation(User user, IOptions<Urls> urlSettings) {
            var body = @"
    <h1>Hello {{title}} {{name}}</h1>
    <p>Click the button below to activate your account.</p>
    <a href='{{url}}/auth/activate/{{id}}' style='display: inline-block;font-weight: 400;text-align: center;white-space: nowrap;vertical-align: middle;
                                -webkit-user-select: none;-moz-user-select: none;-ms-user-select: none;user-select: none;
                                border: 1px solid transparent;padding: .375rem .75rem;font-size: 1rem;line-height: 1.5;border-radius: .25rem;
                                transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,
                                box-shadow .15s ease-in-out;color: #fff;background-color: #007bff;border-color: #007bff;'>
        Activate account
    </a>
";
            // using (var reader = File.OpenText(@"html/activation/activation.html"))
            // {
            //     body = reader.ReadToEnd();
            // }
            body = body.Replace("{{url}}", urlSettings.Value.BackendUrl);
            body = body.Replace("{{id}}", user.id.ToString());
            body = body.Replace("{{name}}", user.name);
            body = body.Replace("{{title}}", user.approach != "" ? user.approach : "");

            return body;
        }
    }
}