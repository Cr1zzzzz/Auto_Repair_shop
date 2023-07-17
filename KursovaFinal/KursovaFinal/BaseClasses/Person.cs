using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace KursovaFinal
{
    [Serializable]
    public abstract class Person
    {
        public string Name;
        public string Password;
        public string NumberPhone;
        public int ID;
        public Person(string name, string password, string numberphone, int id)
        {
            Name = name;
            Password = password;
            NumberPhone = numberphone;
            ID = id;
        }

        public Person()
        { 
        }
        public abstract string ReturnName();
        public abstract void SetName(string name);

        public abstract string ReturnPassword();
        public abstract void SetPassword(string password);

        public abstract int ReturnID();
        public abstract void SetID(int id);

        public abstract string ReturnNumberPhone();
        public abstract void SetNumberPhone(string numberphone);
    }
}
