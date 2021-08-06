using System.Collections.Generic;

namespace DiscoExplorer
{
    static class FreezeClothing
    {
        public static bool on = false;

        public static bool ready = false;
        public static bool updatingClothes = false;

        public static List<string> originalOutfit = new List<string>();
        public static List<string> currentOutfit = new List<string>();

        public static List<string> originalHeadwear = new List<string>();
        public static List<string> currentHeadwear = new List<string>();

        public static void Init()
        {
            int numTypes = 10;
            string[] types = { "HELDLEFT", "HELDRIGHT", "HAT", "GLASSES", "NECK", "SHIRT", "JACKET", "PANTS", "SHOES", "GLOVES" };

            for (int i = 0; i < numTypes; i++)
            {
                string clothing_item = Sunshine.Metric.InventoryViewData.Singleton.GetEquipped(types[i]);
                if (Sunshine.Metric.InventoryViewData.Singleton.GetEquipped(types[i]) != "")
                {
                    BepInExLoader.log.LogMessage("ADDING " + clothing_item + " on load");

                    if (clothing_item == "HAT" || clothing_item == "GLASSES")
                    {
                        originalHeadwear.Add(clothing_item);
  
                    }
                    else
                    {
                        originalOutfit.Add(clothing_item);
                        
                    }
                }
            }
        }

        public static void UpdateClothing()
        {
            updatingClothes = true;

            //  Call Unequip for original outfit
            foreach (string clothingName in originalOutfit)
            {
                TequilaClothing.Unequip(clothingName);
            }

            //  Call Unequip for original headwear
            foreach (string clothingName in originalHeadwear)
            {
                TequilaClothingHeadwear.UnequipHeadWear(clothingName);
            }

            originalOutfit.Clear();
            originalHeadwear.Clear();

            // Call equip for current outfit
            foreach (string clothingName in currentOutfit)
            {
                TequilaClothing.EquipAndBlend(clothingName);
                originalOutfit.Add(clothingName);
            }

            // Call equip for current headwear
            foreach (string clothingName in currentHeadwear)
            {
                TequilaClothingHeadwear.EquipHeadWear(clothingName);
                originalHeadwear.Add(clothingName);
            }

            updatingClothes = false;
        }
    }
}
