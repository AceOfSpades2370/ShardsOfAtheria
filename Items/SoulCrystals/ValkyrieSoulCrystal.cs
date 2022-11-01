﻿using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class ValkyrieSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Grants 8 defense, wing flight time boost and a dash that leaves behind an electric trail\n" +
                "Attacks create 4 closing feather blades in an x pattern\n" +
                "Getting hit by an enemy gives them Electric Shock";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}