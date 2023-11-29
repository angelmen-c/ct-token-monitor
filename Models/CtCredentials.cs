using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ct_token_monitor.Models
{
    public class CtCredentials
    {
        public string ctAuthUrl { get; set; }
        public string ctClientId { get; set; }
        public string ctClientSecret { get; set; }
    }
}
