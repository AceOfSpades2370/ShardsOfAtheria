﻿using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class FeatherBladeFriendly : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<FeatherBlade>());
			Projectile.friendly = true;
			Projectile.hostile = false;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 0;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (++Projectile.ai[0] >= 60 && !Projectile.tileCollide)
            {
                Projectile.tileCollide = true;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }
    }
}