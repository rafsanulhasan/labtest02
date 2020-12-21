using HotChocolate.Types;

using LabTest2.Apps.Web.Shared.DTOs;

using System.Linq;

namespace LabTest2.Apps.Web.Shared.GraphTypes
{
	public class WeatherForecastType
		: ObjectType<WeatherForecastDTO>
	{
		protected override void Configure(IObjectTypeDescriptor<WeatherForecastDTO> descriptor)
		{
			_ = descriptor
				.Field(f => f.Date)
				.Type<NonNullType<DateType>>()
				.ID();

			_ = descriptor
				.Field(f => f.Summary)
				.Type<NonNullType<StringType>>();

			_ = descriptor
				.Field(f => f.TemperatureC)
				.Type<NonNullType<IntType>>()
				.Name(nameof(WeatherForecastDTO.TemperatureC));

			_ = descriptor
				.Field(f => f.TemperatureF)
				.Type<IntType>();
		}
	}
}
