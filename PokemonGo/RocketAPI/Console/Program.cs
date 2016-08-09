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
//using AllEnum;
using System.Xml;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Exceptions;
using PokemonGo.RocketAPI.Extensions;
//using PokemonGo.RocketAPI.GeneratedCode;
using System.Net.Http;
using System.Text;
using Google.Protobuf;
using PokemonGo.RocketAPI.Helpers;
using System.IO;
using System.Net.Mail;
using System.Configuration;
using POGOProtos.Data;
using POGOProtos.Map.Pokemon;


#endregion

namespace PokemonGo.RocketAPI.Console
{
    internal class Program
    {
        private static int Currentlevel = -1;
        private static int TotalExperience = 0;
        private static int TotalPokemon = 0;
        private static DateTime TimeStarted = DateTime.Now;
        public static DateTime InitSessionDateTime = DateTime.Now;

        private static string LogFolderName = String.Format(@"c:\logs\{0:MM}\{0:dd}", TimeStarted);
        private static string LogFileName = String.Format("Rocket_{0}.txt", TimeStarted.ToString("yyyy_MM_dd_HH_mm_ss"));

        private static HashSet<String> WantedList = new HashSet<string>()
        {
            "BULBASAUR",
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
            "PONYTA",
            "RAPIDASH",
            "TENTACRUEL",
            "SLOWBRO",
            "MAGNETON",
            "FARFETCHD",
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

        private static HashSet<String> WantedList2 = new HashSet<string>()
        {
            "CHARMENDER",
            "SQUIRTLE",
            "BUTTERFREE",
            "BEEDRILL",
            "PIDGEOT",
            "FEAROW",
            "EKANS",
            "SANDSHREW",
            "NIDORINA",
            "NIDORINO",
            "CLEFARY",
            "VULPIX",
            "JIGGLYPUFF",
            "GOLBAT",
            "GLOOM",
            "PARASECT",
            "VENOMOTH",
            "DIGLETT",
            "MEOWTH",
            "PSYDUCK",
            "GOLDUCK",
            "MANKEY",
            "GROWLITHE",
            "POLIWHIRL",
            "ABRA",
            "MACHOKE",
            "WEEPINBELL",
            "GEODUGE",
            "SLOWPOKE",
            "MAGNEMITE",
            "SEEL",
            "CLOYSTER",
            "GASTLY",
            "HAUNTER",
            "ONIX",
            "DROWZEE",
            "HYPNO",
            "KRABBY",
            "KINGLER",
            "CUBONE",
            "LICKITUNG",
            "KOFFING",
            "RHYHORN",
            "RHYDON",
            "TANGELA",
            "HORSEA",
            "SEADRA",
            "GOLDEEN",
            "SEAKING",
            "STARYU",
            "STARMIE",
            "SCYTHER",
            "JYNX",
            "ELECTABUZZ",
            "TAUROS",
            "MAGIKARP",
            "EEVEE",
            "DODUO",
        };

        private static Dictionary<String, DateTime> SeenPokemonList = new Dictionary<string, DateTime>();

        public static double GetRuntime()
        {
            return ((DateTime.Now - TimeStarted).TotalSeconds) / 3600;
        }
        
        public static void ColoredConsoleWrite(ConsoleColor color, string text)
        {
            ConsoleColor originalColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine("[" + DateTime.Now.ToString("HH:mm:ss tt") + "] " + text);

            if (!Directory.Exists(LogFolderName))
            {
                Directory.CreateDirectory(LogFolderName);
            }

            File.AppendAllText(Path.Combine(LogFolderName, LogFileName), "[" + DateTime.Now.ToString("HH:mm:ss tt") + "] " + text + Environment.NewLine);
            System.Console.ForegroundColor = originalColor;
        }
        
        private static async Task Login(Client client)
        {
            try
            {
                /*
                switch (client.AuthType)
                {
                    case AuthType.Ptc:
                        await client.DoPtcLogin(client.Settings.PtcUsername, client.Settings.PtcPassword);
                        break;
                    //case AuthType.Google:
                      //  await client.DoGoogleLogin();
                        //break;
                }
                */
                /*
                await client.SetServer();
                var profile = await client.GetProfile();
                //var settings = await client.GetSettings();
                //var mapObjects = await client.GetMapObjects();
                //var inventory = await client.GetInventory();
                //var pokemons =
                  //  inventory.InventoryDelta.InventoryItems.Select(i => i.InventoryItemData?.Pokemon)
                    //    .Where(p => p != null && p?.PokemonId > 0);

                // Write the players ingame details
                */
                ColoredConsoleWrite(ConsoleColor.Yellow, "----------------------------");
                //if (client.Settings.AuthType == AuthType.Ptc)
                //{
                    ColoredConsoleWrite(ConsoleColor.Cyan, "Account: " + client.Settings.PtcUsername);
                //    ColoredConsoleWrite(ConsoleColor.Cyan, "Password: " + ClientSettings.PtcPassword + "\n");
                //}
                await client.Login.DoLogin();
                var profile = await client.Player.GetPlayer();
                
                ColoredConsoleWrite(ConsoleColor.DarkGray, "Name: " + profile.PlayerData.Username);
                //ColoredConsoleWrite(ConsoleColor.DarkGray, "Team: " + profile.Profile.Team);
                //if (profile.Profile.Currency.ToArray()[0].Amount > 0) // If player has any pokecoins it will show how many they have.
                  //  ColoredConsoleWrite(ConsoleColor.DarkGray, "Pokecoins: " + profile.Profile.Currency.ToArray()[0].Amount);
                //ColoredConsoleWrite(ConsoleColor.DarkGray, "Stardust: " + profile.Profile.Currency.ToArray()[1].Amount + "\n");
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

                //await client.Login.DoLogin();
                //await ExecuteFarmingPokestopsAndPokemons(client);
                //await Task.Delay(10000);
                //CheckVersion();
                //Execute();
            }
            //catch (TaskCanceledException) { ColoredConsoleWrite(ConsoleColor.Red, "Task Canceled Exception - Restarting");}
            //catch (UriFormatException) { ColoredConsoleWrite(ConsoleColor.Red, "System URI Format Exception - Restarting");}
            //catch (ArgumentOutOfRangeException) { ColoredConsoleWrite(ConsoleColor.Red, "ArgumentOutOfRangeException - Restarting");}
            //catch (ArgumentNullException) { ColoredConsoleWrite(ConsoleColor.Red, "Argument Null Refference - Restarting");}
            //catch (NullReferenceException) { ColoredConsoleWrite(ConsoleColor.Red, "Null Refference - Restarting");}
            catch (Exception ex) { ColoredConsoleWrite(ConsoleColor.Red, ex.ToString()); throw ex; }
        }

        private static async Task GetNearbyPokemons(Client client, double lat, double lon)
        {
            //var update = await client.UpdatePlayerLocation(lat, lon);
            //await client.Player.UpdatePlayerLocation(lat, lon, new Random().NextDouble() * 50);
            client.Player.SetCoordinates(lat, lon, new Random().NextDouble() * 50);
            //client.SetCoordinates(lat, lon);

            var mapObjects = await client.Map.GetMapObjects();
            
            var pokemons = mapObjects.Item1.MapCells.SelectMany(i => i.CatchablePokemons);

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
                DateTime expiredTime = UnixTimeStampToDateTime(pokemon.ExpirationTimestampMs);

                string hash = String.Format("{0}|{1}|{2:F15}|{3:F15}", pokemonName, pokemon.SpawnPointId, pokemon.Latitude, pokemon.Longitude);

                if (!SeenPokemonList.ContainsKey(hash))
                {
                    SeenPokemonList.Add(hash, expiredTime);

                    // Calculate IV rating
                    double pokemonIV = -1;

                    try
                    {
                        var encounterPokemonResponse = await client.Encounter.EncounterPokemon(pokemon.EncounterId, pokemon.SpawnPointId);
                        pokemonIV = Perfect(encounterPokemonResponse?.WildPokemon?.PokemonData);
                    }
                    catch (Exception ex)
                    {
                        ColoredConsoleWrite(ConsoleColor.Red, $"Unhandled exception: {ex}");
                    }

                    ColoredConsoleWrite(ConsoleColor.Cyan, String.Format("{0} {1},{2} {3} {4} {5}", pokemonName, pokemon.Latitude, pokemon.Longitude, expiredTime.ToString(), pokemonIV, hash));

                    if (WantedList.Contains(pokemonName) || WantedList2.Contains(pokemonName) && pokemonIV >= 0.95 || pokemonIV >= 0.97)
                    {
                        //Notify
                        ColoredConsoleWrite(ConsoleColor.Red, "Found!");
                        
                        await SendEmail(client, pokemon, pokemonIV);
                    }
                }
                else
                {
                    ColoredConsoleWrite(ConsoleColor.Cyan, "Dup");
                }
            }
        }

