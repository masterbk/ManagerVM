using ManagerVM.Contacts.Models.OpenStackResponses;
using ManagerVM.Data.Entities;
using Mapster;

namespace ManagerVM.WebApi.MappingConfig
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreatedVMResponse, VMEntity>()
                .Map(des => des.InstanceId, src=>src.Id)
                .Map(des => des.Address, src => src.Address)
                .Map(des => des.Status, src => src.Status)
                .Map(des => des.Name, src => src.Name)
                .Ignore(des=>des.Id);
        }
    }
}
