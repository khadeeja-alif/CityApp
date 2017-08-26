
// Helpers/Settings.cs This file was automatically added when you installed the Settings Plugin. If you are not using a PCL then comment this file back in to use it.
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CityApp.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private static readonly string SettingsDefault = string.Empty;
        private const string AddressIdentifier = "address";
        private const string CityIdentifier = "city";
        private const string StateIdentifier = "state";
        private const string ZipIdentifier = "zip";
        private const string TimeStamp = "time";
        private const string latitudeIdentifier = "latitude";
        private const string longitudeIdentifier = "longitude";

        #endregion

        public static string Address
        {
            get
            {
                return AppSettings.GetValueOrDefault(AddressIdentifier, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AddressIdentifier, value);
            }
        }

        public static string City
		{
			get
			{
				return AppSettings.GetValueOrDefault(CityIdentifier, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(CityIdentifier, value);
			}
		}


        public static string State
        {
            get
            {
                return AppSettings.GetValueOrDefault(StateIdentifier, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(StateIdentifier, value);
            }
        }


        public static string Zip
        {
            get
            {
                return AppSettings.GetValueOrDefault(ZipIdentifier, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ZipIdentifier, value);
            }
        }

        public static string Time
        {
            get
            {
                return AppSettings.GetValueOrDefault(TimeStamp, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(TimeStamp, value);
            }
        }

        public static string latitude
        {
            get
            {
                return AppSettings.GetValueOrDefault(latitudeIdentifier, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(latitudeIdentifier, value);
            }
        }

        public static string longtiude
        {
            get
            {
                return AppSettings.GetValueOrDefault(longitudeIdentifier, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(longitudeIdentifier, value);
            }
        }

    }
}