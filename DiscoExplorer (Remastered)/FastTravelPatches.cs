using HarmonyLib;

namespace DiscoExplorer
{
    public static class FastTravelPatches
    {
        static bool SaveCoR(string fileNamePrefix)
        {
            fileNamePrefix = SunshinePersistenceFileManager.ReplaceInvalidFileNameChars(fileNamePrefix);
            fileNamePrefix = SunshinePersistenceFileManager.PutDateSuffixOnPath(fileNamePrefix);

            FastTravel.Save(fileNamePrefix);
            return true;
        }

        public static bool Load(string fileName)
        {
            FastTravel.Load(fileName);
            return true;
        }
  
        public static bool ChangeArea(string areaId, string destinationId)
        {
            if (areaId == FastTravel.areaId)
            {
                FastTravel.AddVisited(destinationId);
            }

            return true;
        }

        public static void ApplyPatches()
        {
            var harmony = new Harmony("tparker48.DiscoElysium.il2cpp");

            var originalSave = AccessTools.Method(typeof(SunshinePersistence), "SaveCoR");
            var postSave = AccessTools.Method(typeof(FastTravelPatches), "SaveCoR");
            harmony.Patch(originalSave, prefix: new HarmonyMethod(postSave));

            var originalLoad = AccessTools.Method(typeof(SunshinePersistence), "Load");
            var postLoad = AccessTools.Method(typeof(FastTravelPatches), "Load");
            harmony.Patch(originalLoad, prefix: new HarmonyMethod(postLoad));

            var originalMove = AccessTools.Method(typeof(FortressOccident.ApplicationManager), "ChangeArea");
            var postMove = AccessTools.Method(typeof(FastTravelPatches), "ChangeArea");
            harmony.Patch(originalMove, prefix: new HarmonyMethod(postMove));

            BepInExLoader.log.LogMessage("[DiscoExplorer] Fast Travel Patches Applied");
        }
    }
}
