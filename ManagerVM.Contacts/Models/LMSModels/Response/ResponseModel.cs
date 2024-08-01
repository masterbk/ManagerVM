using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.LMSModels.Response
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Data { get; set; }
    }

    public class GetListReponseDataModel<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageTotal { get; set; }
        public int ItemsTotal { get; set; }
        public List<T> Items { get; set; }
    }
}
