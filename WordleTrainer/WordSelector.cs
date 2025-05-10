using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleTrainer
{
    public class WordSelector
    {
        private static readonly List<string> FiveLetterWords = new List<string>
        {
        // 20 words for each letter A-Z
        "ABOUT", "ABOVE", "ABUSE", "ACTOR", "ACUTE", "ADAPT", "ADMIT", "ADOPT", "ADORE", "ADULT",
        "AFTER", "AGAIN", "AGENT", "AGILE", "AGING", "AISLE", "ALBUM", "ALERT", "ALIEN", "ALIGN",

        "BADGE", "BADLY", "BAFFLE", "BAKER", "BALKY", "BALLS", "BALSA", "BANDY", "BANGS", "BANJO",
        "BARGE", "BARKS", "BARNY", "BARRE", "BASIC", "BASIS", "BATHE", "BATON", "BAWDY", "BAYOU",

        "CABLE", "CACHE", "CADET", "CADGE", "CADRE", "CAFFE", "CAGED", "CAKES", "CALLS", "CALVE",
        "CAMEO", "CAMPS", "CANAL", "CANNY", "CANOE", "CANON", "CAPER", "CAPES", "CAPON", "CARAT",

        "DAILY", "DAINTY", "DAIRY", "DALLY", "DAMAGED", "DAMPER", "DANCED", "DANCES", "DANDY", "DANGER",
        "DAPPER", "DARING", "DARKLY", "DARTS", "DASHED", "DATER", "DATES", "DAUBED", "DAUGHT", "DAUNT",

        "EAGLE", "EARLS", "EARLY", "EARNS", "EARTH", "EASED", "EASEL", "EASES", "EASTER", "EATEN",
        "EATER", "EAVES", "EBBED", "EBBING", "ECHOED", "EDGED", "EDGES", "EERIE", "EJECT", "ELBOW",

        "FABLE", "FACED", "FACES", "FACET", "FAILS", "FAINT", "FAIRS", "FAITH", "FALKE", "FANCY",
        "FARCE", "FARMS", "FASTS", "FATAL", "FATED", "FATES", "FATTY", "FAUNA", "FAVOR", "FAWNS",

        "GABBY", "GADGE", "GAFFE", "GAINS", "GALAS", "GALES", "GALLS", "GAMED", "GAMES", "GAMMY",
        "GAPER", "GAPES", "GARBS", "GASPS", "GATES", "GAUDY", "GAUNT", "GAUZE", "GAVEL", "GAWKY",

        "HABIT", "HACKS", "HAILS", "HAIRY", "HAKIM", "HALED", "HALES", "HALLO", "HALSE", "HANDS",
        "HANGS", "HAPLY", "HAPPY", "HARDS", "HARMS", "HARPS", "HARSH", "HASPS", "HASTE", "HATCH",

        "IMAGE", "IMBED", "IMBUE", "IMIDE", "IMMUNE", "IMPACT", "IMPLY", "INANE", "INCUS", "INDEX",
        "INPUT", "IRATE", "IRKED", "IRONY", "ISLE", "ISSUE", "ITCHY", "ITEMS", "IVORY", "IXIAS",

        "JABOT", "JADES", "JAILS", "JAMBS", "JAZZY", "JEANS", "JERKS", "JESTS", "JEWEL", "JIBED",
        "JIBES", "JIFFY", "JILTS", "JINNI", "JINXED", "JOKED", "JOKER", "JOLLY", "JOLTS", "JOUST",

        "KABOB", "KACHA", "KAFIR", "KAMES", "KARAT", "KAZOO", "KEELS", "KEENS", "KEEPS", "KELPS",
        "KERBS", "KERFS", "KERNE", "KESTS", "KETCH", "KEYED", "KIBLA", "KICKS", "KIDDO", "KILNS",

        "LABOR", "LACED", "LACES", "LACKS", "LADDY", "LAGER", "LAIRS", "LAKES", "LAMBS", "LAMPS",
        "LANDS", "LAPEL", "LAPSE", "LARCH", "LARDS", "LARGE", "LARVA", "LASER", "LATCH", "LATER",

        "MAGIC", "MAILS", "MAIMS", "MAIZE", "MAKER", "MAKES", "MALAR", "MALES", "MALTS", "MAMBA",
        "MANGA", "MANGO", "MANIA", "MANLY", "MAPLE", "MARCH", "MARIA", "MARKS", "MARRY", "MASKS",

        "NABOB", "NAILS", "NAIVE", "NAKED", "NAMED", "NAMES", "NANAS", "NARCO", "NARRA", "NASAL",
        "NASTY", "NATAL", "NAVAL", "NAVES", "NAVY", "NEEDY", "NEGRO", "NEIGH", "NERDS", "NERVY",

        "OAKEN", "OATER", "OBESE", "OCEAN", "ODDLY", "ODIUM", "OFFAL", "OFTEN", "OHMIC", "OILER",
        "OKAPI", "OKAYS", "OLDEN", "OLEIN", "OMENS", "OMITS", "ONSET", "OPALS", "OPERA", "OPINE",

        "PACED", "PACES", "PACKS", "PADGE", "PADRE", "PAEAN", "PAGED", "PAGES", "PAILS", "PAINS",
        "PAIRS", "PAISE", "PALER", "PALES", "PALLS", "PALMS", "PANEL", "PANGA", "PANTS", "PAPAL",

        "QUACK", "QUAD", "QUAIL", "QUAKE", "QUALM", "QUANT", "QUARK", "QUART", "QUASH", "QUAYS",
        "QUEEN", "QUEST", "QUEUE", "QUICK", "QUIET", "QUILL", "QUILT", "QUIPS", "QUOTA", "QUOTE",

        "RABAT", "RACKS", "RADAR", "RADIO", "RAFTS", "RAGAS", "RAIDS", "RAILS", "RAINS", "RAISE",
        "RAKES", "RALLY", "RAMPS", "RANCH", "RANDS", "RANGE", "RAPID", "RAPS", "RARER", "RASPS",

        "SABLE", "SACKS", "SADLY", "SAFES", "SAGAS", "SAGGY", "SAILS", "SAINTS", "SALAD", "SALES",
        "SALTS", "SALVE", "SAMBA", "SANDS", "SANER", "SAPID", "SARAN", "SARIS", "SATIN", "SATYR",

        "TABLE", "TABOO", "TACIT", "TAFFY", "TAILS", "TAINT", "TAKEN", "TAKER", "TAKES", "TALCS",
        "TALES", "TALKY", "TAMED", "TAMES", "TAMMY", "TANGO", "TANGY", "TAPED", "TAPES", "TAROT",

        "UDDER", "ULCER", "ULTRA", "UMBRA", "UNAPT", "UNDER", "UNFIT", "UNIFY", "UNION", "UNITE",
        "UNITY", "UNLIT", "UNMET", "UNPEG", "UNSET", "UNTIL", "UPEND", "UPPER", "UPSET", "URBAN",

        "VACUA", "VAGUE", "VALES", "VALET", "VALID", "VALUE", "VAPID", "VARIA", "VAULT", "VEERS",
        "VEGAN", "VEILS", "VENAL", "VENDS", "VERGE", "VERVE", "VETOS", "VEXED", "VIBES", "VICAR",

        "WACKY", "WAFTS", "WAGED", "WAGER", "WAGES", "WAIFS", "WAILS", "WAIST", "WAKEN", "WALES",
        "WALKS", "WALTZ", "WANDS", "WANED", "WANTS", "WARMS", "WARNS", "WARPS", "WASTE", "WATCH",

        "XEBEC", "XENIA", "XENIC", "XENON", "XERIC", "XEROX", "XIPHI", "XLIII", "XRAYS", "XYLEM",
        "YACHT", "YACKS", "YAHOO", "YAMEN", "YAPON", "YARDS", "YARNS", "YAWED", "YAWLS", "YEARN",

        "ZAPPY", "ZARFS", "ZAXES", "ZEALS", "ZEBRA", "ZESTS", "ZIGZAG", "ZINCS", "ZINGS", "ZIPPY"
        };

        public static string GetRandomWord()
        {
            Random random = new Random();
            int randomIndex = random.Next(FiveLetterWords.Count);

            // Ensure the word is exactly 5 letters long - some are not - word list generated by ChatGPT and could be wrong - implement api later???
            while (FiveLetterWords[randomIndex].Length != 5)
            {
                randomIndex = random.Next(FiveLetterWords.Count);
            }

            return FiveLetterWords[randomIndex];
        }
    }

}
