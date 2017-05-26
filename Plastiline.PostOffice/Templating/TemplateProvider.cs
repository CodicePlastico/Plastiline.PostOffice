namespace Plastiline.PostOffice.Templating
{
    public interface TemplateProvider
    {
        EmailTemplate ProvideTemplate(string templateName);
    }
}
