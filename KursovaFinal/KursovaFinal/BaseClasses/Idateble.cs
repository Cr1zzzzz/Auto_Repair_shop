using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursovaFinal.BaseClasses
{
    public interface Idateble
    {
        DateTime ReturnDate();
        void SetDate(DateTime date);
    }
}
