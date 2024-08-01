using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class GetCategoryReponseDataModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Link ảnh đại diện
        /// </summary>
        public string Img { get; set; }
        public int ClassesNumber { get; set; }
    }
}
