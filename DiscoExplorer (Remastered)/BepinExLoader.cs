using BepInEx;
using UnhollowerRuntimeLib;
using HarmonyLib;
using UnityEngine;
using BepInEx.Configuration;
using System.IO;
using Object = UnityEngine.Object;

namespace DiscoExplorer
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepInExLoader : BepInEx.IL2CPP.BasePlugin
    {
        public const string
            MODNAME = "DiscoExplorer",
            AUTHOR = "tparker48",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1.0.0.0";

        public static BepInEx.Logging.ManualLogSource log;

        public BepInExLoader()
        {
            log = Log;
            Config_Init();
        }

        public override void Load()
        {
            log.LogMessage("[DiscoExplorer] Registering DiscoExplorerComponent in Il2Cpp");

            try
            {
                // Register our custom Types in Il2Cpp
                ClassInjector.RegisterTypeInIl2Cpp<DiscoExplorerComponent>();

                var go = new GameObject("DiscoExplorerObject");
                go.AddComponent<DiscoExplorerComponent>();
                Object.DontDestroyOnLoad(go);
            }
            catch
            {
                log.LogError("[DiscoExplorer] FAILED to Register Il2Cpp Type: DiscoExplorerComponent!");
            }

            try
            {
                var harmony = new Harmony("tparker48.DiscoElysium.il2cpp");

                var originalAwake = AccessTools.Method(typeof(HudController), "Awake");
                var postAwake = AccessTools.Method(typeof(DiscoExplorerComponent), "Awake");
                harmony.Patch(originalAwake, postfix: new HarmonyMethod(postAwake));

                var originalStart = AccessTools.Method(typeof(MainMenuList), "Start");
                var postStart = AccessTools.Method(typeof(DiscoExplorerComponent), "Start");
                harmony.Patch(originalStart, postfix: new HarmonyMethod(postStart));

                var originalUpdate = AccessTools.Method(typeof(NavigationManager), "Update");
                var postUpdate = AccessTools.Method(typeof(DiscoExplorerComponent), "Update");
                harmony.Patch(originalUpdate, postfix: new HarmonyMethod(postUpdate));

                var originalDraw = AccessTools.Method(typeof(BorderDebugDrawer), "OnGUI");
                var postDraw = AccessTools.Method(typeof(DiscoExplorerComponent), "OnGUI");
                harmony.Patch(originalDraw, postfix: new HarmonyMethod(postDraw));

                log.LogMessage("[DiscoExplorer] Core Patches Applied");

                RunSpeed.ApplyPatches();
                FastTravelPatches.ApplyPatches();
                FreezeClothingPatches.ApplyPatches();
                ChecksPassFail.ApplyPatches();

                log.LogMessage("[DiscoExplorer] Applied All Patches");

                /* ---------- PATCH TEMPLATE ---------
                // [NAME]
                var original[NAME] = AccessTools.Method(typeof([CLASS]), "[METHOD]");
                log.LogMessage("[DiscoExplorer] Harmony - Original Method: " + original[NAME].DeclaringType.Name + "." + original[NAME].Name);
                var post[NAME] = AccessTools.Method(typeof(DiscoExplorerComponent), "[METHOD]");
                log.LogMessage("[DiscoExplorer] Harmony - Postfix Method: " + post[NAME].DeclaringType.Name + "." + post[NAME].Name);
                harmony.Patch(original[NAME], postfix: new HarmonyMethod(post[NAME]));
                log.LogMessage("[DiscoExplorer] Harmony - Runtime Patch's Applied");
                 */

            }
            catch
            {
                log.LogError("[DiscoExplorer] FAILED to Apply Patches!");
            }
        }
    
        public void Config_Init()
        {
            // Create a new configuration file.
            // First argument is the path to where the configuration is saved
            // Second arguments specifes whether to create the file right away or whether to wait until any values are accessed/written
            var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "custom_config.cfg"), true);

            // You can now create configuration wrappers for it
            var userName = customFile.Bind("General",
                "UserName",
                "Deuce",
                "Name of the user");

            // In plug-ins, you can still access the default configuration file
            var configGreeting = Config.Bind("General",
                "GreetingTest",
                "Hello, world!",
                "A greeting text to show when the game is launched");
        }
    }
}