        private static double Perfect(PokemonData pokemonData)
        {
            return (pokemonData.IndividualAttack + pokemonData.IndividualDefense + pokemonData.IndividualStamina) / 45.0;
        }

        private static async Task Travel(Client client)
        {
            double step = 0.0020;
            for (double lat = client.Settings.lat_min; lat <= client.Settings.lat_max; lat += step)
            {
                for (double lon = client.Settings.lon_min; lon <= client.Settings.lon_max; lon += step)
                {
                    try
                    {
                        ColoredConsoleWrite(ConsoleColor.Gray, String.Format("Current lat,lon: {0},{1}", lat, lon));
                        await GetNearbyPokemons(client, lat, lon);
                        await Task.Delay(10 * 1000);
                    }
                    catch (Exception ex)
                    {
                        ColoredConsoleWrite(ConsoleColor.Red, $"Unhandled exception: {ex}");
                    }
                }
            }
        }

        private static async Task SendEmail(Client client, MapPokemon pokemon, double pokemonIV)
        {
            try
            {
                DateTime expiredTime = UnixTimeStampToDateTime(pokemon.ExpirationTimestampMs);
                string pokemonName = Convert.ToString(pokemon.PokemonId).ToUpper();

                string msg = String.Format("Pokemon: {1}{0}Pokedex: {2}{0}Map: {3}{0}Link: {4}{0}Email Time: {5}{0}",
                    Environment.NewLine,
                    String.Format("{0}  Name: {1}{0}  Rating: {2:F2}{0}  Expire Time:{3}", Environment.NewLine, pokemonName, pokemonIV, expiredTime.ToString()),
                    String.Format("http://www.pokemon.com/us/pokedex/{0}", pokemonName),
                    String.Format("http://maps.google.com/?q={0},{1}", pokemon.Latitude, pokemon.Longitude),
                    String.Format("{0}?lat={1}&lon={2}", client.Settings.linkPrefix, pokemon.Latitude, pokemon.Longitude),
                    DateTime.Now.ToString()
                    );


                MailMessage message = new MailMessage(client.Settings.emailFromUserName, client.Settings.emailTo);
                message.Subject = String.Format("{0} {1:F2}", pokemonName, pokemonIV);
                message.Body = msg;
                SmtpClient smtpClient = new SmtpClient(client.Settings.emailFromServer);
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(client.Settings.emailFromUserName, client.Settings.emailFromPassword);
                smtpClient.Send(message);

                ColoredConsoleWrite(ConsoleColor.Cyan, "email sent");
            }
            catch (Exception ex)
            {
                ColoredConsoleWrite(ConsoleColor.Red, $"Unhandled exception: {ex}");
            }
        }

