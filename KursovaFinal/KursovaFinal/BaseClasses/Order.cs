using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KursovaFinal.BaseClasses;

namespace KursovaFinal.BaseClasses
{
    [Serializable]
    public class Order : Idateble, Iidentificationable
    {      
        public Client client;       
        public List<ServiceBasePrice> check;
        public DateTime date;
        public int IDofOrder;
        public int finalPrice;

        public Order(Client client, List<ServiceBasePrice> check, DateTime date, int IDofOrder, int finalPrice)
        {           
            this.client = client;           
            this.check = check;
            this.date = date;
            this.IDofOrder = IDofOrder;
            this.finalPrice = finalPrice;           
        }

        public Order()
        {
        }

        public void SetClient(Client client)
        {
            this.client = client;
        }

        public void SetCheck(List<ServiceBasePrice> check)
        {
            this.check = check;
        }

        public void SetDate(DateTime date)
        {
            this.date = date;
        }
        public void SetID(int IDofOrder)
        {
            this.IDofOrder = IDofOrder;
        }
        public void SetFinalPrice(int finalPrice)
        {
            this.finalPrice = finalPrice;
        }

        public Client ReturnClient()
        {
            return client;
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
            return IDofOrder;
        }

        public int ReturnFinalPrice()
        {
            return finalPrice;
        }
    }
}
