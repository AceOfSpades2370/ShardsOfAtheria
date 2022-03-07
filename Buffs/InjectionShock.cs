﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class InjectionShock : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Injection Shock");
            Description.SetDefault("You cannont use another injection, cannot regenerate life and mild blood loss");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffImmune[BuffID.Regeneration] = true;
            player.buffImmune[BuffID.Honey] = true;
            player.buffImmune[BuffID.Campfire] = true;
            player.buffImmune[BuffID.HeartLamp] = true;
            player.shinyStone = false;
        }
    }

    public class InjectionShockedPlayer : ModPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<InjectionShock>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes .5 life lost per second.
                Player.lifeRegen -= 1;
            }
        }
    }
}
