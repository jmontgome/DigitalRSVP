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
    }
}
