using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace faceitapi.Controllers
{
    public class TokenLogin
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class TokenRetorno
    {
        public string Token { get; set; }
        public DateTime DataTokenGerado { get; set; }
    }
}
