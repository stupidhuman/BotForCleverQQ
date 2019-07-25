namespace DataBase
{
    public enum ParamType
    {
        Text = 0,
        File = 1,
        DateTime = 2,
        Integer = 3
    }

    class DataBaseParam
    {
        private string paramname;
        private ParamType paramtype;
        private int size;
        private object value;

        public string ParamName
        {
            get { return this.paramname; }
            set { this.paramname = value; }
        }

        public ParamType ParamType
        {
            get { return this.paramtype; }
            set { this.paramtype = value; }
        }

        public int Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public DataBaseParam()
        {
        }

        public DataBaseParam(string paramName, ParamType type, int size, object value)
        {
            this.paramname = paramName;
            this.paramtype = type;
            this.size = size;
            this.value = value;
        }

    }
}
