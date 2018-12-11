using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary1
{
    public abstract class AbstractUser
    {
        private int name;
        private int sex;
        private int avatar;
        private int login;
        private int parol;

        public int status
        {
            get => default(int);
            set
            {
            }
        }
    }
}