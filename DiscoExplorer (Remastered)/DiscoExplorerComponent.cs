using System;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Input = BepInEx.Unity.IL2CPP.UnityEngine.Input;
using KeyCode = BepInEx.Unity.IL2CPP.UnityEngine.KeyCode;

namespace DiscoExplorer
{
    public class DiscoExplorerComponent : MonoBehaviour
    {
        public static bool toggle = false;

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
            BorderDebugDrawer.Singleton.enabled = true;

            // X key to toggle the GUI
            if (Input.GetKeyInt(KeyCode.X) && Event.current.type == EventType.KeyDown)
            {
                toggle = !toggle;

                if (Sunshine.Views.ViewController.Current != Sunshine.Views.ViewType.INVENTORY)
                {
                    Sunshine.Views.ViewController.ToggleView(Sunshine.Views.ViewType.INVENTORY, false);
                }

                Event.current.Use();
            }

            // Escape key to close the GUI
            if (toggle && (Input.GetKeyInt(KeyCode.Escape) && Event.current.type == EventType.KeyDown))
            {
                toggle = false;

                if (Sunshine.Views.ViewController.Current == Sunshine.Views.ViewType.INVENTORY)
                {
                    Sunshine.Views.ViewController.ToggleView(Sunshine.Views.ViewType.INVENTORY, false);
                }

                Event.current.Use();
            }
        }

        [HarmonyPostfix]
        public static void OnGUI()
        {
            if (toggle)
            {
                DrawGUI.Draw();
            }
        }
    }
}
