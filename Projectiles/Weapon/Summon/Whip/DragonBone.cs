﻿using BattleNetworkElements.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Summon.Whip
{
    public class DragonBone : ModProjectile
    {
        int timer = 0;

        public override void SetStaticDefaults()
        {
            Projectile.AddWood();
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 22;

            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;

            AIType = ProjectileID.Bone;
        }

        public override void AI()
        {
            if (timer == 0)
            {
                if (Projectile.velocity.Y > 0)
                {
                    Projectile.velocity.Y *= -1;
                }
            }
            if (++timer >= 15)
            {
                Projectile.friendly = true;
            }
            base.AI();
        }
    }
}
