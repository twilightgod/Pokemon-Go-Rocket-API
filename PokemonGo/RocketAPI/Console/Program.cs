#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AllEnum;
using System.Xml;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo.RocketAPI.GeneratedCode;
using System.Net.Http;
using System.Text;
using Google.Protobuf;
using PokemonGo.RocketAPI.Helpers;
using System.IO;


#endregion

namespace PokemonGo.RocketAPI.Console
{
    internal class Program
    {
        private static ISettings ClientSettings = new Settings();
        private static int Currentlevel = -1;
        private static int TotalExperience = 0;
        private static int TotalPokemon = 0;
        private static DateTime TimeStarted = DateTime.Now;
        public static DateTime InitSessionDateTime = DateTime.Now;

        private static HashSet<String> WantedList = new HashSet<string>()
        {
            "IVYSAUR",
            "VENUSAUR",
            "CHARMELEON",
            "CHARIZARD",
            "WARTORTLE",
            "BLASTOISE",
            "ARBOK",
            "PIKACHU",
            "RAICHU",
            "SANDLASH",
            "NIDOQUEEN",
            "NIDOKING",
            "CLEFABLE",
            "VULPIX",
            "NINETALES",
            "VILEPLUME",
            "DUGTRIO",
            "PERSIAN",
            "GOLDUCK",
            "PRIMEAPE",
            "ARCANINE",
            "POLIWRATH",
            "KADABRA",
            "ALAKHAZAM",
            "MACHAMP",
            "VICTREEBELL",
            "GRAVELER",
            "GOLEM",
            "RAPIDASH",
            "SLOWBRO",
            "MAGNETON",
            "FARFETCHD",
            "DODUO",
            "DODRIO",
            "DEWGONG",
            "GRIMER",
            "MUK",
            "GENGAR",
            "ELECTRODE",
            "EXEGGUTOR",
            "MAROWAK",
            "HITMONLEE",
            "HITMONCHAN",
            "WEEZING",
            "CHANSEY",
            "KANGASKHAN",
            "MR_MIME",
            "MAGMAR",
            "PINSIR",
            "GYARADOS",
            "LAPRAS",
            "DITTO",
            "VAPOREON",
            "JOLTEON",
            "FLAREON",
            "PORYGON",
            "OMANYTE",
            "OMASTAR",
            "KABUTO",
            "KABUTOPS",
            "AERODACTYL",
            "SNORLAX",
            "ARTICUNO",
            "ZAPDOS",
            "MOLTRES",
            "DRATINI",
            "DRAGONAIR",
            "DRAGONITE",
            "MEWTWO",
            "MEW",
        };

        private static HashSet<String> SeenPokemonList = new HashSet<string>();

        public static double GetRuntime()
        {
            return ((DateTime.Now - TimeStarted).TotalSeconds) / 3600;
        }
        
        public static void ColoredConsoleWrite(ConsoleColor color, string text)
        {
            ConsoleColor originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss tt") + "] " + text);
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Logs.txt", "[" + DateTime.Now.ToString("HH:mm:ss tt") + "] " + text + "\n");
            System.Console.ForegroundColor = originalColor;
        }
        
