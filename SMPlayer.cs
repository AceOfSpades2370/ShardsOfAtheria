﻿using Microsoft.Xna.Framework;
using SagesMania.Buffs;
using SagesMania.Projectiles;
using SagesMania.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania
{
    public class SMPlayer : ModPlayer
    {
        public bool areusBatteryElectrify;
        public bool BBBottle;
        public bool PhantomBulletBottle;
        public bool Co2Cartridge;
        public bool lesserSapphireCore;
        public bool sapphireCore;
        public bool superSapphireCore;
        public bool greaterSapphireCore;
        public bool greaterRubyCore;
        public bool superRubyCore;
        public bool OrangeMask;
        public bool Overdrive;
        public bool livingMetal;
        public bool Infected;
        public bool omnicientTome;
        public bool baseConservation;
        public bool sapphireMinion;
        public bool superEmeraldCore;
        public bool areusKey;
        public bool unshackledTome;
        public bool megaGemCore;

        public int TomeKnowledge;

        public override void ResetEffects()
        {
            areusBatteryElectrify = false;
            BBBottle = false;
            PhantomBulletBottle = false;
            Co2Cartridge = false;
            lesserSapphireCore = false;
            sapphireCore = false;
            superSapphireCore = false;
            greaterSapphireCore = false;
            greaterRubyCore = false;
            superRubyCore = false;
            OrangeMask = false;
            livingMetal = false;
            Overdrive = false;
            Infected = false;
            omnicientTome = false;
            baseConservation = false;
            sapphireMinion = false;
            superEmeraldCore = false;
            areusKey = false;
            unshackledTome = false;
            megaGemCore = false;
        }

        public override void PostUpdate()
        {
            if (OrangeMask)
            {
                player.statDefense += 7;
                player.rangedDamage += .1f;
                player.rangedCrit += 4;
            }
            if (Overdrive)
            {
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawOutlines = true;
            }
            if (omnicientTome)
            {
                if (TomeKnowledge == 0)
                {
                    player.AddBuff(ModContent.BuffType<BaseCombat>(), 1);
                }
                else if (TomeKnowledge == 1)
                {
                    player.AddBuff(ModContent.BuffType<BaseConservation>(), 1);
                }
                else if (TomeKnowledge == 2)
                {
                    player.AddBuff(ModContent.BuffType<BaseExploration>(), 1);
                    player.AddBuff(BuffID.Mining, 1);
                    player.AddBuff(BuffID.Builder, 1);
                    player.AddBuff(BuffID.Shine, 1);
                    player.AddBuff(BuffID.Hunter, 1);
                }
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && greaterSapphireCore)
            {
                Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 80, 5, player.whoAmI);
            }
            if (unshackledTome)
            {
                if (!player.GetModPlayer<SMPlayer>().areusKey)
                {
                    player.AddBuff(BuffID.ChaosState, 10 * 60);
                    player.AddBuff(BuffID.Confused, 10 * 60);
                    player.AddBuff(BuffID.ManaSickness, 10 * 60);
                    player.AddBuff(BuffID.Poisoned, 10 * 60);
                }
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && superSapphireCore)
            {
                Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 157, 5, player.whoAmI);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && megaGemCore)
            {
                Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 267, 5, player.whoAmI);
            }
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (BBBottle)
            {
                return Main.rand.NextFloat() >= .05f;
            }
            if (PhantomBulletBottle)
            {
                return Main.rand.NextFloat() >= .48f;
            }

            if (baseConservation)
            {
                return Main.rand.NextFloat() >= .15f;
            }
            return true;
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (PhantomBulletBottle)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<PhantomBullet>(), damage, knockBack, player.whoAmI);
            }
            if (BBBottle)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BBProjectile>(), damage, knockBack, player.whoAmI);
            }
            if (Co2Cartridge)
            {
                if (type == ModContent.ProjectileType<BBProjectile>())
                {
                    type = ProjectileID.BulletHighVelocity;
                }
                return true;
            }
            return true;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (SagesMania.OverdriveKey.JustPressed)
            {
                if (livingMetal && !player.HasBuff(ModContent.BuffType<Overdrive>()))
                {
                    CombatText.NewText(player.Hitbox, Color.Green, "Overdrive: ON", true);
                    Main.PlaySound(SoundID.Item4, player.position);
                    player.AddBuff(ModContent.BuffType<Overdrive>(), 600 * 60);
                }
                else
                {
                    player.ClearBuff(ModContent.BuffType<Overdrive>());
                    CombatText.NewText(player.Hitbox, Color.Red, "Overdrive: OFF");
                    Main.PlaySound(SoundID.NPCDeath56, player.position);

                }
            }
            if (SagesMania.TomeKey.JustPressed)
            {
                if (omnicientTome)
                {
                    if (TomeKnowledge == 2)
                    {
                        TomeKnowledge = 0;
                    }
                    else TomeKnowledge += 1;
                    Main.PlaySound(SoundID.Item1, player.position);
                }
            }
            if (SagesMania.EmeraldTeleportKey.JustPressed)
            {
                if (superEmeraldCore)
                {
                    Vector2 vector21 = default(Vector2);
                    vector21.X = (float)Main.mouseX + Main.screenPosition.X;
                    if (player.gravDir == 1f)
                    {
                        vector21.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                    }
                    vector21.X -= player.width / 2;
                    if (vector21.X > 50f && vector21.X < (float)(Main.maxTilesX * 16 - 50) && vector21.Y > 50f && vector21.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].wall != 87 || !((double)num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, player.width, player.height))
                        {
                            player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, vector21.X, vector21.Y, 1);
                            if (player.chaosState)
                            {
                                player.statLife -= player.statLifeMax2 / 7;
                                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                                if (Main.rand.Next(2) == 0)
                                {
                                    damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
                                }
                                if (player.statLife <= 0)
                                {
                                    player.KillMe(damageSource, 1.0, 0);
                                }
                                player.lifeRegenCount = 0;
                                player.lifeRegenTime = 0;
                            }
                            player.AddBuff(BuffID.ChaosState, 360);
                        }
                    }
                }
                if (megaGemCore)
                {
                    Vector2 vector21 = default(Vector2);
                    vector21.X = (float)Main.mouseX + Main.screenPosition.X;
                    if (player.gravDir == 1f)
                    {
                        vector21.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                    }
                    vector21.X -= player.width / 2;
                    if (vector21.X > 50f && vector21.X < (float)(Main.maxTilesX * 16 - 50) && vector21.Y > 50f && vector21.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].wall != 87 || !((double)num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, player.width, player.height))
                        {
                            player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, vector21.X, vector21.Y, 1);
                            if (player.chaosState)
                            {
                                player.statLife -= player.statLifeMax2 / 7;
                                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                                if (Main.rand.Next(2) == 0)
                                {
                                    damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
                                }
                                if (player.statLife <= 0)
                                {
                                    player.KillMe(damageSource, 1.0, 0);
                                }
                                player.lifeRegenCount = 0;
                                player.lifeRegenTime = 0;
                            }
                            player.AddBuff(BuffID.ChaosState, 360);
                        }
                    }
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (areusBatteryElectrify)
            {
                target.AddBuff(BuffID.Electrified, 10 * 60);
            }
            if (greaterRubyCore)
            {
                target.AddBuff(BuffID.OnFire, 10 * 60);
            }
            if (greaterRubyCore)
            {
                target.AddBuff(BuffID.CursedInferno, 10 * 60);
                target.AddBuff(BuffID.Ichor, 10 * 60);
            }
            if (megaGemCore)
            {
                target.AddBuff(BuffID.Daybreak, 10 * 60);
                target.AddBuff(BuffID.BetsysCurse, 10 * 60);
                player.AddBuff(BuffID.Ironskin, 10 * 60);
                player.AddBuff(BuffID.Endurance, 10 * 60);
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (areusBatteryElectrify)
            {
                target.AddBuff(BuffID.Electrified, 10 * 60);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (lesserSapphireCore && Main.rand.NextFloat() < 0.05f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            if (sapphireCore && Main.rand.NextFloat() < 0.1f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            if (superSapphireCore && Main.rand.NextFloat() < 0.15f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            if (megaGemCore && Main.rand.NextFloat() < 0.2f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            else return true;
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                player.ClearBuff(ModContent.BuffType<Overdrive>());
                CombatText.NewText(player.Hitbox, Color.Red, "Overdrive: BREAK", true);
                Main.PlaySound(SoundID.NPCDeath44, player.position);
            }
            if (megaGemCore)
            {
                player.AddBuff(BuffID.Rage, 10 * 60);
                player.AddBuff(BuffID.Wrath, 10 * 60);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (Overdrive)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= 12;
            }
            if (Infected)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= 10;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Overdrive)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Blood, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
            }
        }
    }
}