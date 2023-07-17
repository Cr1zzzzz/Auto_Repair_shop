using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KursovaFinal.BaseClasses;

namespace KursovaFinal
{
    [Serializable]
    public class ServiceFinalCheck : Idateble, Iidentificationable
    {
        public Worker worker;
        public Client client;
        public string description;
        public List<ServiceBasePrice> check;
        public DateTime date;
        public int ID;
        public int finalPrice;
        public bool isEnded;

        public ServiceFinalCheck(Worker worker, Client client, string description, List<ServiceBasePrice> check, DateTime date, int ID, int finalPrice, bool isended)
        {
            this.worker = worker;
            this.client = client;
            this.description = description;
            this.check = check;
            this.date = date;
            this.ID = ID;
            this.finalPrice = finalPrice;
            this.isEnded = isended;
        }

        public ServiceFinalCheck()
        {
        }

        public void SetWorker(Worker worker)
        {
            this.worker = worker;
        }
        public void SetClient(Client client)
        {
            this.client = client;
        }
        public void SetDescription(string description)
        {
            this.description = description;
        }

        public void SetCheck(List<ServiceBasePrice> check)
        {
            this.check = check;
        }

        public void SetDate(DateTime date)
        {
            this.date = date;
        }
        public void SetID(int ID)
        {
            this.ID = ID;
        }
        public void SetFinalPrice(int finalPrice)
        {
            this.finalPrice = finalPrice;
        }

        public void SetIsEnded(bool isEnded)
        {
            this.isEnded = isEnded;
        }
        public Worker ReturnWorker()
        {
            return worker;
        }
        public Client ReturnClient()
        {
            return client;
        }
        public string ReturnDescription()
        {
            return description;
        }
        public List<ServiceBasePrice> ReturnCheck()
        {
            return check;
        }
        public DateTime ReturnDate()
        {
            return date;
        }
        public int ReturnID()
        {
            return ID;
        }
        public int ReturnFinalPrice()
        {
            return finalPrice;
        }

        public bool ReturnIsEnded()
        {
            return isEnded;
        }
    }
}
