using HarmonyLib;
using UnityEngine;

namespace DiscoExplorer
{
    public static class RunSpeed
    {
        public static float speed = 1.0f;

        public static void SetRunSpeed(float desiredSpeed)
        {
            if (desiredSpeed >= 1.0f && desiredSpeed <= 3.0f)
            {
                speed = desiredSpeed;
            }
        }

        public static void get_deltaPosition(ref Vector3 __result)
        {
            __result *= speed;
        }

        public static void ApplyPatches()
        {
            var harmony = new Harmony("tparker48.DiscoElysium.il2cpp");
            var originalSpeed = AccessTools.Method(typeof(Animator), "get_deltaPosition");
            var postSpeed = AccessTools.Method(typeof(RunSpeed), "get_deltaPosition");
            harmony.Patch(originalSpeed, postfix: new HarmonyMethod(postSpeed));

            BepInExLoader.log.LogMessage("[DiscoExplorer] Run Speed Patches Applied");
        }
    }
}
