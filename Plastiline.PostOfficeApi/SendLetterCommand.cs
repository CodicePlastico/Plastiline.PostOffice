using System.Collections.Generic;

namespace Plastiline.PostOfficeApi
{
    public class SendLetterCommand
    {
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public IEnumerable<string> Ccn { get; set; }
        public string LetterTemplate { get; set; }
        public IDictionary<string, string> Data { get; set; }
        public IList<Attachment> Attachments { get; set; }
    }

    public class Attachment {
        public string Id { get; set; }
        public string AttachmentName { get; set; }
    }
}
