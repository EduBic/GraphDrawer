using System;

namespace GraphDrawer
{
    public class ConnectionViewModel : BaseViewModel
    {
        private int numInput;
        private int numOutput;
        private string type;

        public int NumInput 
        { 
            get { return numInput; } 
            set { SetProperty(ref numInput, value); }
        }

        public int NumOutput 
        {
            get { return numOutput; }
            set { SetProperty(ref numOutput, value); }
        }

        public string Type 
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        public void Reset()
        {
            NumInput = 0;
            NumOutput = 0;
            Type = "Water";
        }

        internal ConnectionViewModel Clone()
        {
            var res = new ConnectionViewModel
            {
                numInput = numInput,
                numOutput = numOutput,
                type = type
            };

            return res;
        }
    }
}