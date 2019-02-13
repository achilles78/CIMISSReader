namespace QCimiss
{
    using System;

    public class paramhelp
    {
        private string _codeTableId;
        private string _isCodeParam;
        private string _valueFormat;

        public string codeTableId
        {
            get => 
                this._codeTableId;
            set
            {
                this._codeTableId = value;
            }
        }

        public string isCodeParam
        {
            get => 
                this._isCodeParam;
            set
            {
                this._isCodeParam = value;
            }
        }

        public string valueFormat
        {
            get => 
                this._valueFormat;
            set
            {
                this._valueFormat = value;
            }
        }
    }
}

