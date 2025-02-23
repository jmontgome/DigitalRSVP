namespace DigitalRSVP.Core.Models
{
    public enum Age
	{
		INFANT = 0,
		MINOR = 1,
		ADULT = 2
	}
	
	public class Guest
    {
		private Guid _id;
		public Guid Id 
		{
			get 
			{
				return _id;
			}
			set 
			{
				this._id = value;
			}
		}
		
		private Guid _rsvpId;
		public Guid RSVPId
		{
			get 
			{
				return this._rsvpId;
			}
			set 
			{
				this._rsvpId = value;
			}
		}
		
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
            }
        }
		
		private Age _age;
		public Age Age
		{
			get
			{
				return this._age;
			}
			set 
			{
				this._age = value;
			}
		}

        private bool _attendingWedding;
        public bool AttendingWedding
        {
            get
            {
                return this._attendingWedding;
            }
            set
            {
                this._attendingWedding = value;
            }
        }

        private bool _attendingReception;
        public bool AttendingReception
        {
            get
            {
                return this._attendingReception;
            }
            set
            {
                this._attendingReception = value;
            }
        }
    }
}
