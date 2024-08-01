using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models
{
    public class MoodleConfig
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public MainServer MainServer {  get; set; }
    }

    public class MainServer
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
    }
}
