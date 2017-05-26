using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Plastiline.PostOfficeApi;
using Plastiline.PostOffice.Configuration;
using Plastiline.PostOffice.Templating;

namespace Plastiline.PostOffice.Sender
{
    public class PostOfficeService
    {
        private readonly SmtpConfigurationProvider _configurationProvider;
        private readonly TemplateProvider _templateProvider;
        //private readonly IAttachmentProvider _attachmentProvider;

        public PostOfficeService(SmtpConfigurationProvider configurationProvider, TemplateProvider templateProvider/*, IAttachmentProvider attachmentProvider*/)
        {
            _templateProvider = templateProvider;
            //_attachmentProvider = attachmentProvider;
            _configurationProvider = configurationProvider;
        }

        public void SendMail(SendLetterCommand command)
        {
            EmailTemplate template = _templateProvider.ProvideTemplate(command.LetterTemplate);
            SmtpClient client = ConfigureSmtpClient();
            MailMessage message = BuildEmailMessage(template, command);
            client.Send(message);
        }

        private MailMessage BuildEmailMessage(EmailTemplate template, SendLetterCommand command)
        {
            MailMessage message = new MailMessage
            {
                Subject = Interpolate(template.Subject, command.Data),
                Body = Interpolate(template.Body, command.Data),
                From = new MailAddress(template.From)
            };
            
            foreach (string to in command.To)
            {
                message.To.Add(new MailAddress(to));
            }
            
            //if (command.Attachments != null)
            //{
            //    foreach (string attachment in command.Attachments)
            //    {
            //        Stream stream = _attachmentProvider.ProvideAttachment(attachment);
            //        if (stream != null)
            //        {
            //            message.Attachments.Add(new Attachment(stream, attachment));
            //        }
            //    }
            //}
            return message;
        }

        private string Interpolate(string src, IDictionary<string, string> pairs)
        {
            Regex re = new Regex("\\{\\w*\\}");
            MatchCollection matches = re.Matches(src);
            foreach (var match in matches)
            {
                src = src.Replace(match.ToString(), match.ToString().ToLower());
            }

            foreach (KeyValuePair<string, string> pair in pairs)
            {
                src = src.Replace("{" + pair.Key.ToLower() + "}", pair.Value);
            }

            return src;
        }

        private SmtpClient ConfigureSmtpClient()
        {
            SmtpConfiguration configuration = _configurationProvider.ProvideConfiguration();
            SmtpClient client = new SmtpClient
            {
                Port = configuration.Port,
                Host = configuration.Host,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
            if (configuration.RequiresAuthentication)
            {
                client.Credentials = new NetworkCredential(configuration.Username, configuration.Password);
            }

            return client;
        }
    }
}
