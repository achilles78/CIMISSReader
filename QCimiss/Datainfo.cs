namespace QCimiss
{
    using System;

    public class Datainfo
    {
        private string _id;
        private string _name;

        public string id
        {
            get => 
                this._id;
            set
            {
                this._id = value;
            }
        }

        public string name
        {
            get => 
                this._name;
            set
            {
                this._name = value;
            }
        }
    }
}

