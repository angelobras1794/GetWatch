using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.SupportTickets;

namespace GetWatch.Services.Tickets
{
    public class SupportTicketBuilder : ISupportTicketBuilder
    {
        private string _subject = string.Empty;
        private string _description = string.Empty;
        private bool _isResolved = false;
        private Guid _id;

        public ISupportTicketBuilder SetSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public ISupportTicketBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public ISupportTicketBuilder SetResolved(bool isResolved)
        {
            _isResolved = isResolved;
            return this;
        }

        public ISupportTicketBuilder SetId(Guid id)
        {
            _id = id;
            return this;
        }

        public ISupportTicket Build()
        {
            return new SupportTicket(_id)
            {
                Subject = _subject,
                Description = _description,
                IsResolved = _isResolved
            };
        }
    
        
    }
}