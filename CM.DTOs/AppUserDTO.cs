using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class AppUserDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public AppUserDTO()
        {

        }

    }
}
