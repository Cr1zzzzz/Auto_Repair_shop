using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaFinal.BaseClasses
{
    public static class FindTheHighestID
    {

        //public static int ReturnIDo(List<Person> persons)
        //{
        //    int id = 0;
        //    foreach (var i in persons)
        //    {
        //        if (i.ReturnID() > id)
        //        {
        //            id = i.ReturnID();
        //        }
        //    }
        //    return id;
        //}

        public static int ReturnIDofAdmin(List<Admin> admins)
        {
            int id = 0;
            foreach (var i in admins)
            {
                if (i.ReturnID() > id)
                {
                    id = i.ReturnID();
                }
            }
            return id;
        }

        public static int ReturnIDofWorker(List<Worker> workers)
        {
            int id = 0;
            foreach (var i in workers)
            {
                if (i.ReturnID() > id)
                {
                    id = i.ReturnID();
                }
            }
            return id;
        }

        public static int ReturnIDofClient(List<Client> clients)
        {
            int id = 0;
            foreach (var i in clients)
            {
                if (i.ReturnID() > id)
                {
                    id = i.ReturnID();
                }
            }
            return id;
        }
        public static int ReturnIDofCheck(List<ServiceFinalCheck> finalChecks)
        {
            int id = 0;
            foreach (var i in finalChecks)
            {
                if (i.ReturnID() > id)
                {
                    id = i.ReturnID();
                }
            }
            return id;
        }

    }
}
