using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaFinal
{
    [Serializable]
    public class ServiceBasePrice
    {
        public string Posluga;
        public int Price;

        public ServiceBasePrice(string posluga, int price)
        {
            Posluga = posluga;
            Price = price;
        }

        public ServiceBasePrice()
        {
        }

        public void SetPrice(int price)
        {
            Price = price;
        }

        public int ReturnPrice()
        {
            return Price;
        }

        public void SetPosluga(string posluga)
        {
            Posluga = posluga;
        }

        public string ReturnPosluga()
        {
            return Posluga;
        }
    }
}
