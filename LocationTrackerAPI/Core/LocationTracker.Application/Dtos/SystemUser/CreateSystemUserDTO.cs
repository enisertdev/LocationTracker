using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationTracker.Application.Dtos.SystemUser
{
    public class CreateSystemUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
