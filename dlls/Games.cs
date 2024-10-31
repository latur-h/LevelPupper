using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelPupper__Parser.dlls
{
    internal static class Games
    {
        public static Dictionary<string, _game> games = new()
            {
                { "WoW Cataclysm Classic", new _game("wow-cataclysm", "14", "wow retail") },
                { "Last Epoch", new _game("last-epoch", "12", "le") },
                { "Apex Legends", new _game("apex", "4", "apex") },
                { "Helldivers 2", new _game("helldivers-2", "11", "") },
                { "WoW: Season of Discovery", new _game("season-of-discovery", "2", "wow sod") },
                { "The First Descendant", new _game("the-first-descendant", "15", "tfd") },
                { "Diablo 4", new _game("d4", "5", "diablo") },
                { "Path of Exile", new _game("path-of-exile", "13", "poe") },
                { "Destiny 2", new _game("d2", "3", "destiny") },
                { "World of Warcraft", new _game("wow", "1", "wow retail") },
                { "Call of Duty", new _game("cod", "16", "cod") }
            };

        public static Dictionary<string, string> GetCodenames() => games.ToDictionary(x => x.Key, x => x.Value.codename);
        public static string GetBoostSpec(string game) => games[game].boostSpec;
        public static string GetOptionValue(string game) => games[game].optionValue;
        public static string GetCodename(string game) => games[game].codename;
    }
    internal class _game
    {
        public string codename { get; set; }
        public string optionValue { get; set; }
        public string boostSpec { get; set; }

        public _game(string codename, string optionValue, string boostSpec)
        {
            this.codename = codename;
            this.optionValue = optionValue;
            this.boostSpec = boostSpec;
        }
    }
}
