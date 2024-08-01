using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Services
{
    public class ServiceConstants
    {
        public const string NAME = "ManagerVM";
        public const string INSTALL_DOCKER = "#!/bin/bash\n/bin/su\napt update -y\napt install docker-compose -y";
        public const string INSTALL_LMS = "#!/bin/bash\n/bin/su\ncd /home\nrm -rf /home/LMS-Latest\ngit clone https://github.com/masterbk/LMS-Latest.git\n" +
            "sed -i 's/@FullServerName/@_FullServerNameReplace/g' /home/LMS-Latest/disk/moodle-node/config/config.php\n" +
            "sed -i 's/@ServerName/@_ServerNameReplace/g' /home/LMS-Latest/disk/moodle-node/config/node1/nginx/site-enable/vtc.conf\n" +
            "cd /home/LMS-Latest\n" +
            //"docker rmi $(docker images -q)\n" +
            //"docker rm -f $(docker ps -a -q)\n" +
            //"docker-compose -f docker-compose.yaml down\n" +
            //"docker volume rm $(docker volume ls -q)" +
            "docker-compose -f /home/LMS-Latest/docker-compose.yaml up -d";
        public const string ADD_USER = "#!/bin/bash\n/bin/su\nuseradd {0}";
    }
}