        private static async Task Login(Client client)
        {
            try
            {
                switch (ClientSettings.AuthType)
                {
                    case AuthType.Ptc:
                        await client.DoPtcLogin(ClientSettings.PtcUsername, ClientSettings.PtcPassword);
                        break;
                    case AuthType.Google:
                        await client.DoGoogleLogin();
                        break;
                }

                await client.SetServer();
                var profile = await client.GetProfile();
                //var settings = await client.GetSettings();
                //var mapObjects = await client.GetMapObjects();
                //var inventory = await client.GetInventory();
                //var pokemons =
                  //  inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.Pokemon)
                    //    .Where(p => p != null && p?.PokemonId > 0);

                // Write the players ingame details
                ColoredConsoleWrite(ConsoleColor.Yellow, "----------------------------");
                if (ClientSettings.AuthType == AuthType.Ptc)
                {
                    ColoredConsoleWrite(ConsoleColor.Cyan, "Account: " + ClientSettings.PtcUsername);
                //    ColoredConsoleWrite(ConsoleColor.Cyan, "Password: " + ClientSettings.PtcPassword + "\n");
                }
                ColoredConsoleWrite(ConsoleColor.DarkGray, "Name: " + profile.Profile.Username);
                ColoredConsoleWrite(ConsoleColor.DarkGray, "Team: " + profile.Profile.Team);
                if (profile.Profile.Currency.ToArray()[0].Amount > 0) // If player has any pokecoins it will show how many they have.
                    ColoredConsoleWrite(ConsoleColor.DarkGray, "Pokecoins: " + profile.Profile.Currency.ToArray()[0].Amount);
                ColoredConsoleWrite(ConsoleColor.DarkGray, "Stardust: " + profile.Profile.Currency.ToArray()[1].Amount + "\n");
                //ColoredConsoleWrite(ConsoleColor.DarkGray, "Latitude: " + ClientSettings.DefaultLatitude);
                //ColoredConsoleWrite(ConsoleColor.DarkGray, "Longitude: " + ClientSettings.DefaultLongitude);
                /*
                try
                {
                    ColoredConsoleWrite(ConsoleColor.DarkGray, "Area: " + CallAPI("place", ClientSettings.DefaultLatitude, ClientSettings.DefaultLongitude));
                    ColoredConsoleWrite(ConsoleColor.DarkGray, "Country: " + CallAPI("country", ClientSettings.DefaultLatitude, ClientSettings.DefaultLongitude));
                }
                catch (Exception)
                {
                    ColoredConsoleWrite(ConsoleColor.DarkGray,  "Unable to get Country/Place");
                }
                */
                ColoredConsoleWrite(ConsoleColor.Yellow, "----------------------------");
         
                //await ExecuteFarmingPokestopsAndPokemons(client);
                //await Task.Delay(10000);
                //CheckVersion();
                //Execute();
            }
            catch (TaskCanceledException) { ColoredConsoleWrite(ConsoleColor.Red, "Task Canceled Exception - Restarting");}
            catch (UriFormatException) { ColoredConsoleWrite(ConsoleColor.Red, "System URI Format Exception - Restarting");}
            catch (ArgumentOutOfRangeException) { ColoredConsoleWrite(ConsoleColor.Red, "ArgumentOutOfRangeException - Restarting");}
            catch (ArgumentNullException) { ColoredConsoleWrite(ConsoleColor.Red, "Argument Null Refference - Restarting");}
            catch (NullReferenceException) { ColoredConsoleWrite(ConsoleColor.Red, "Null Refference - Restarting");}
            catch (Exception ex) { ColoredConsoleWrite(ConsoleColor.Red, ex.ToString());}
        }

        private static async Task GetNearbyPokemons(Client client, double lat, double lon)
        {
            var update = await client.UpdatePlayerLocation(lat, lon);

            var mapObjects = await client.GetMapObjects();

            var pokemons = mapObjects.MapCells.SelectMany(i => i.CatchablePokemons);


            foreach (var pokemon in pokemons)
            {
                /*
                var update = await client.UpdatePlayerLocation(pokemon.Latitude, pokemon.Longitude);
                var encounterPokemonResponse = await client.EncounterPokemon(pokemon.EncounterId, pokemon.SpawnpointId);
                var pokemonCP = encounterPokemonResponse?.WildPokemon?.PokemonData?.Cp;
                var pokemonIV = Perfect(encounterPokemonResponse?.WildPokemon?.PokemonData);
                CatchPokemonResponse caughtPokemonResponse;
                do
                {
                    if (ClientSettings.RazzBerryMode == "cp")
                        if (pokemonCP > ClientSettings.RazzBerrySetting)
                            await client.UseRazzBerry(client, pokemon.EncounterId, pokemon.SpawnpointId);
                    if (ClientSettings.RazzBerryMode == "probability")
                        if (encounterPokemonResponse.CaptureProbability.CaptureProbability_.First() < ClientSettings.RazzBerrySetting)
                            await client.UseRazzBerry(client, pokemon.EncounterId, pokemon.SpawnpointId);
                    caughtPokemonResponse = await client.CatchPokemon(pokemon.EncounterId, pokemon.SpawnpointId, pokemon.Latitude, pokemon.Longitude, MiscEnums.Item.ITEM_POKE_BALL, pokemonCP); ; //note: reverted from settings because this should not be part of settings but part of logic
                } while (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchMissed || caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchEscape);
                /*
                /*
                if (ClientSettings.Language == "german")
                {
                    string name_english = Convert.ToString(pokemon.PokemonId);
                    var request = (HttpWebRequest)WebRequest.Create("http://boosting-service.de/pokemon/index.php?pokeName=" + name_english);
                    var response = (HttpWebResponse)request.GetResponse();
                    pokemonName = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                
                else*/

                string pokemonName = Convert.ToString(pokemon.PokemonId).ToUpper();

                string hash = String.Format("{0}|{1}|{2:F15}|{3:F15}", pokemonName, pokemon.SpawnpointId, pokemon.Latitude, pokemon.Longitude);

                if (!SeenPokemonList.Contains(hash))
                {
                    SeenPokemonList.Add(hash);
                    ColoredConsoleWrite(ConsoleColor.Cyan, String.Format("{0} {1} {2} {3} {4}", pokemonName, pokemon.Latitude, pokemon.Longitude, UnixTimeStampToDateTime(pokemon.ExpirationTimestampMs).ToString(), hash));

                    if (WantedList.Contains(pokemonName))
                    {
                        //Notify
                        ColoredConsoleWrite(ConsoleColor.Red, "Found!");
                    }
                }
                else
                {
                    ColoredConsoleWrite(ConsoleColor.Cyan, "Dup");
                }





                /*
                if (caughtPokemonResponse.Status == CatchPokemonResponse.Types.CatchStatus.CatchSuccess)
                {
                    ColoredConsoleWrite(ConsoleColor.Green, $"We caught a {pokemonName} with {pokemonCP} CP and {pokemonIV}% IV");
                    foreach (int xp in caughtPokemonResponse.Scores.Xp)
                        TotalExperience += xp;
                    TotalPokemon += 1;
                }
                else
                    ColoredConsoleWrite(ConsoleColor.Red, $"{pokemonName} with {pokemonCP} CP and {pokemonIV}% IV");

                if (ClientSettings.TransferType == "leaveStrongest")
                    await TransferAllButStrongestUnwantedPokemon(client);
                else if (ClientSettings.TransferType == "all")
                    await TransferAllGivenPokemons(client, pokemons2);
                else if (ClientSettings.TransferType == "duplicate")
                    await TransferDuplicatePokemon(client);
                else if (ClientSettings.TransferType == "cp")
                    await TransferAllWeakPokemon(client, ClientSettings.TransferCPThreshold);
                else if (ClientSettings.TransferType == "iv")
                    await TransferAllGivenPokemons(client, pokemons2, ClientSettings.TransferIVThreshold);

                await Task.Delay(3000);
                */
            }
        }

