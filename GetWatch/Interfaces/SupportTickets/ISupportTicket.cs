using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Interfaces.SupportTickets
{
    public interface ISupportTicket
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; }
        public Guid Id { get; set; }


        public void updateDescription(string description);
        public void updateSubject(string subject);
        public void setResolved();

        public void AddComment(string comment);
        public List<string> GetComments();
        
    }
}