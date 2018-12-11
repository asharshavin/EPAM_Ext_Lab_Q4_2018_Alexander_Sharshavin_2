using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public class Image : Attachment
    {
        public int picture
        {
            get => default(int);
            set
            {
            }
        }
    }

    public class Music : Attachment
    {
        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}