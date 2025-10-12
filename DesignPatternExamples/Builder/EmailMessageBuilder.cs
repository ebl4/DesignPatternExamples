public class EmailMessageBuilder
{
    private readonly EmailMessage _email = new();

    public EmailMessageBuilder From(string from)
    {
        _email.From = from;
        return this;
    }

    public EmailMessageBuilder To(params string[] recipients)
    {
        _email.To.AddRange(recipients);
        return this;
    }

    public EmailMessageBuilder Cc(params string[] recipients)
    {
        _email.Cc.AddRange(recipients);
        return this;
    }

    public EmailMessageBuilder WithSubject(string subject)
    {
        _email.Subject = subject;
        return this;
    }

    public EmailMessageBuilder WithBody(string body, bool isHtml = false)
    {
        _email.Body = body;
        _email.IsHtml = isHtml;
        return this;
    }

    public EmailMessageBuilder WithAttachment(string fileName, byte[] content, string contentType)
    {
        _email.Attachments.Add(new Attachment 
        { 
            FileName = fileName, 
            Content = content, 
            ContentType = contentType 
        });
        return this;
    }

    public EmailMessageBuilder WithPriority(Priority priority)
    {
        _email.Priority = priority;
        return this;
    }

    public EmailMessage Build()
    {
        // Validações
        if (string.IsNullOrEmpty(_email.From))
            throw new InvalidOperationException("Sender is required");
        
        if (!_email.To.Any())
            throw new InvalidOperationException("At least one recipient is required");

        return _email;
    }
}