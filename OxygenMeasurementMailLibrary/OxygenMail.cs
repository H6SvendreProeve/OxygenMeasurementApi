using MimeKit;

namespace OxygenMeasurementMailLibrary;

public class OxygenMail
{
    public OxygenMail(MailHandler mailHandler)
    {
        MailHandler = mailHandler;
    }

    private MailHandler MailHandler { get; }

    public enum MailOptions
    {
        Error,
        Harvest
    }


    public void SendMailToSubscribes(List<string> listOfNames, MailOptions option = MailOptions.Harvest)
    {
    
        if (option == MailOptions.Harvest)
        {
            MailHandler.SendMailToAll(SetSubjectAndMessageHarvestClamsMsg(), listOfNames);
        }
        else
        {
            MailHandler.SendMailToAll(SetSubjectAndMessageErrorMsg(), listOfNames);
        }
    }

    private static MimeMessage SetSubjectAndMessageErrorMsg()
    {
        MimeMessage msg = new MimeMessage();
        msg.Subject = "An error occurred with your system";
        msg.Body = new TextPart("plain")
        {
            Text = $"Hallo, \n " +
                   $"We have detected an error with your system, please go and have a look at it. \n\n " +
                   $"From the system automatic mail notifier."
        };
        return msg;
    }

    private static MimeMessage SetSubjectAndMessageHarvestClamsMsg()
    {
        MimeMessage msg = new MimeMessage();
        msg.Subject = "Time to Harvest";
        msg.Body = new TextPart("plain")
        {
            Text = $"Hallo,\n" +
                   $"It's time to harvest the clams.\n" +
                   $"here is a quick guideline and bullet points of things than you need to do after the harvest is done.\n" +
                   $"\t 1). Order new clams.\n" +
                   $"\t 2). Wait for the clams to arrived.\n" +
                   $"\t 3). Fill the water tank at the clams cage.\n" +
                   $"\t 4). Add seasalt to the water.\n"+
                   $"\t 5). Add the clams to the water.\n\n\n" +
                   $"From the automatic mail notifier"
        };
        return msg;
    }
}