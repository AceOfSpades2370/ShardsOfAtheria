﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class Ragnarok_Shield : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 56;

            Projectile.aiStyle = 75;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
            Projectile.light = .4f;
        }

        public override bool? CanCutTiles() => false;

        public override void AI()
        {
            var direction = Main.MouseWorld - Projectile.Center;
            Player player = Main.player[Projectile.owner];

            if (Main.myPlayer == Projectile.owner)
            {
                if (!Main.mouseRight || player.dead || Main.mouseLeft)
                    Projectile.Kill();
                Projectile.rotation = direction.ToRotation();

                int newDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
                player.ChangeDir(newDirection);
                Projectile.direction = newDirection;
                Parry();
            }
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 30f;
            if (!player.dead && !Main.mouseLeft && Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), player.Center, velocity.RotatedByRandom(MathHelper.ToRadians(5)), ModContent.ProjectileType<RagnarokProj>(), player.HeldItem.damage, player.HeldItem.knockBack, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item1);
            }
        }

        public void Parry()
        {
            Rectangle hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
            Player player = Main.player[Projectile.owner];

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile reflProjectile = Main.projectile[i];
                if (hitbox.Intersects(reflProjectile.getRect()))
                {
                    if (reflProjectile.active && reflProjectile.velocity != Vector2.Zero && reflProjectile.hostile)
                    {
                        float damage = reflProjectile.damage;
                        int penetrate = reflProjectile.penetrate;
                        Vector2 velocity = -reflProjectile.velocity;
                        int extraUpdates = reflProjectile.extraUpdates;
                        float knockback = reflProjectile.knockBack;

                        Vector2 dir = Main.MouseWorld - reflProjectile.position;
                        dir.Normalize();
                        dir *= (Math.Abs(reflProjectile.velocity.X) + Math.Abs(reflProjectile.velocity.Y));
                        velocity = dir;
                        if (reflProjectile.hostile)
                        {
                            SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack.WithVolume(1), Projectile.Center);
                            SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack.WithVolume(1), Projectile.Center);
                            reflProjectile.hostile = false;
                            reflProjectile.friendly = true;
                            reflProjectile.damage = (int)damage;
                            reflProjectile.penetrate = penetrate;
                            reflProjectile.velocity = velocity;
                            reflProjectile.extraUpdates = extraUpdates;
                            reflProjectile.knockBack = knockback;

                            player.immune = true;
                            player.immuneTime = 60;
                        }
                    }
                }
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC parryNPC = Main.npc[i];
                if (hitbox.Intersects(parryNPC.getRect()) && !player.HasBuff(ModContent.BuffType<ParryCooldown>()) && !parryNPC.friendly && parryNPC.damage > 0)
                {
                    player.immune = true;
                    player.immuneTime = 60;
                    player.AddBuff(ModContent.BuffType<ParryCooldown>(), 300);
                    player.AddBuff(BuffID.ParryDamageBuff, 300);
                    parryNPC.AddBuff(BuffID.OnFire, 600);
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;

            var drawPosition = Main.MouseWorld;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            if (Projectile.alpha == 0 && Main.myPlayer == Projectile.owner)
            {
                int direction = -1;

                if (Main.MouseWorld.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }
            return true;
        }
    }
}