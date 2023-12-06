namespace OxygenMeasurementApi;

public class SmtpConfigLoader
{
    private IConfiguration Configuration { get; }

    public SmtpConfig? SmtpConfig { get; set; }

    public SmtpConfigLoader(IConfiguration configuration)
    {
        Configuration = configuration;
        LoadSmtpConfig();
    }

    private void LoadSmtpConfig()
    {
        var mailConfigSection = Configuration.GetSection("SmtpConfig");

        SmtpConfig = mailConfigSection.Get<SmtpConfig>();
    }
}