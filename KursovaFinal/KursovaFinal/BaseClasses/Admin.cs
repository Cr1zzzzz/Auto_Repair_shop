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
    public class Admin : Person
    {
        
        public int Experience;
        public Admin(string name, string password, string numberphone, int id, int experience) : base(name, password, numberphone, id)
        {
            Experience = experience;
        }
        public Admin()
        {
        }

        public int ReturnExperience()
        {
            return Experience;
        }

        public void SetExperience(int experience)
        {
            Experience = experience;
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
