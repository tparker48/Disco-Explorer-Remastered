using BepInEx;
using UnhollowerRuntimeLib;
using HarmonyLib;
using UnityEngine;
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
                var harmony = new Harmony("wh0am15533.trainer.il2cpp");

                // Our Primary Unity Event Hooks 

                // Awake
                var originalAwake = AccessTools.Method(typeof(HudController), "Awake");
                log.LogMessage("[DiscoExplorer] Harmony - Original Method: " + originalAwake.DeclaringType.Name + "." + originalAwake.Name);
                var postAwake = AccessTools.Method(typeof(DiscoExplorerComponent), "Awake");
                log.LogMessage("[DiscoExplorer] Harmony - Postfix Method: " + postAwake.DeclaringType.Name + "." + postAwake.Name);
                harmony.Patch(originalAwake, postfix: new HarmonyMethod(postAwake));

                // Start
                var originalStart = AccessTools.Method(typeof(MainMenuList), "Start");
                log.LogMessage("[DiscoExplorer] Harmony - Original Method: " + originalStart.DeclaringType.Name + "." + originalStart.Name);
                var postStart = AccessTools.Method(typeof(DiscoExplorerComponent), "Start");
                log.LogMessage("[DiscoExplorer] Harmony - Postfix Method: " + postStart.DeclaringType.Name + "." + postStart.Name);
                harmony.Patch(originalStart, postfix: new HarmonyMethod(postStart));

                // Update
                var originalUpdate = AccessTools.Method(typeof(NavigationManager), "Update");
                log.LogMessage("[DiscoExplorer] Harmony - Original Method: " + originalUpdate.DeclaringType.Name + "." + originalUpdate.Name);
                var postUpdate = AccessTools.Method(typeof(DiscoExplorerComponent), "Update");
                log.LogMessage("[DiscoExplorer] Harmony - Postfix Method: " + postUpdate.DeclaringType.Name + "." + postUpdate.Name);
                harmony.Patch(originalUpdate, postfix: new HarmonyMethod(postUpdate));
                log.LogMessage("[DiscoExplorer] Harmony - Runtime Patch's Applied");

                // Speed
                var originalSpeed = AccessTools.Method(typeof(Animator), "get_deltaPosition");
                log.LogMessage("[DiscoExplorer] Harmony - Original Method: " + originalSpeed.DeclaringType.Name + "." + originalSpeed.Name);
                var postSpeed = AccessTools.Method(typeof(RunSpeed), "get_deltaPosition");
                log.LogMessage("[DiscoExplorer] Harmony - Postfix Method: " + postSpeed.DeclaringType.Name + "." + postSpeed.Name);
                harmony.Patch(originalSpeed, postfix: new HarmonyMethod(postSpeed));
                log.LogMessage("[DiscoExplorer] Harmony - Runtime Patch's Applied");

                /* ---------- PATCH TEMPLATE ---------
                // NAME
                var original[NAME] = AccessTools.Method(typeof(NavigationManager), "[METHOD]");
                log.LogMessage("[DiscoExplorer] Harmony - Original Method: " + originalUpdate.DeclaringType.Name + "." + originalUpdate.Name);
                var post[NAME] = AccessTools.Method(typeof(DiscoExplorerComponent), "[METHOD]");
                log.LogMessage("[DiscoExplorer] Harmony - Postfix Method: " + postUpdate.DeclaringType.Name + "." + postUpdate.Name);
                harmony.Patch(originalUpdate, postfix: new HarmonyMethod(post[NAME]));
                log.LogMessage("[DiscoExplorer] Harmony - Runtime Patch's Applied");
                 */

            }
            catch
            {
                log.LogError("[DiscoExplorer] Harmony - FAILED to Apply Patch's!");
            }

            
        }
    }
}