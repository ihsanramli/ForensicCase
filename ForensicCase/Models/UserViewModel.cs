using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ForensicCase.Models
{
    public class UserViewModel
    {
        public DataTable Users { get; set; }
        public string Search { get; set; }
    }

}
