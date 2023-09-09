using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public class RiggedCoin : ModItem
    {
        int rigMode = 2;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.rare = ItemRarityID.Blue;
            Item.value = 50000;
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void RightClick(Player player)
        {
            rigMode--;
            if (rigMode < 0)
            {
                rigMode = 2;
            }
        }

        public override void UpdateInventory(Player player)
        {
            var shards = player.Shards();
            shards.riggedCoin = rigMode;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string keyBase = "Mods.ShardsOfAtheria.Items.RiggedCoin.";
            string mode = "";
            if (rigMode == 0)
            {
                mode = Language.GetTextValue(keyBase + "Off");
            }
            if (rigMode == 1)
            {
                mode = Language.GetTextValue(keyBase + "Tails");
            }
            if (rigMode == 2)
            {
                mode = Language.GetTextValue(keyBase + "Heads");
            }
            tooltips[0].Text += mode;
        }
    }
}