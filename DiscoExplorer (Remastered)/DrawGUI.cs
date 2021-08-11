using BepInEx;
using UnhollowerRuntimeLib;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DiscoExplorer
{
    public static class DrawGUI
    {
        private static string skillPoints = "";
        public static void Draw()
        {
            int x, y, w, h;
            int pad = 5;
            w = (int)(Screen.width * (.35));
            h = (int)(Screen.height * (.6));
            x = Screen.width - w - pad;
            y = pad;
            Rect boxPos = new Rect(x + pad, y + pad, w - pad, h - pad);

            Box(x, y, w, h, 5);
            GUILayout.BeginArea(boxPos);
            skillPoints = GUILayout.TextField(skillPoints,10, WidthHeight(100,22));
            GUILayout.EndArea();

        }

        private static void Box(int x, int y, int w, int h, int darkness = 1)
        {
            Rect boxPos = new Rect(x, y, w, h);

            for (int i = 0; i < darkness; i++)
            {
                GUI.Box(boxPos, new GUIContent());
            }
        }

        private static UnhollowerBaseLib.Il2CppReferenceArray<GUILayoutOption> Width(int w)
        {
            return new UnhollowerBaseLib.Il2CppReferenceArray<GUILayoutOption>(1) { [0] = GUILayout.Width(w) };
        }

        private static UnhollowerBaseLib.Il2CppReferenceArray<GUILayoutOption> Height(int h)
        {
            return new UnhollowerBaseLib.Il2CppReferenceArray<GUILayoutOption>(1) { [0] = GUILayout.Height(h) };
        }

        private static UnhollowerBaseLib.Il2CppReferenceArray<GUILayoutOption> WidthHeight(int w, int h)
        {
            return new UnhollowerBaseLib.Il2CppReferenceArray<GUILayoutOption>(2) { [0] = GUILayout.Width(w), [1] = GUILayout.Height(h) };
        }

        private static UnhollowerBaseLib.Il2CppReferenceArray<GUILayoutOption> NoOptions()
        {
            return new UnhollowerBaseLib.Il2CppReferenceArray<GUILayoutOption>(0);
        }
    }

    public class test : MonoBehaviour
    { 
    }
}
