using System.Collections.Generic;
using Plastiline.PostOffice.Configuration;
using Plastiline.PostOffice.Sender;
using Plastiline.PostOffice.Templating;
using Plastiline.PostOfficeApi;

namespace PostUfficeRunner {
    internal class Program {
        public static void Main(string[] args) {
            PostOfficeService pos = new PostOfficeService(new HardcodedSmtConfigurationprovider(), new HardcodedTemplateProvider());
            pos.SendMail(new SendLetterCommand {
                To = new [] { "pietro1@codiceplastico.com" },
                Data = new Dictionary<string, string>(),
                LetterTemplate = "HtmlSample"
            });
            
            pos.SendMail(new SendLetterCommand {
                To = new [] { "pietro2@codiceplastico.com" },
                Data = new Dictionary<string, string>(),
                LetterTemplate = "HtmlSample"
            });
        }
    }

    internal class HardcodedSmtConfigurationprovider : SmtpConfigurationProvider {
        public SmtpConfiguration ProvideConfiguration() {
            return new SmtpConfiguration {
                Host = "localhost",
                Port = 25,
                EnableSsl = false,
                RequiresAuthentication = false
            };
        }
    }

    internal class HardcodedTemplateProvider : TemplateProvider {
        public EmailTemplate ProvideTemplate(string templateName) {
            return new EmailTemplate {
                From = "test@postoffice.com",
                Subject = "Email test",
                Body = "Hello, <i>World</i>!",
                HasHtmlContent = true
            };
        }
    }
}