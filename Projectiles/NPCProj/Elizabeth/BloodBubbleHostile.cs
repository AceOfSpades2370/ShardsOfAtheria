﻿using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodBubbleHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddAqua();
            Projectile.AddWood();
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;

            Projectile.timeLeft = 60 * 5;
            Projectile.aiStyle = 0;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;

            DrawOffsetX = -2;
        }

        Vector2 trackPosition = Vector2.Zero;
        Player player;

        public override void AI()
        {
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
                player = Projectile.FindClosestPlayer(-1);
                float radius = 150 * Main.rand.NextFloat(0.6f, 1f);
                float rotation = MathHelper.ToRadians(360);
                trackPosition = Vector2.One.RotatedByRandom(rotation) * radius;
                Projectile.ai[0]++;
            }

            Projectile.Track(player.Center + trackPosition, -1);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
            var vector = Vector2.Normalize(player.Center - Projectile.Center) * 16f;

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                vector, ModContent.ProjectileType<BloodNeedleHostile>(), Projectile.damage,
                Projectile.knockBack, Projectile.owner, 0, Projectile.Center.X, Projectile.Center.Y);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}