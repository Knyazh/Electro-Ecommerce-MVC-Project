namespace E_Commerce_Platform.Contracts;

public class MessageDto
{
    public string Subject { get; set; }
    public string Content { get; set; }
    public List<string> Receipents { get; set; }

    public MessageDto() { }

    public MessageDto(string subject, string content, List<string> receipents)
    {
        Subject = subject;
        Content = content;
        Receipents = receipents;
    }


   
}
