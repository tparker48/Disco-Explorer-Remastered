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

        // PATCHES
        [HarmonyPostfix]
        public static void get_deltaPosition(ref Vector3 __result)
        {
            __result *= speed;
        }
    }
}
