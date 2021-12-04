using System;
using HarmonyLib;
using UnityEngine;
using Input = BepInEx.IL2CPP.UnityEngine.Input;

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

            // X
            if (Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.X) && Event.current.type == EventType.KeyDown)
            {
                // BepInExLoader.log.LogMessage("[DiscoExplorer] X Pressed");
                toggle = !toggle;

                if (Sunshine.Views.ViewController.Current != Sunshine.Views.ViewType.INVENTORY)
                { 
                    Sunshine.Views.ViewController.ToggleView(Sunshine.Views.ViewType.INVENTORY, false);
                }

                Event.current.Use();
            }

            // Esc
            if (toggle && (Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.Escape) && Event.current.type == EventType.KeyDown))
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
            // BepInExLoader.log.LogMessage("[DiscoExplorer] OnGUI");
            if (toggle)
            {
                DrawGUI.Draw();
            }
        }
    } 
}