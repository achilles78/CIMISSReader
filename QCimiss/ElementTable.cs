namespace QCimiss
{
    using System;

    public class ElementTable
    {
        private ElementTableId _id;

        public ElementTableId id
        {
            get => 
                this._id;
            set
            {
                this._id = value;
            }
        }
    }
}

