using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Просмотр_графиков
{
    [Serializable]
    public class MassReport
    {
        public int id { get; set; }
        public int sales { get; set; }
        public DateTime year { get; set; }
    }
}
