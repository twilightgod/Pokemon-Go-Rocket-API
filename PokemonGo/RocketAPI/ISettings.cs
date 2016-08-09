#region

using PokemonGo.RocketAPI.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#endregion

namespace PokemonGo.RocketAPI
{
    public interface ISettings
    {
        AuthType AuthType { get; }
        double DefaultLatitude { get; set; }
        double DefaultLongitude { get; set; }
        string LevelOutput { get; }
        int LevelTimeInterval { get; }
        string GoogleRefreshToken { get; set; }
        string PtcPassword { get; }
        string PtcUsername { get; }
        bool EvolveAllGivenPokemons { get; }
        string TransferType { get; }
        int TransferCPThreshold { get; }
        int TransferIVThreshold { get; }
        bool Recycler { get; }
        //ICollection<KeyValuePair<AllEnum.ItemId, int>> ItemRecycleFilter { get; }
        int RecycleItemsInterval { get; }
        string Language { get; }
        string RazzBerryMode { get; }
        double RazzBerrySetting { get; }

        double lat_min { get; }
        double lat_max { get; }
        double lon_min { get; }
        double lon_max { get; }

        string emailFromServer { get; }
        string emailFromUserName { get; }
        string emailFromPassword { get; }
        string emailTo { get; }

        string linkPrefix { get; }
    }
}
