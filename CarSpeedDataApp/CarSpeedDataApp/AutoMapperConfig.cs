using AutoMapper;
using CarSpeedDataApp.Core.Models;

namespace CarSpeedDataApp;

public class AutoMapperConfig
{
	public static IMapper CreateMapper()
	{
		var configuration = new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<CarSpeedDataRequest, CarSpeedData>()
				.ForMember(a => a.Id,
					opt =>
						opt.Ignore());
			cfg.CreateMap<CarSpeedData, CarSpeedDataRequest>();
		});

		// only during development, validate your mappings; remove it before release
#if DEBUG
		configuration.AssertConfigurationIsValid();
#endif

		// use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself
		var mapper = configuration.CreateMapper();
		return mapper;
	}
}