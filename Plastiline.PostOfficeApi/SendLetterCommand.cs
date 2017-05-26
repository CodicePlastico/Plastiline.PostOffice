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
        public IList<string> Attachments { get; set; }
    }
}
