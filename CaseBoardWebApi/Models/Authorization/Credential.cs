using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBoardWebApi.Models.Authorization
{
    public class Credential
    {
        public string calltoken { get; set; }
        public string password { get; set; }
    }
}
