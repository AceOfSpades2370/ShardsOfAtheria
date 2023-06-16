﻿using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.BloodDagger
{
    public class HolyBloodOffense : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.GolfBallDyedViolet}";

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
            Projectile.AddAqua();
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 360;
            Projectile.extraUpdates = 3;
        }

        NPC Target;
        int timer = 12;

        public override void AI()
        {
            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Blood);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;

            if (timer > 0)
            {
                timer--;
                if (timer == 1)
                {
                    Projectile.friendly = true;
                }
            }
            else
            {
                if (Target == null)
                {
                    Target = Projectile.FindClosestNPC(-1);
                    return;
                }
                Projectile.Track(Target, -1);

                if (!Target.active || Target.life <= 0)
                {
                    Projectile.Kill();
                }
            }
        }
    }
}