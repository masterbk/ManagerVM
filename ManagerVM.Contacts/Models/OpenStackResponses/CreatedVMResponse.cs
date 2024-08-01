using ManagerVM.Contacts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ManagerVM.Contacts.Models.OpenStackResponses
{
    public class CreatedVMResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public VMStatus Status { get; set; }
    }
}
