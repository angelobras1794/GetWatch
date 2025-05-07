using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.SupportTickets;

namespace GetWatch.Services.Tickets
{
    public class SupportTicket : ISupportTicket
    {
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsResolved { get; set; } = false;

        private List<string> Comments { get; set; }
        public Guid Id { get; set; } 

        public SupportTicket(Guid id = new Guid())
        {
            Comments = new List<string>();
            Id = id;
        }

        public void updateDescription(string description) => Description = description;
        public void updateSubject(string subject) => Subject = subject;
        public void setResolved() => IsResolved = true;

        public void AddComment(string comment) => Comments.Add(comment);
        public List<string> GetComments() => Comments;
        
    
        
    }
}