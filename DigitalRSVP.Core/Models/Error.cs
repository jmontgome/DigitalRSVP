using Newtonsoft.Json;

namespace DigitalRSVP.Core.Models
{
    public class Error
    {
        private Guid _inviteId;
        public Guid InviteId
        {
            get
            {
                return this._inviteId;
            }
            set
            {
                this._inviteId = value;
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return this._errorMessage;
            }
            set
            {
                this._errorMessage = value;
            }
        }

        private Invitation _invite;
        public Invitation Invite
        {
            get
            {
                return this._invite;
            }
            set
            {
                this._invite = value;
            }
        }

        private RSVP _rsvp;
        public RSVP RSVP
        {
            get
            {
                return this._rsvp;
            }
            set
            {
                this._rsvp = value;
            }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get
            {
                return this._dateTime;
            }
            set
            {
                this._dateTime = value;
            }
        }

        public override string ToString()
        {
            return $"{JsonConvert.SerializeObject(this).ToString()}";
        }
    }
}
