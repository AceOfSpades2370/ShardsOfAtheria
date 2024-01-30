﻿using ShardsOfAtheria.Utilities;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather
{
    public class Snow : HarpyFeathers
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElementAqua();
            Projectile.AddRedemptionElement(4);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            debuffType = BuffID.Frostburn;
            dustType = DustID.Snow;
        }
    }
}