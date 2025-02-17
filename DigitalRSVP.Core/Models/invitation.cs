using System;

namespace DigitalRSVP.Core.Models
{
    public class Invitation
    {
        private Guid _id;
        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        private Guid _eventId;
        public Guid EventId
        {
            get
            {
                return this._eventId;
            }
            set
            {
                this._eventId = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        private bool _weddingParty;
        public bool WeddingParty
        {
            get
            {
                return this._weddingParty;
            }
            set
            {
                this._weddingParty = value;
            }
        }

        private bool _designatedSeating;
        public bool DesignatedSeating
        {
            get
            {
                return this._designatedSeating;
            }
            set
            {
                this._designatedSeating = value;
            }
        }

        private string? _noteToInvitee;
        public string? NoteToInvitee
        {
            get
            {
                return this._noteToInvitee;
            }
            set
            {
                this._noteToInvitee = value;
            }
        }

        private DateTime _createdDate;
        public DateTime Created_Date
        {
            get
            {
                return this._createdDate;
            }
            set
            {
                if (value > new DateTime(2025, 1, 1))
                {
                    this._createdDate = value;
                }
            }
        }

        private DateTime _updatedDate;
        public DateTime Updated_Date
        {
            get
            {
                return this._updatedDate;
            }
            set
            {
                if (value > new DateTime(2025, 1, 1))
                {
                    this._createdDate = value;
                }
            }
        }
    }
}
