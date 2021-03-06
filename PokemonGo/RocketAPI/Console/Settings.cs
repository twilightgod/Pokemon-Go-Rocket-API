#region

using System.Configuration;
using System.Globalization;
using System.Runtime.CompilerServices;
using PokemonGo.RocketAPI.Enums;
using System.Collections.Generic;
//using AllEnum;
using System;

#endregion

namespace PokemonGo.RocketAPI.Console
{
    public class Settings : ISettings
    {
        private Configuration CustomAppConfig = null;

        public Settings(string appConfigPath = null)
        {
            if (!String.IsNullOrEmpty(appConfigPath))
            {
                var configMap = new ExeConfigurationFileMap();
                configMap.ExeConfigFilename = appConfigPath;
                CustomAppConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            }
        }

        public string TransferType => GetSetting() != string.Empty ? GetSetting() : "none";
        public int TransferCPThreshold => GetSetting() != string.Empty ? int.Parse(GetSetting(), CultureInfo.InvariantCulture) : 0;
        public int TransferIVThreshold => GetSetting() != string.Empty ? int.Parse(GetSetting(), CultureInfo.InvariantCulture) : 0;
        public bool EvolveAllGivenPokemons => GetSetting() != string.Empty ? System.Convert.ToBoolean(GetSetting(), CultureInfo.InvariantCulture) : false;


        public AuthType AuthType => (GetSetting() != string.Empty ? GetSetting() : "Ptc") == "Ptc" ? AuthType.Ptc : AuthType.Google;
        public string PtcUsername => GetSetting() != string.Empty ? GetSetting() : "username";
        public string PtcPassword => GetSetting() != string.Empty ? GetSetting() : "password";

        public double DefaultLatitude
        {
            get { return GetSetting() != string.Empty ? double.Parse(GetSetting(), CultureInfo.InvariantCulture) : 51.22640; }
            set { SetSetting(value); }
        }


        public double DefaultLongitude
        {
            get { return GetSetting() != string.Empty ? double.Parse(GetSetting(), CultureInfo.InvariantCulture) : 6.77874; }
            set { SetSetting(value); }
        }


        public string LevelOutput => GetSetting() != string.Empty ? GetSetting() : "time";

        public int LevelTimeInterval => GetSetting() != string.Empty ? System.Convert.ToInt16(GetSetting()) : 600;

        public bool Recycler => GetSetting() != string.Empty ? System.Convert.ToBoolean(GetSetting(), CultureInfo.InvariantCulture) : false;

        /*
        ICollection<KeyValuePair<ItemId, int>> ISettings.ItemRecycleFilter
        {
            get
            {
                //Type and amount to keep
                return new[]
                {
                    new KeyValuePair<ItemId, int>(ItemId.ItemPokeBall, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemGreatBall, 50),
                    new KeyValuePair<ItemId, int>(ItemId.ItemUltraBall, 100),
                    new KeyValuePair<ItemId, int>(ItemId.ItemMasterBall, 200),
                    new KeyValuePair<ItemId, int>(ItemId.ItemRazzBerry, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemRevive, 20),
                    new KeyValuePair<ItemId, int>(ItemId.ItemPotion, 0),
                    new KeyValuePair<ItemId, int>(ItemId.ItemSuperPotion, 0),
                    new KeyValuePair<ItemId, int>(ItemId.ItemHyperPotion, 50)
                };
            }
        }*/

        public int RecycleItemsInterval => GetSetting() != string.Empty ? Convert.ToInt16(GetSetting()) : 60;

        public string Language => GetSetting() != string.Empty ? GetSetting() : "english";

        public string RazzBerryMode => GetSetting() != string.Empty ? GetSetting() : "cp";

        public double RazzBerrySetting => GetSetting() != string.Empty ? double.Parse(GetSetting(), CultureInfo.InvariantCulture) : 500;

        public string GoogleRefreshToken
        {
            get { return GetSetting() != string.Empty ? GetSetting() : string.Empty; }
            set { SetSetting(value); }
        }

        public double lat_min => GetSetting() != string.Empty ? System.Convert.ToDouble(GetSetting()) : 0;
        public double lat_max => GetSetting() != string.Empty ? System.Convert.ToDouble(GetSetting()) : 0;
        public double lon_min => GetSetting() != string.Empty ? System.Convert.ToDouble(GetSetting()) : 0;
        public double lon_max => GetSetting() != string.Empty ? System.Convert.ToDouble(GetSetting()) : 0;

        public string emailFromServer => GetSetting() != string.Empty ? GetSetting() : String.Empty;
        public string emailFromUserName => GetSetting() != string.Empty ? GetSetting() : String.Empty;
        public string emailFromPassword => GetSetting() != string.Empty ? GetSetting() : String.Empty;
        public string emailTo => GetSetting() != string.Empty ? GetSetting() : String.Empty;

        public string linkPrefix => GetSetting() != string.Empty ? GetSetting() : String.Empty;

        private string GetSetting([CallerMemberName] string key = null)
        {
            if (CustomAppConfig != null)
            {
                return CustomAppConfig.AppSettings.Settings[key].Value;
            }
            else
            {
                return ConfigurationManager.AppSettings[key];
            }
        }

        private void SetSetting(string value, [CallerMemberName] string key = null)
        {
            //var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //if (key != null) configFile.AppSettings.Settings[key].Value = value;
            //configFile.Save();
        }

        private void SetSetting(double value, [CallerMemberName] string key = null)
        {
            //CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            //customCulture.NumberFormat.NumberDecimalSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            //var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //if (key != null) configFile.AppSettings.Settings[key].Value = value.ToString();
            //configFile.Save();
        }
    }
}
