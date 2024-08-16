﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class AmbassadorBeam : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 6;
            if (++Projectile.ai[0] >= 5)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric);
                d.velocity *= 0;
                d.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, Type + 1, Projectile.damage, 0f);
        }
    }
}