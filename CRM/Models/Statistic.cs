using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
    public sealed class Statistic
    {
        public int countAcceptedRequestRegistration { get; set; } = 0;
        public int countRefuseRequestRegistration { get; set; } = 0;
        public int countAcceptedRequestGetMoney { get; set; } = 0;
        public int countRefuseRequestGetMoney { get; set; } = 0;
        
    }
}
