using System;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DiscoExplorer
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepInExLoader : BasePlugin
    {
        public const string
            MODNAME = "DiscoExplorerRemastered",
            AUTHOR = "tparker48",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1.0.0.0";

        public static BepInEx.Logging.ManualLogSource log;

        public BepInExLoader()
        {
            log = Log;
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
            catch (Exception e)
            {
                log.LogError("[DiscoExplorer] FAILED to Register Il2Cpp Type: DiscoExplorerComponent!");
                log.LogError(e.ToString());
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
            }
            catch (Exception e)
            {
                log.LogError("[DiscoExplorer] FAILED to Apply Patches!");
                log.LogError(e.ToString());
            }
        }
    }
}
