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
            // X
            if (Input.GetKeyInt(BepInEx.IL2CPP.UnityEngine.KeyCode.X) && Event.current.type == EventType.KeyDown)
            {
                toggle = !toggle;

                if (Sunshine.Views.ViewController.Current != Sunshine.Views.ViewType.INVENTORY)
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