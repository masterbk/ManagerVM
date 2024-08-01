using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class UploadMediaResponseDataModel
    {
        public string Component { get; set; }
        public long Contextid { get; set; }
        public string Userid { get; set; }
        public string Filearea { get; set; }
        public string Filename { get; set; }
        public string Filepath { get; set; }
        public long Itemid { get; set; }
        public string License { get; set; }
        public string Author { get; set; }
        public dynamic Source { get; set; }
        public long Filesize { get; set; }
    }
}
