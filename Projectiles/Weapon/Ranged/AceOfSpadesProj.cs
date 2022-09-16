﻿using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.DevItems.TheEternalAce;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class AceOfSpadesProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);

            if (Main.myPlayer == Projectile.owner && Main.mouseRight && !Main.LocalPlayer.mouseInterface && Main.LocalPlayer.HeldItem.type == ModContent.ItemType<AceOfSpades>())
            {
                Projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FieryExplosion>(), Projectile.damage, 9, Main.player[Projectile.owner].whoAmI);
            base.Kill(timeLeft);
        }
    }
}