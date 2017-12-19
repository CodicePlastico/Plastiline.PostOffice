namespace Plastiline.PostOffice.Templating
{
    public class EmailTemplate
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool HasHtmlContent { get; set; } = false;
    }
}
