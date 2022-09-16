﻿using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace ShardsOfAtheria.Items.DevItems.nightlight
{
    [AutoloadEquip(EquipType.Body)]
    public class CoatOfnight : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            ModLoader.TryGetMod("excels", out Mod excels);
            return excels == null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("nightlight's Coat");
            Tooltip.SetDefault("'Great for impersonating devs!'\n" +
                "'no capitalization on my name'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 36;
            Item.rare = ItemRarityID.Cyan;
            Item.vanity = true;
        }
    }
}