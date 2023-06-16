﻿using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged
{
    public class AreusGrenadeProj : ModProjectile
    {
        int armTimer = 0;

        public override void SetStaticDefaults()
        {
            Projectile.AddElec();
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 22;

            Projectile.aiStyle = ProjAIStyleID.Explosive;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 7;

            AIType = ProjectileID.Grenade;
        }

        public override void AI()
        {
            if (++armTimer == 5)
            {
                SoundEngine.PlaySound(SoundID.Unlock.WithPitchOffset(-1f).WithVolumeScale(0.6f));
                Projectile.frame = 1;
                Projectile.friendly = true;
                for (int i = 0; i < 5; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<AreusDust>());
                    dust.velocity *= 2f;
                }
            }
            if (armTimer == 240 && Projectile.alpha == 0)
            {
                Projectile.Explode();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.alpha == 0)
            {
                Projectile.Explode();
            }
        }
    }
}
