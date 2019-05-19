namespace Utils
{
	using System;

	public class TypeConverter
	{
		public static TDestinationType ConvertType<TDestinationType, TSourceType>(TSourceType value) where TSourceType : IConvertible
		{
			return (TDestinationType)Convert.ChangeType(value, typeof(TDestinationType));
		}
	}
}
