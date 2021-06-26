﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    public class ZenovaBlackAreusSword : ModProjectile {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenova Black Areus Sword");
        }

        public override void SetDefaults() {
            projectile.width = 21;
            projectile.height = 21;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.5f;
            projectile.extraUpdates = 1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            drawOffsetX = -27;
            drawOriginOffsetX = 14;
        }

        public override void AI()
        {
            projectile.velocity.Y = projectile.velocity.Y + 0.1f; // 0.1f for arrow gravity, 0.4f for knife gravity
            if (projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
            {
                projectile.velocity.Y = 16f;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }
    }
}