        private static void RemoveExpiredPokemon(Client client)
        {
            var expiredList = SeenPokemonList.Where(kvp => kvp.Value <= DateTime.Now).ToList();

            foreach (var kvp in expiredList)
            {
                SeenPokemonList.Remove(kvp.Key);
                ColoredConsoleWrite(ConsoleColor.Gray, "remove expired pokemon: " + kvp.Key + kvp.Value.ToString());
            }

            ColoredConsoleWrite(ConsoleColor.Gray, "Total removed: " + expiredList.Count);
            ColoredConsoleWrite(ConsoleColor.Gray, "Total remaining: " + SeenPokemonList.Count);
        }

        private static void Main(string[] args)
        {
            string customAppConfigPath = null;

            if (args.Length > 0)
            {
                customAppConfigPath = args[0];
            }

            Task.Run(async () =>
            {
                try
                {
                    var client = new Client(new Settings(customAppConfigPath), new APIFailure());
                    await Login(client);
                    SeenPokemonList.Clear();
                    while ((DateTime.Now - TimeStarted).TotalMinutes < 30)
                    {
                        try
                        {
                            await Travel(client);
                            RemoveExpiredPokemon(client);
                            //wait for 90s
                            await Task.Delay(10 * 1000);
                        }
                        catch (Exception ex)
                        {
                            ColoredConsoleWrite(ConsoleColor.Red, $"Unhandled exception: {ex}");
                        }
                    }
                    
                    //ColoredConsoleWrite(ConsoleColor.Red, SeenPokemonList.Count.ToString());
                    //System.Console.ReadKey();
                }
                catch (PtcOfflineException)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, "PTC Servers are probably down OR your credentials are wrong. Try google");
                }
                catch (Exception ex)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"Unhandled exception: {ex}");
                }
            }).Wait();
            //System.Console.ReadLine();
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
