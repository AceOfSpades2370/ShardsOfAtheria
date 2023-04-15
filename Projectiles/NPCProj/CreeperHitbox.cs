﻿using MMZeroElements.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj
{
    public class CreeperHitbox : ModProjectile
    {
        public override string Texture => SoA.Blank_String;

        public override void SetStaticDefaults()
        {
            Projectile.AddIce();
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;

            Projectile.timeLeft = 2;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;

            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }
    }
}