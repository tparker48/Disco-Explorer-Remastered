using System;
using System.IO;
using System.Collections.Generic;
using Sunshine;

namespace DiscoExplorer
{
    public static class FastTravel
    {
        // destinations
        public static string whirling = "whirling";
        public static string shack = "second-home";
        public static string union = "union-boss";
        public static string pier = "apartments-pier-1";

        public static string areaId = "Martinaise-ext";
        private static List<string> locations = new List<string>();

        private static string dir = Path.Combine(Environment.CurrentDirectory, "BepInEx", "plugins", "DiscoExplorer", "FastTravelData");
        private static System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(locations.GetType());


        // load the list of available fast travel locations for this save
        public static void Load(string saveName)
        {
            if (!LoadData(saveName))
            {
                InitData();
            }
        }

        // save the list of available fast travel locations for this save
        public static void Save(string saveName)
        {
            try
            {
                StreamWriter writer = new StreamWriter(Path.Combine(dir, CleanFileName(saveName)));
                serializer.Serialize(writer, locations);
                writer.Close();
            }
            catch
            {

                BepInExLoader.log.LogMessage("[Disco Explorer] Could not save fast travel locations");
            }
        }

        // add location to available fast travel locations
        public static void AddVisited(string destination)
        {
            if (destination == whirling || destination == shack || destination == pier || destination == union)
            {
                if (!locations.Contains(destination))
                {
                    locations.Add(destination);
                }
            }
        }

        // returns true if the target destination is available for this save
        public static bool CheckVisited(string destination)
        {
            return locations.Contains(destination);
        }

        // fast travel to desired destination
        public static void GoTo(string destination)
        {
            if (CheckVisited(destination))
            {
                Sunshine.Dialogue.MapLuaFunctions.GoToDestination(FastTravel.areaId, destination);
            }
        }


        private static void InitData()
        {
            locations = new List<string>();
        }

        private static bool LoadData(string saveName)
        {
            try
            {
                BepInExLoader.log.LogMessage("[Disco Explorer] Loading Fast travel data: " + saveName);

                FileStream fs = new FileStream(Path.Combine(dir, CleanFileName(saveName)), FileMode.Open);
                TextReader reader = new StreamReader(fs);
                locations = (List<string>)serializer.Deserialize(reader);
                reader.Close();
                fs.Close();
                return true;
            }
            catch
            {
                BepInExLoader.log.LogMessage("[Disco Explorer] Load failed!");
                return false;
            }
        }

        private static string CleanFileName(string saveName)
        {
            int milliseconds = saveName.LastIndexOf('-');
            saveName = saveName.Substring(0, milliseconds) + ")";
            return saveName;
        }
    }
}
