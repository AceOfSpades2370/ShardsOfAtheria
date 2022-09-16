﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Projectiles.Minions;

namespace ShardsOfAtheria.Buffs
{
    public class JuvenileHarpy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Juvenile Harpy");
            Description.SetDefault("The harpy will fight along side you");

            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<YoungHarpy>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}