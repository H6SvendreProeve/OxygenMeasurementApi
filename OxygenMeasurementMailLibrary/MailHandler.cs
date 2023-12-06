using MailKit.Security;
using MimeKit;

namespace OxygenMeasurementMailLibrary;

public class MailHandler
{
    private string SmtpUser { get; set; }
    private string SmtpPassword { get; set; }

    private string Host { get; set; }

    private int Port { get; set; }
    
    public MailHandler(string smtpUser, string smtpPassword, string host, int port)
    {
        SmtpUser = smtpUser;
        SmtpPassword = smtpPassword;
        Host = host;
        Port = port;
    }
    
    public void SendMailToAll(MimeMessage message, List<string> listOfNames)
    {

        message.From.Add(new MailboxAddress("Mail fra oxygen measurement system", SmtpUser));

        MimeMessage messageWithNameAdded = AddAllRecipients(message, listOfNames);

        MailKit.Net.Smtp.SmtpClient smtpClient = new();
        try
        {
            smtpClient.Connect(Host, Port, SecureSocketOptions.StartTls);
            smtpClient.Authenticate(SmtpUser, SmtpPassword);
            smtpClient.Send(messageWithNameAdded);
        }
       
        finally
        {
            smtpClient.Disconnect(true);
            smtpClient.Dispose();
        }
    }

    private static MimeMessage AddAllRecipients(MimeMessage message, List<string> listOfNames)
    {
        foreach (var name in listOfNames)
        {
            message.To.Add(MailboxAddress.Parse(name));
        }

        return message;
    }
}