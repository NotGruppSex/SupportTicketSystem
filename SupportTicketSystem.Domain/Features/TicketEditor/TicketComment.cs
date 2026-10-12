using System;

namespace SupportTicketSystem.Domain.Features.TicketEditor;

public class TicketComment
{
    public string CommentText { get; set; } = string.Empty;
    public DateTime CommentDate { get; set; }

    //Parameterlös konstruktor för deserialisering av Json - annars krash
    public TicketComment() { }

    // Skapar en ny kommentar med text samt aktuellt datum och tid.
    public TicketComment(string commentText)
    {
        CommentText = commentText;
        CommentDate = DateTime.Now;
    }
}