        /*
        private static async Task ExecuteFarmingPokestopsAndPokemons(Client client)
        {
            var mapObjects = await client.GetMapObjects();

            var pokeStops = mapObjects.MapCells.SelectMany(i => i.Forts).Where(i => i.Type == FortType.Checkpoint && i.CooldownCompleteTimestampMs < DateTime.UtcNow.ToUnixTime());

            foreach (var pokeStop in pokeStops)
            {
                var update = await client.UpdatePlayerLocation(pokeStop.Latitude, pokeStop.Longitude);
                var fortInfo = await client.GetFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);
                var fortSearch = await client.SearchFort(pokeStop.Id, pokeStop.Latitude, pokeStop.Longitude);

                StringWriter PokeStopOutput = new StringWriter();
                PokeStopOutput.Write($"");
                if (fortInfo.Name != string.Empty)
                    PokeStopOutput.Write("PokeStop: " + fortInfo.Name);
                if (fortSearch.ExperienceAwarded != 0)
                    PokeStopOutput.Write($", XP: {fortSearch.ExperienceAwarded}");
                if (fortSearch.GemsAwarded != 0)
                    PokeStopOutput.Write($", Gems: {fortSearch.GemsAwarded}");
                if (fortSearch.PokemonDataEgg != null)
                    PokeStopOutput.Write($", Eggs: {fortSearch.PokemonDataEgg}");
                if (GetFriendlyItemsString(fortSearch.ItemsAwarded) != string.Empty)
                    PokeStopOutput.Write($", Items: {GetFriendlyItemsString(fortSearch.ItemsAwarded)} ");
                ColoredConsoleWrite(ConsoleColor.Cyan, PokeStopOutput.ToString());

                if (fortSearch.ExperienceAwarded != 0)
                    TotalExperience += (fortSearch.ExperienceAwarded);
                await Task.Delay(15000);
                await ExecuteCatchAllNearbyPokemons(client);
            }
        }
        */

        private static async Task Travel(Client client)
        {
            SeenPokemonList.Clear();
            double step = 0.0020;
            for (double lat = client._settings.lat_min; lat <= client._settings.lat_max; lat += step)
            {
                for (double lon = client._settings.lon_min; lon <= client._settings.lon_max; lon += step)
                {
                    ColoredConsoleWrite(ConsoleColor.Gray, String.Format("Current lat/lon: {0}/{1}", lat, lon));
                    await GetNearbyPokemons(client, lat, lon);
                    await Task.Delay(1000);
                }
            }
        }

        private static void SendEmail(Client client)
        {

        }

        private static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    var client = new Client(ClientSettings);
                    await Login(client);
                    //await GetNearbyPokemons(client, 47.61250655050066, -122.2007668018341);
                    await Travel(client);
                    ColoredConsoleWrite(ConsoleColor.Red, SeenPokemonList.Count.ToString());
                    System.Console.ReadKey();
                }
                catch (PtcOfflineException)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, "PTC Servers are probably down OR your credentials are wrong. Try google");
                }
                catch (Exception ex)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"Unhandled exception: {ex}");
                }
            });
            System.Console.ReadLine();
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
