
using System;


namespace Back_Communication
{
    [Serializable]
    public class Communication
    {
        private Instructions _instruction;
        private Object _content;

        public Communication(Instructions instruction, Object content)
        {
            this.instruction = instruction;
            this.content = content;
        }


        public Instructions instruction
        {
            get { return _instruction; }
            set { _instruction = value; }
        }
        public Object content
        {
            get { return _content; }
            set { _content = value; }
        }

        public override string ToString()
        {
            return "instruction : " + instruction + "\ncontent : " + content.ToString();
        }
    }
}