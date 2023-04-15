﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Bases;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.AreusSwordProjs
{
    public class AreusSwordProj : SwordProjectileBase
    {
        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.AreusProj.Add(Type);
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 100;
            rotationOffset = -MathHelper.PiOver4 * 3f;
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo > 0)
            {
                swingDirection *= -1;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 position = target.Center + Vector2.One.RotatedByRandom(360) * 180;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, Vector2.Normalize(target.Center - position) * 20, ModContent.ProjectileType<ElectricBlade>(), 50, 6f, Projectile.owner);
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            base.AI();
            if (Main.player[Projectile.owner].itemAnimation <= 1)
            {
                Main.player[Projectile.owner].ShardsOfAtheria().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            }
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-1f), Projectile.Center);
            }
        }

        public override Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 1.5f) - (MathHelper.PiOver2 * 1.5f)) * -swingDirection * 1.1f);
        }

        public override float SwingProgress(float progress)
        {
            return GenericSwing2(progress);
        }

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            if (progress > 0.8f)
            {
                float p = 1f - (1f - progress) / 0.2f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            if (progress < 0.35f)
            {
                float p = 1f - progress / 0.35f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            return 0f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return SingleEdgeSwordDraw<AreusSword>(lightColor);
        }
    }
}