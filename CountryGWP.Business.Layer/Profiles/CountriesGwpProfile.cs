using AutoMapper;

namespace CountryGWP.Business.Layer.Profiles
{
    public class CountriesGwpProfile : Profile
    {
        public CountriesGwpProfile()
        {
            CreateMap<DataAccess.Layer.Entities.CountryGwp, Models.CountryGwp>();
        }
    }
}
