﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodScytheHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
            Projectile.AddElementAqua();
            Projectile.AddElementWood();
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 70;
            Projectile.height = 140;
            Projectile.scale = 3f;

            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;

            DrawOriginOffsetY = 140;
        }

        int frameCounterMax = 60;

        public override void AI()
        {
            var npc = Main.npc[(int)Projectile.ai[0]];
            Projectile.Center = npc.Center +
                new Vector2(Projectile.width / 2 * Projectile.velocity.X, 0);

            Projectile.spriteDirection = -(int)Projectile.velocity.X;
            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = 142;
            }
            else
            {
                DrawOffsetX = -74;
            }
            if (++Projectile.frameCounter >= frameCounterMax)
            {
                frameCounterMax = 60;
                if (++Projectile.frame >= 3)
                {
                    Projectile.Kill();
                }
                else if (Projectile.frame == 1)
                {
                    frameCounterMax = 10;
                    SoundEngine.PlaySound(SoundID.Item71.WithPitchOffset(-1f), Projectile.Center);
                }
                Projectile.frameCounter = 0;
            }
        }

        public override bool? CanDamage()
        {
            return Projectile.frame == 1;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SoA.Eternity())
            {
                info.DamageSource = PlayerDeathReason.ByCustomReason(target.name + " had their soul reaped.");
                target.KillMe(info.DamageSource, info.Damage, info.HitDirection);
            }
            target.AddBuff<DeathBleed>(1200);
        }
    }
}