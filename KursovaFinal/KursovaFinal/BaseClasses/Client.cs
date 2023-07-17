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
    public class Client : Person
    {
        public Client(string name, string password, string numberphone, int id) : base(name, password, numberphone, id)
        {

        }
        public Client()
        {
        }

        public override string ReturnName()
        {
            return Name;
        }
        public override string ReturnPassword()
        {
            return Password;
        }
        public override void SetName(string name)
        {
            Name = name;
        }
        public override void SetPassword(string password)
        {
            Password = password;
        }
        public override void SetNumberPhone(string numberphone)
        {
            NumberPhone = numberphone;
        }
        public override string ReturnNumberPhone()
        {
            return NumberPhone;
        }
        public override int ReturnID()
        {
            return ID;
        }
        public override void SetID(int id)
        {
            ID = id;
        }
    }
}
