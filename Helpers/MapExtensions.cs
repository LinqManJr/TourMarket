using TourMarket.Dto;
using TourMarket.Models;

namespace TourMarket.Helpers
{
    public static class MapExtensions
    {
        public static Manager ToManager(this ManagerDto managerDto)
        {
            return new Manager { Id = managerDto.Id, Login = managerDto.Login, Name = managerDto.Name };
        }
    }
}
