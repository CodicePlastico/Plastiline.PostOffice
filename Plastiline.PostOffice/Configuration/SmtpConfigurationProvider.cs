namespace Plastiline.PostOffice.Configuration
{
    public interface SmtpConfigurationProvider
    {
        SmtpConfiguration ProvideConfiguration();
    }
}
