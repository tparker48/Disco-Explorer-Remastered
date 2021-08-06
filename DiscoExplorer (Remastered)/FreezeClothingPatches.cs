using System;
using HarmonyLib;

namespace DiscoExplorer
{
    class FreezeClothingPatches
    {
        // toggle clothing change is not ready before load
        static bool LoadDataAfterLoadingAreaPrefix()
        {
            FreezeClothing.ready = false;
            return true;
        }
        
        // toggle clothing change is ready after load
        static void LoadDataAfterLoadingAreaPostfix()
        {
            FreezeClothing.ready = true;
            FreezeClothing.Init();
        }
        
        //[HarmonyPatch(new Type[] { typeof(string), typeof(bool) })]
        static bool EquipAndBlend(string itemName)
        {
            BepInExLoader.log.LogMessage("Equipping " + itemName);
            if (!FreezeClothing.updatingClothes)
            {
                string itemType = itemName.Split('_')[0];

                for (int i = 0; i < FreezeClothing.currentOutfit.Count; i++)
                {
                    string type = FreezeClothing.currentOutfit[i].Split('_')[0];
                    if (type == itemType)
                    {
                        FreezeClothing.currentOutfit.Remove(FreezeClothing.currentOutfit[i]);
                    }
                }

                FreezeClothing.currentOutfit.Add(itemName);
                if (!FreezeClothing.on) FreezeClothing.originalOutfit.Add(itemName);

                if (FreezeClothing.ready)
                {
                    return !FreezeClothing.on;
                }
            }
        
            return true;
        }
        
        private static string GetClothingType(string itemName)
        {
            if (itemName.StartsWith( "jacket"))
            {
                return "jacket";
            }
            if (itemName.StartsWith( "pants"))
            {
                return "pants";
            }
            if (itemName.StartsWith( "shirt"))
            {
                return "shirt";
            }
            if (itemName.StartsWith( "shoes"))
            {
                return "shoes";
            }
            if (itemName.StartsWith( "gloves"))
            {
                return "gloves";
            }
            if (itemName.StartsWith( "hat"))
            {
                return "hat";
            }
            if (itemName.StartsWith( "glasses"))
            {
                return "glasses";
            }
            if (itemName.StartsWith( "neck"))
            {
                return "neck";
            }

            return "";
        }

        static bool Unequip(string itemName)
        {
            BepInExLoader.log.LogMessage("Unequiping " + itemName);
            if (!FreezeClothing.updatingClothes)
            {
                FreezeClothing.currentOutfit.Remove(itemName);
                if (!FreezeClothing.on) FreezeClothing.originalOutfit.Remove(itemName);
        
                if (FreezeClothing.ready)
                {
                    return !FreezeClothing.on;
                }
            }
        
            return true;
        }
        
        static bool EquipHeadWear(string itemName)
        {
            if (!FreezeClothing.updatingClothes)
            {
                FreezeClothing.currentHeadwear.Add(itemName);
                if (!FreezeClothing.on) FreezeClothing.originalHeadwear.Add(itemName);
        
                if (FreezeClothing.ready)
                {
                    return !FreezeClothing.on;
                }
            }

            return true;
        }

        static bool UnequipHeadWear(string itemName)
        {
            if (!FreezeClothing.updatingClothes)
            {
                FreezeClothing.currentHeadwear.Remove(itemName);
                if (!FreezeClothing.on) FreezeClothing.originalHeadwear.Remove(itemName);
        
                if (FreezeClothing.ready)
                {
                    return !FreezeClothing.on;
                }
            }
        
            return true;
        }

        public static void ApplyPatches()
        {
            var harmony = new Harmony("tparker48.DiscoElysium.il2cpp");

            var originalReadyPre = AccessTools.Method(typeof(SunshinePersistenceLoadDataManager), "LoadDataAfterLoadingArea");
            var postReadyPre = AccessTools.Method(typeof(FreezeClothingPatches), "LoadDataAfterLoadingAreaPrefix");
            harmony.Patch(originalReadyPre, prefix: new HarmonyMethod(postReadyPre));

            var originalReadyPost = AccessTools.Method(typeof(SunshinePersistenceLoadDataManager), "LoadDataAfterLoadingArea");
            var postReadyPost = AccessTools.Method(typeof(FreezeClothingPatches), "LoadDataAfterLoadingAreaPostfix");
            harmony.Patch(originalReadyPost, postfix: new HarmonyMethod(postReadyPost));

            var originalEquip = AccessTools.Method(typeof(TequilaClothing), "EquipAndBlend");
            var postEquip = AccessTools.Method(typeof(FreezeClothingPatches), "EquipAndBlend");
            harmony.Patch(originalEquip, prefix: new HarmonyMethod(postEquip));

            var originalUnequip = AccessTools.Method(typeof(TequilaClothing), "Unequip");
            var postUnequip = AccessTools.Method(typeof(FreezeClothingPatches), "Unequip");
            harmony.Patch(originalUnequip, prefix: new HarmonyMethod(postUnequip));

            var originalEquipHeadwear = AccessTools.Method(typeof(TequilaClothingHeadwear), "EquipHeadWear");
            var postEquipHeadwear = AccessTools.Method(typeof(FreezeClothingPatches), "EquipHeadWear");
            harmony.Patch(originalEquipHeadwear, prefix: new HarmonyMethod(postEquipHeadwear));

            var originalUnequipHeadwear = AccessTools.Method(typeof(TequilaClothingHeadwear), "UnequipHeadWear");
            var postUnequipHeadwear = AccessTools.Method(typeof(FreezeClothingPatches), "UnequipHeadWear");
            harmony.Patch(originalUnequipHeadwear, prefix: new HarmonyMethod(postUnequipHeadwear));

            BepInExLoader.log.LogMessage("[DiscoExplorer] Freeze Clothing Patches Applied");
        }
    }
}
