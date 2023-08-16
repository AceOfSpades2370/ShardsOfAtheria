﻿using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class CataracniaEye : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.aiStyle = ProjAIStyleID.Explosive;

            AIType = ProjectileID.BouncyGrenade;
        }

        int gravity;
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, TorchID.Yellow);
            ModContent.GetInstance<CameraFocus>().SetTarget("CataracniaEye", Projectile.Center, CameraPriority.Weak);
        }
    }
}
