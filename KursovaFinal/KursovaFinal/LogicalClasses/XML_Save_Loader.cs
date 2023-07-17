using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace KursovaFinal
{
    public static class XML_Save_Loader
    {
        //public static List<Person> Load(string fileName)
        //{
        //    XmlSerializer formatter = new XmlSerializer(typeof(List<Person>), new Type[] { typeof(Client), typeof(Admin), typeof(Worker) });
        //    using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        //    {
        //        return formatter.Deserialize(fs) as List<Person>;
        //    }
        //}

        //public static void Save(string fileName, List<Person> persons)
        //{
        //    XmlSerializer formatter = new XmlSerializer(typeof(List<Person>), new Type[] { typeof(Client), typeof(Admin), typeof(Worker) });
        //    using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
        //    {
        //        formatter.Serialize(fs, persons);
        //    }
        //}
        public static List<Admin> LoadAdmin(string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Admin>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return formatter.Deserialize(fs) as List<Admin>;
            }
        }
        public static List<Worker> LoadWorker(string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Worker>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return formatter.Deserialize(fs) as List<Worker>;
            }
        }

        public static List<Client> LoadClient(string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Client>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return formatter.Deserialize(fs) as List<Client>;
            }
        }
       

        public static void SaveAdmin(string fileName, List<Admin> admins)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Admin>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, admins);
            }
        }

        public static void SaveWorker(string fileName, List<Worker> workers)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Worker>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, workers);
            }
        }

        public static void SaveClient(string fileName, List<Client> clients)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Client>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, clients);
            }
        }
        public static List<ServiceBasePrice> LoadServicePrices(string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<ServiceBasePrice>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return formatter.Deserialize(fs) as List<ServiceBasePrice>;
            }
        }

        public static void SaveServicePrices(string fileName, List<ServiceBasePrice> servicePrices)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<ServiceBasePrice>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, servicePrices);
            }
        }

        public static List<ServiceFinalCheck> LoadServiceFinals(string fileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<ServiceFinalCheck>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                return formatter.Deserialize(fs) as List<ServiceFinalCheck>;
            }
        }

        public static void SaveServiceFinals(string fileName, List<ServiceFinalCheck> serviceFinal)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<ServiceFinalCheck>));
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, serviceFinal);
            }
        }
    }
}
