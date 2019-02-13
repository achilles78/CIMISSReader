namespace QCimiss
{
    using System;

    public class ElementTableId
    {
        private string _eleName;
        private string _eleUnit;
        private string _userEleCode;

        public string eleName
        {
            get => 
                this._eleName;
            set
            {
                this._eleName = value;
            }
        }

        public string eleUnit
        {
            get => 
                this._eleUnit;
            set
            {
                this._eleUnit = value;
            }
        }

        public string userEleCode
        {
            get => 
                this._userEleCode;
            set
            {
                this._userEleCode = value;
            }
        }
    }
}

