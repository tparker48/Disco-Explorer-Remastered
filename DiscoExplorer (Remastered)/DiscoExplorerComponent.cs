using System;
using System.Collections.Generic;
using BepInEx;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib;
using HarmonyLib;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

namespace DiscoExplorer
{
    public class DiscoExplorerComponent : MonoBehaviour
    {
        static bool toggle = false;

        public DiscoExplorerComponent(IntPtr ptr) : base(ptr)
        {
            BepInExLoader.log.LogMessage("[DiscoExplorer] Entered Constructor");
        }

        [HarmonyPostfix]
        public static void Awake()
        {
            BepInExLoader.log.LogMessage("[DiscoExplorer] I'm Awake!");
        }

        [HarmonyPostfix]
        public static void Start()
        {
            BepInExLoader.log.LogMessage("[DiscoExplorer] I'm Starting Up...");
        }

        [HarmonyPostfix]
        public static void Update()
        {
            // Note the difference for getting keypress.
            if (Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.Backslash) && Event.current.type == EventType.KeyDown)
            {
                BepInExLoader.log.LogMessage("[DiscoExplorer] Backslash detected!");
                toggle = !toggle;

                // Tests

                // Utilities.SetSkillPoints(99);
                // Utilities.SetMoney(777);
                // Utilities.AddAllClothes();
                // Utilities.AddAllThoughts();
                // Utilities.ToggleHud();
                // Utilities.FinishAllThoughts();
                // Utilities.UnlockAllWhiteChecks();
                // if (toggle) RunSpeed.SetRunSpeed(3f);
                // else RunSpeed.SetRunSpeed(1f);
                // 
                // FastTravel.GoTo(FastTravel.whirling);

                FreezeClothing.on = !FreezeClothing.on;

                if (!FreezeClothing.on)
                {
                    FreezeClothing.UpdateClothing();
                }

                Event.current.Use();
            }
        }
    }
}