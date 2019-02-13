namespace QCimiss
{
    using System;

    public class Parainfo
    {
        private string _attId;
        private string _id;
        private string _name;
        private string _type;

        public string attId
        {
            get => 
                this._attId;
            set
            {
                this._attId = value;
            }
        }

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

        public string type
        {
            get => 
                this._type;
            set
            {
                if (value == "N")
                {
                    this._type = "是";
                }
                else
                {
                    this._type = "否";
                }
            }
        }
    }
}

