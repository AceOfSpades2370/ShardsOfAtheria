﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class Overdrive : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Overdrive: ON");
            Description.SetDefault("'Your systems are being pushed beyond their limits'\n" +
                "Damage and movement speed increased by 50%\n" +
                "Defense lowered by 30\n" +
                "You cannot regen life");
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            SMPlayer p = player.GetModPlayer<SMPlayer>();

            // We use blockyAccessoryPrevious here instead of blockyAccessory because UpdateBuffs happens before UpdateEquips but after ResetEffects.
            if (p.overdriveTimeCurrent >= 0)
            {
                player.allDamage += .5f;
                player.moveSpeed += .5f;
                player.statDefense -= 30;
                Lighting.AddLight(player.position, 0.5f, 0.5f, 0.5f);
                player.buffTime[buffIndex] = 18000;
                player.buffImmune[BuffID.Regeneration] = true;
                player.buffImmune[BuffID.Honey] = true;
                player.buffImmune[BuffID.Campfire] = true;
                player.buffImmune[BuffID.HeartLamp] = true;
                player.shinyStone = false;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
