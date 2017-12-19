using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Contexts;
using Plastiline.PostOffice.Attachments;
using Plastiline.PostOffice.Configuration;
using Plastiline.PostOffice.Sender;
using Plastiline.PostOffice.Templating;
using Plastiline.PostOfficeApi;

namespace PostUfficeRunner {
    internal class Program {
        public static void Main(string[] args) {
            PostOfficeService pos = new PostOfficeService(new HardcodedSmtConfigurationprovider(), new HardcodedTemplateProvider(),
                new HardcodedAttachmentProvider());
            pos.SendMail(new SendLetterCommand {
                To = new [] { "pietro1@codiceplastico.com" },
                Data = new Dictionary<string, string>(),
                LetterTemplate = "HtmlSample"
            });
            
            pos.SendMail(new SendLetterCommand {
                To = new [] { "pietro2@codiceplastico.com" },
                Data = new Dictionary<string, string>(),
                LetterTemplate = "HtmlSample",
                Attachments = new[] {
                    new Attachment {
                        Id = "OriginalFileName.data",
                        AttachmentName = "MyAttachment.txt"
                    }
                }
            });
        }
    }

    internal class HardcodedAttachmentProvider : AttachmentProvider {
        public Stream ProvideAttachment(string attachmentId) {
            //return File.OpenRead(@"C:\Users\pie\Desktop\Trello-Embrace-Remote-Work-Ultimate-Guide.pdf");
            string content = $"Attachment with id {attachmentId}";
            return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
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