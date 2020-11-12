using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWS.Dal.Sql.Config
{
    public class HWSDatabaseSettings : IHWSDatabaseSettings
    {
        public string MSSQLConnection { get; set; }
    }

    public interface IHWSDatabaseSettings
    {
        public string MSSQLConnection { get; set; }
    }
}
