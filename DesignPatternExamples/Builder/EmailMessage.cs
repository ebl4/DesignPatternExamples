// Produto complexo
public class EmailMessage
{
    public string From { get; set; } = string.Empty;
    public List<string> To { get; } = new();
    public List<string> Cc { get; } = new();
    public List<string> Bcc { get; } = new();
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtml { get; set; }
    public List<Attachment> Attachments { get; } = new();
    public Priority Priority { get; set; } = Priority.Normal;
}

public enum Priority { Low, Normal, High, Urgent }