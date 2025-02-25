namespace DigitalRSVP.Core.Models
{
    public class Event
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

        //private string _contactEmail;
        //public string ContactEmail
        //{
        //    get
        //    {
        //        return this._contactEmail;
        //    }
        //    set
        //    {
        //        this._contactEmail = value;
        //    }
        //}

        private DateTime _expiryDate;
        public DateTime ExpiryDate
        {
            get
            {
                return this._expiryDate;
            }
            set
            {
                this._expiryDate = value;
            }
        }
    }
}
