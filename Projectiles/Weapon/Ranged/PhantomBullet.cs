﻿using Microsoft.Xna.Framework;
using BattleNetworkElements.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class PhantomBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddFire();
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.damage = 300;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.arrow = true;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }
    }
}
