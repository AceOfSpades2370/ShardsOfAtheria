﻿using Microsoft.Xna.Framework;
using BattleNetworkElements.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class GolemBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddFire();
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EyeBeam);
            Projectile.friendly = true;
            Projectile.hostile = false;
            AIType = ProjectileID.EyeBeam;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }
    }
}