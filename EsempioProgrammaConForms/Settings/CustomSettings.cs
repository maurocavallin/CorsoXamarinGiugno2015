using System;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;

namespace EsempioProgrammaConForms
{
	public static class CustomSettings
	{
		private const string ServerAddressKey = "ServerAddress";
		private static readonly string ServerAddressDefault = "10.123.26.1";


		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		} 

		public static string ServerAddress
		{
			get { return AppSettings.GetValueOrDefault<string>(ServerAddressKey, ServerAddressDefault); }
			set { AppSettings.AddOrUpdateValue<string>(ServerAddressKey, value); }
		}
	}
}

