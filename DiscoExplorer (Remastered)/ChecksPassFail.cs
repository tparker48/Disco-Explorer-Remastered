using HarmonyLib;

namespace DiscoExplorer
{
    public static class ChecksPassFail
    {
        private static bool on = false;
        private static bool passing = false;

        public static void Toggle()
        {
            if (!on)
            {
                on = true;
                passing = true;
            }
            else if (passing)
            {
                passing = false;
            }
            else
            {
                on = false;
            }

            BepInExLoader.log.LogMessage("ON is " + on.ToString());
            BepInExLoader.log.LogMessage("PASSING is " + passing.ToString());
        }

        public static bool IsOn()
        {
            return on;
        }

        public static bool IsPassing()
        {
            return passing;
        }

        public static int GetDieValue()
        {
            // roll 10 or 0
            if (passing)
            {
                return 20;
            }
            else
            {
                return 0;
            }
        }

        static void Total(ref int __result)
        {
            BepInExLoader.log.LogMessage("[Disco Explorer] Rolling dice...");
            if (ChecksPassFail.IsOn())
            {
                BepInExLoader.log.LogMessage("[Disco Explorer] Dice fixed at " + ChecksPassFail.GetDieValue());
                __result = ChecksPassFail.GetDieValue();
            }
        }

        public static void ApplyPatches()
        {
            var harmony = new Harmony("tparker48.DiscoElysium.il2cpp");
            var originalTotal = AccessTools.Method(typeof(Sunshine.Metric.CheckResult), "Total");
            var postTotal = AccessTools.Method(typeof(ChecksPassFail), "Total");
            harmony.Patch(originalTotal, postfix: new HarmonyMethod(postTotal));

            BepInExLoader.log.LogMessage("[DiscoExplorer] Checks Pass/Fail Patches Applied");
        }   
    }
}
