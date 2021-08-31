using UnityEngine;

namespace DiscoExplorer
{
    public static class DrawGUI
    {
        private static string runSpeed = "";
        private static string skillPoints = "";
        private static string money = "";
        private static string intellect = "";
        private static string psyche = "";
        private static string physique = "";
        private static string motorics = "";
        public static string checkPassFailStatus = "";

        public static void Draw()
        {
            int x, y, w, h;
            int pad = 5;
            w = (int)(Screen.width * (.3));
            h = (int)(Screen.height * (.53));
            x = Screen.width - w - pad;
            y = pad;
            Rect boxPos = new Rect(x + pad, y + pad, w - pad, h - pad);
            Box(x, y, w, h, 7);

            checkPassFailStatus = "Checks : ";
            if (!ChecksPassFail.IsOn())
            {
                checkPassFailStatus += " Unaffected";
            }
            else if (ChecksPassFail.IsPassing())
            {
                checkPassFailStatus += " Always Pass";
            }
            else
            {
                checkPassFailStatus += " Always Fail";
            }

            GUILayout.BeginArea(boxPos);

            GUILayout.Label("Disco Explorer", NoOptions());

            GUILayout.BeginHorizontal(WidthHeight(550,200));
            GUILayout.BeginVertical(WidthHeight(200,200));

            GUILayout.Label("\n--- GENERAL ---", NoOptions());

            // run speed
            GUILayout.Label("Run Speed Multiplier\n[1.0 - 3.0]", NoOptions());
            runSpeed = GUILayout.TextField(runSpeed, 10, WidthHeight(100, 22));
            if (GUILayout.Button("Apply", Width(100)))
            {
                // set speed 
                if (float.TryParse(runSpeed, out var s))
                {
                    RunSpeed.SetRunSpeed(s);
                }
            }

            // skill points
            GUILayout.Label("\nSkill Points\n[0 - 100]", NoOptions());
            skillPoints = GUILayout.TextField(skillPoints, 10, WidthHeight(100, 22));
            if (GUILayout.Button("Apply", Width(100)))
            {
                // set speed 
                if (int.TryParse(skillPoints, out var s))
                {
                    Utilities.SetSkillPoints(s);
                }
            }

            // money
            GUILayout.Label("\nMoney\n[0 - 999]", NoOptions());
            money = GUILayout.TextField(money, 10, WidthHeight(100, 22));
            if (GUILayout.Button("Apply", Width(100)))
            {
                // set speed 
                if (int.TryParse(money, out var s))
                {
                    Utilities.SetMoney(s);
                }
            }

            GUILayout.EndVertical();


            // attributes
            GUILayout.BeginVertical(WidthHeight(200, 200));

            GUILayout.Label("\n--- ATTRIBUTES ---", NoOptions());

            // intellect
            GUILayout.Label("Intellect", NoOptions());
            intellect = GUILayout.TextField(intellect, 10, WidthHeight(100, 22));

            // psyche
            GUILayout.Label("Psyche", NoOptions());
            psyche = GUILayout.TextField(psyche, 10, WidthHeight(100, 22));

            // physique
            GUILayout.Label("Physique", NoOptions());
            physique = GUILayout.TextField(physique, 10, WidthHeight(100, 22));

            // motorics
            GUILayout.Label("Motorics", NoOptions());
            motorics = GUILayout.TextField(motorics, 10, WidthHeight(100, 22));

            // apply attributes
            if (GUILayout.Button("Apply", Width(100)))
            {
                if (int.TryParse(intellect, out var i))
                {
                    Utilities.SetAbilityLevel(Sunshine.Metric.AbilityType.INT, i);
                }
                if (int.TryParse(psyche, out var p))
                {
                    Utilities.SetAbilityLevel(Sunshine.Metric.AbilityType.PSY, p);
                }
                if (int.TryParse(physique, out var ph))
                {
                    Utilities.SetAbilityLevel(Sunshine.Metric.AbilityType.FYS, ph);
                }
                if (int.TryParse(motorics, out var m))
                {
                    Utilities.SetAbilityLevel(Sunshine.Metric.AbilityType.MOT, m);
                }
            }
            GUILayout.EndVertical();

            // Fast travel
            GUILayout.BeginVertical(WidthHeight(200, 200));
            GUILayout.Label("\n--- FAST TRAVEL ---", NoOptions());

            // whirling
            if (GUILayout.Button((FastTravel.CheckVisited(FastTravel.whirling) ? "Whirling-In-Rags" : "Undiscovered"), Width(150)))
            {
                if (FastTravel.CheckVisited(FastTravel.whirling))
                {
                    DiscoExplorerComponent.toggle = false;
                    FastTravel.GoTo(FastTravel.whirling);
                }
            }

            GUILayout.Label("", NoOptions());

            // claire's office
            if (GUILayout.Button((FastTravel.CheckVisited(FastTravel.union) ? "Evrart Claire's Office" : "Undiscovered"), Width(150)))
            {
                if (FastTravel.CheckVisited(FastTravel.union))
                {
                    DiscoExplorerComponent.toggle = false;
                    FastTravel.GoTo(FastTravel.union);
                }
            }

            GUILayout.Label("", NoOptions());

            // pier
            if (GUILayout.Button((FastTravel.CheckVisited(FastTravel.pier) ? "Pier Apartments" : "Undiscovered"), Width(150)))
            {
                if (FastTravel.CheckVisited(FastTravel.pier))
                {
                    DiscoExplorerComponent.toggle = false;
                    FastTravel.GoTo(FastTravel.pier);
                }
            }

            GUILayout.Label("", NoOptions());

            // shack
            if (GUILayout.Button((FastTravel.CheckVisited(FastTravel.shack) ? "Shack on the coast" : "Undiscovered"), Width(150)))
            {
                if (FastTravel.CheckVisited(FastTravel.shack))
                {
                    DiscoExplorerComponent.toggle = false;
                    FastTravel.GoTo(FastTravel.shack);
                }
            }

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();


            GUILayout.Label("\n--- TOGGLES ---", NoOptions());
            GUILayout.BeginHorizontal(WidthHeight(400, 50));

            if (GUILayout.Button(checkPassFailStatus, Width(200)))
            {
                ChecksPassFail.Toggle();
            }

            GUILayout.Label(" ", NoOptions());

            if (GUILayout.Button("Unlock White Checks", Width(200)))
            {
                Utilities.UnlockAllWhiteChecks();
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(WidthHeight(400, 50));

            if (GUILayout.Button("Add All Thoughts", Width(200)))
            {
                Utilities.AddAllThoughts();
            }

            GUILayout.Label(" ", NoOptions());

            if (GUILayout.Button("Finish All Thoughts", Width(200)))
            {
                Utilities.FinishAllThoughts();
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(WidthHeight(400, 50));

            if (GUILayout.Button("Add All Clothes", Width(200)))
            {
                Utilities.AddAllClothes();

            }

            GUILayout.Label(" ", NoOptions());

            // toggle appearance lock
            if (GUILayout.Button("Lock Appearance: " + (FreezeClothing.on ? "ON" : "OFF"), Width(200)))
            {
                FreezeClothing.on = !FreezeClothing.on;
                if (!FreezeClothing.on)
                {
                    FreezeClothing.UpdateClothing();
                }
            }
            GUILayout.EndHorizontal();
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
