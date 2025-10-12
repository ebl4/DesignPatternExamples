
// Uso do Builder
public class BuilderExample
{
    public static void Execute()
    {
        var email = new EmailMessageBuilder()
            .From("sender@example.com")
            .To("recipient1@example.com", "recipient2@example.com")
            .Cc("manager@example.com")
            .WithSubject("Important Notification")
            .WithBody("<h1>Hello World</h1>", true)
            .WithPriority(Priority.High)
            .WithAttachment("report.pdf", new byte[1024], "application/pdf")
            .Build();

        Console.WriteLine($"Email built with {email.To.Count} recipients");
    }
}