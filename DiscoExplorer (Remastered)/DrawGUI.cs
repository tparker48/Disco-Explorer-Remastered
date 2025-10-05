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

            GUILayout.BeginHorizontal(WidthHeight(550, 200));
            GUILayout.BeginVertical(WidthHeight(200, 200));

            GUILayout.Label("\n--- GENERAL ---", NoOptions());

            // Run Speed
            GUILayout.Label("Run Speed Multiplier\n[1.0 - 3.0]", NoOptions());
            runSpeed = GUILayout.TextField(runSpeed, 10, WidthHeight(100, 22));
            if (GUILayout.Button("Apply", Width(100)))
            {
                if (float.TryParse(runSpeed, out var s))
                {
                    RunSpeed.SetRunSpeed(s);
                }
            }

            // Skill Points
            GUILayout.Label("\nSkill Points\n[0 - 100]", NoOptions());
            skillPoints = GUILayout.TextField(skillPoints, 10, WidthHeight(100, 22));
            if (GUILayout.Button("Apply", Width(100)))
            {
                if (int.TryParse(skillPoints, out var s))
                {
                    Utilities.SetSkillPoints(s);
                }
            }

            // Money
            GUILayout.Label("\nMoney\n[0 - 999]", NoOptions());
            money = GUILayout.TextField(money, 10, WidthHeight(100, 22));
            if (GUILayout.Button("Apply", Width(100)))
            {
                if (int.TryParse(money, out var s))
                {
                    Utilities.SetMoney(s);
                }
            }

            GUILayout.EndVertical();

            // Attributes
            GUILayout.BeginVertical(WidthHeight(200, 200));

            GUILayout.Label("\n--- ATTRIBUTES ---", NoOptions());

            // Intellect
            GUILayout.Label("Intellect", NoOptions());
            intellect = GUILayout.TextField(intellect, 10, WidthHeight(100, 22));

            // Psyche
            GUILayout.Label("Psyche", NoOptions());
            psyche = GUILayout.TextField(psyche, 10, WidthHeight(100, 22));

            // Physique
            GUILayout.Label("Physique", NoOptions());
            physique = GUILayout.TextField(physique, 10, WidthHeight(100, 22));

            // Motorics
            GUILayout.Label("Motorics", NoOptions());
            motorics = GUILayout.TextField(motorics, 10, WidthHeight(100, 22));

            // Apply Attributes
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

            // Fast Travel
            GUILayout.BeginVertical(WidthHeight(200, 200));
            GUILayout.Label("\n--- FAST TRAVEL ---", NoOptions());

            // Whirling
            if (GUILayout.Button((FastTravel.CheckVisited(FastTravel.whirling) ? "Whirling-In-Rags" : "Undiscovered"), Width(150)))
            {
                if (FastTravel.CheckVisited(FastTravel.whirling))
                {
                    DiscoExplorerComponent.toggle = false;
                    FastTravel.GoTo(FastTravel.whirling);
                }
            }

            GUILayout.Label("", NoOptions());

            // Claire's Office
            if (GUILayout.Button((FastTravel.CheckVisited(FastTravel.union) ? "Evrart Claire's Office" : "Undiscovered"), Width(150)))
            {
                if (FastTravel.CheckVisited(FastTravel.union))
                {
                    DiscoExplorerComponent.toggle = false;
                    FastTravel.GoTo(FastTravel.union);
                }
            }

            GUILayout.Label("", NoOptions());

            // Pier
            if (GUILayout.Button((FastTravel.CheckVisited(FastTravel.pier) ? "Pier Apartments" : "Undiscovered"), Width(150)))
            {
                if (FastTravel.CheckVisited(FastTravel.pier))
                {
                    DiscoExplorerComponent.toggle = false;
                    FastTravel.GoTo(FastTravel.pier);
                }
            }

            GUILayout.Label("", NoOptions());

            // Shack
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

            // Toggle Appearance Lock
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
                GUI.Box(boxPos, GUIContent.none);
            }
        }

        private static GUILayoutOption[] Width(int w)
        {
            return new GUILayoutOption[] { GUILayout.Width(w) };
        }

        private static GUILayoutOption[] Height(int h)
        {
            return new GUILayoutOption[] { GUILayout.Height(h) };
        }

        private static GUILayoutOption[] WidthHeight(int w, int h)
        {
            return new GUILayoutOption[]
            {
                GUILayout.Width(w),
                GUILayout.Height(h)
            };
        }

        private static GUILayoutOption[] NoOptions()
        {
            return new GUILayoutOption[0];
        }
    }
}
