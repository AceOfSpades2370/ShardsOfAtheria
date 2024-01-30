﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class Plague : ModBuff
    {
        public const float SpeedReduction = 0.3f;
        public const int DefenseReduction = 20;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex]++;
            int severity = player.buffTime[buffIndex] / 600 + 1;
            if (severity > 1)
            {
                player.moveSpeed -= SpeedReduction;
                player.accRunSpeed -= SpeedReduction;
                player.statDefense -= DefenseReduction;
                player.blind = true;
            }
            if (severity > 2)
            {
                player.moveSpeed -= SpeedReduction;
                player.accRunSpeed -= SpeedReduction;
                player.blackout = true;
            }
            player.GetModPlayer<PlaguePlayer>().plagueSeverity = severity;
            if (player.buffTime[buffIndex] > 3600)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.buffTime[buffIndex] += 2;
            int severity = npc.buffTime[buffIndex] / 600 + 1;
            if (severity > 1)
            {
                npc.StatSpeed() -= SpeedReduction;
            }
            if (severity > 2)
            {
                npc.StatSpeed() -= SpeedReduction;
            }
            npc.GetGlobalNPC<PlagueNPC>().plagueSeverity = severity;
            if (npc.buffTime[buffIndex] > 3600)
            {
                npc.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }

    public class PlagueNPC : GlobalNPC
    {
        public int plagueSeverity;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            plagueSeverity = 0;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (plagueSeverity > 0)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                int dpt = 10 * plagueSeverity;
                npc.lifeRegen -= dpt;
                if (damage < dpt)
                {
                    damage = dpt;
                }
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (plagueSeverity > 1)
            {
                modifiers.Defense.Flat -= Plague.DefenseReduction;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (plagueSeverity > 0 && Main.rand.NextBool(8))
            {
                int type = ModContent.DustType<PlagueDust>();
                int dust = Dust.NewDust(npc.position, npc.width + 4, npc.height + 4, type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }

    public class PlaguePlayer : ModPlayer
    {
        public int plagueSeverity;

        public override void ResetEffects()
        {
            plagueSeverity = 0;
        }

        public override void UpdateBadLifeRegen()
        {
            if (plagueSeverity > 0)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 10 life lost per second, if the player is holding their left or right movement keys.
                Player.lifeRegen -= 20 * plagueSeverity;
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (damageSource.SourceNPCIndex == -1 &&
                damageSource.SourceItem == null &&
                damageSource.SourceProjectileType == 0)
            {
                if (Player.HasBuff(ModContent.BuffType<Plague>()))
                {
                    damageSource = PlayerDeathReason.ByCustomReason(Player.name + " succumbed to the plague.");
                }
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (plagueSeverity > 0 && Main.rand.NextBool(8))
            {
                int type = ModContent.DustType<PlagueDust>();
                int dust = Dust.NewDust(Player.position, Player.width + 4, Player.height + 4, type, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }
}