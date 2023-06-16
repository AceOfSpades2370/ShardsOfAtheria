﻿using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.DataDisks;
using ShardsOfAtheria.Items.GrabBags;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.PetItems;
using ShardsOfAtheria.Items.Placeable.Furniture.Trophies;
using ShardsOfAtheria.Items.Placeable.Furniture.Trophies.Master;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon.Minion;
using ShardsOfAtheria.ModCondition.ItemDrop;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie
{
    [AutoloadBossHead]
    public class NovaStellar : ModNPC
    {
        public int attackType = 0;
        public int attackTimer = 0;
        public int attackCooldown = 40;
        public int attackTypeNext = -1;

        int frameX = 0;
        int frameY = 0;
        int maxFrameY = 6;

        public override string BossHeadTexture => "ShardsOfAtheria/Items/BossSummons/ValkyrieCrest";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];

            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    ModContent.BuffType<ElectricShock>(),

                    BuffID.Confused // Most NPCs have this
				}
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
            NPC.AddElec();
        }

        public override void SetDefaults()
        {
            NPC.width = 24;
            NPC.height = 38;
            NPC.damage = 30;
            NPC.defense = 8;
            NPC.lifeMax = 5200;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPC.boss = true;
            NPC.noGravity = true;
            Music = MusicID.Boss4;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.npcSlots = 15f;
            NPC.ElementMultipliers() = new[] { 2.0f, 0.8f, 0.8f, 1.5f };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.ShardsOfAtheria.NPCBestiary.NovaStellar"))
            });
        }

        public override bool CheckDead()
        {
            KillProjectiles();
            attackType = 0;
            attackTimer = 0;
            attackCooldown = 0;
            frameX = 1;
            if (NPC.ai[3] == 0f)
            {
                NPC.ai[3] = 1f;
                NPC.damage = 0;
                NPC.life = 1;
                NPC.dontTakeDamage = true;
                NPC.netUpdate = true;
                return false;
            }
            return true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            // Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            // This requires you to set BossBag in SetDefaults accordingly
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<NovaBossBag>()));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            LeadingConditionRule slayerMode = new LeadingConditionRule(new IsSlayerMode());
            LeadingConditionRule master = new(new Conditions.IsMasterMode());
            LeadingConditionRule notDefeated = new(new DownedValkyrie());

            int[] drops = { ModContent.ItemType<ValkyrieBlade>(), ModContent.ItemType<DownBow>(), ModContent.ItemType<PlumeCodex>(), ModContent.ItemType<NestlingStaff>() };

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, drops));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HardlightKnife>(), 5, 150, 180));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieCrown>(), 5));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HardlightPrism>(), 1, 15, 28));
            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.GoldBar, 1, 8, 14));

            npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<NovaRelic>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NovaTrophy>(), 10));
            npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<SmallHardlightCrest>(), 4));

            slayerMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieSoulCrystal>()));

            if (ModLoader.TryGetMod("MagicStorage", out Mod magicStorage) && !ShardsDownedSystem.downedValkyrie)
            {
                notExpertRule.OnSuccess(ItemDropRule.Common(magicStorage.Find<ModItem>("ShadowDiamond").Type));
            }

            notDefeated.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<HardlightDataDisk>()));
            master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieStormLance>()));
            npcLoot.Add(master);

            // Finally add the leading rule
            npcLoot.Add(notExpertRule);
            npcLoot.Add(slayerMode);
        }

        public override void OnKill()
        {
            Player lastPlayerToHitThisNPC = NPC.AnyInteractions() ? Main.player[NPC.lastInteraction] : null;
            NPC.SetEventFlagCleared(ref ShardsDownedSystem.downedValkyrie, -1);
            if (Main.LocalPlayer.GetModPlayer<SlayerPlayer>().slayerMode)
            {
                ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie = true;
            }

            if (lastPlayerToHitThisNPC != null && lastPlayerToHitThisNPC.GetModPlayer<SlayerPlayer>().slayerMode)
            {
                NPC.SlayNPC(lastPlayerToHitThisNPC);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode)
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
            }
        }

        public override bool PreAI()
        {
            if (ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Nova Stellar was slain..."), Color.White);
                NPC.active = false;
            }
            return base.PreAI();
        }

        private bool phase2 = false;
        private bool desperation = false;
        bool AlreadyDefeated = ShardsDownedSystem.downedValkyrie;

        public override void AI()
        {
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];
            bool isSlayer = player.GetModPlayer<SlayerPlayer>().slayerMode;

            // death drama
            if (NPC.ai[3] > 0f)
            {
                NPC.ai[3] += 1f; // increase our death timer.
                NPC.velocity = isSlayer ? new Vector2(0, 1) * 8f : Vector2.Zero;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];

                    if (proj.type == ModContent.ProjectileType<StormSword>() || proj.type == ModContent.ProjectileType<StormLance>() || proj.type == ModContent.ProjectileType<FeatherBlade>()
                        || proj.type == ModContent.ProjectileType<StormCloud>())
                    {
                        proj.Kill();
                    }
                }
                if (NPC.ai[3] == 2)
                {
                    UseDialogue(isSlayer ? Death : AlreadyDefeated ? ReDefeat : Defeat);
                }
                if (NPC.ai[3] >= 120f)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 0);
                    NPC.checkDead(); // This will trigger ModNPC.CheckDead the second time, causing the real death.
                }
                return;
            }

            if (player.dead)
            {
                attackCooldown = 0;
                attackTimer = 0;
                attackType = 0;
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }

            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !ModContent.GetInstance<ShardsDownedSystem>().slainValkyrie)
            {
                if (Main.rand.NextFloat() <= .5f)
                    NPC.position = player.position - new Vector2(500, 250);
                else NPC.position = player.position - new Vector2(-500, 250);
                UseDialogue(isSlayer ? SlayerSummon : AlreadyDefeated ? ReSummon : Summon);
                NPC.localAI[0] = 1f;
            }
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;

            bool transitioning = false;
            if (NPC.life <= NPC.lifeMax * 0.5 && !phase2)
            {
                transitioning = true;
                attackCooldown = 120;
                attackTimer = 0;
                attackType = -1;
                DoPhase2Transition();
            }

            if (NPC.life <= NPC.lifeMax * 0.25 && !desperation && player.GetModPlayer<SlayerPlayer>().slayerMode)
            {
                UseDialogue(Desperation);
                desperation = true;
            }

            if (!transitioning)
            {
                if (attackCooldown <= 0)
                {
                    ChoseAttacks();
                }

                if (attackTimer > 0)
                {
                    CycleAttack(player);
                    attackTimer--;
                }
                else
                {
                    if (phase2)
                    {
                        frameX = 1;
                    }
                    else
                    {
                        frameX = 0;
                    }
                    // decrease wait timer when attack done
                    attackCooldown--;
                }
            }
            NPC.netUpdate = true;
        }

        int damage = 0;
        const int ElectricDash = 0;
        const int FeatherBarrage = 1;
        const int LanceDash = 2;
        const int StormCloud = 3;
        const int SwordsDance = 4;
        const int BowShoot = 5;
        const int SwordSwing = 6;
        const int KnifeThrow = 7;
        List<int> blacklistedAttacks = new();
        void ChoseAttacks()
        {
            WeightedRandom<int> random = new WeightedRandom<int>();
            AddNonBlacklistedAttack(ref random, ElectricDash);
            AddNonBlacklistedAttack(ref random, FeatherBarrage);
            if (NPC.life <= NPC.lifeMax / 4 * 3)
            {
                if (NPC.life <= NPC.lifeMax / 2)
                {
                    AddNonBlacklistedAttack(ref random, BowShoot);
                    AddNonBlacklistedAttack(ref random, SwordSwing);
                }
                AddNonBlacklistedAttack(ref random, StormCloud);
                AddNonBlacklistedAttack(ref random, SwordsDance);
            }
            if (Main.expertMode)
            {
                AddNonBlacklistedAttack(ref random, KnifeThrow);
            }
            if (Main.masterMode)
            {
                AddNonBlacklistedAttack(ref random, LanceDash);
            }
            attackType = random;
            if (attackTypeNext > -1)
            {
                attackType = attackTypeNext;
            }
            blacklistedAttacks.Clear();
            SetAttackStats();
        }
        void AddNonBlacklistedAttack(ref WeightedRandom<int> randomAttack, int attack, double weight = 1)
        {
            if (!blacklistedAttacks.Contains(attack))
            {
                randomAttack.Add(attack, weight);
            }
        }
        void SetAttackStats()
        {
            attackTypeNext = -1;
            switch (attackType)
            {
                default:
                    break;
                case ElectricDash:
                    attackTimer = 60;
                    attackCooldown = 60;
                    damage = 18;
                    attackTypeNext = FeatherBarrage;
                    if (phase2)
                    {
                        attackTypeNext = BowShoot;
                    }
                    break;
                case FeatherBarrage:
                    attackTimer = 260;
                    attackCooldown = 60;
                    damage = 18;
                    if (phase2)
                    {
                        attackTypeNext = BowShoot;
                    }
                    break;
                case LanceDash:
                    attackTimer = 120;
                    attackCooldown = 60;
                    damage = 20;
                    attackTypeNext = KnifeThrow;
                    break;
                case StormCloud:
                    attackTimer = 340;
                    attackCooldown = 60;
                    damage = 18;
                    attackTypeNext = FeatherBarrage;
                    break;
                case SwordsDance:
                    attackTimer = 9 * 60;
                    attackCooldown = 60;
                    damage = 16;
                    if (NPC.life <= NPC.lifeMax / 2)
                    {
                        attackTypeNext = SwordSwing;
                    }
                    break;
                case BowShoot:
                    attackTimer = 56;
                    attackCooldown = 20;
                    damage = 11;
                    attackTypeNext = SwordSwing;
                    break;
                case SwordSwing:
                    attackTimer = 360;
                    attackCooldown = 20;
                    blacklistedAttacks.Add(SwordsDance);
                    blacklistedAttacks.Add(BowShoot);
                    break;
                case KnifeThrow:
                    attackTimer = 60;
                    attackCooldown = 20;
                    damage = 10;
                    blacklistedAttacks.Add(KnifeThrow);
                    break;
            }
            blacklistedAttacks.Add(attackType);
            if (Main.masterMode || Main.getGoodWorld)
            {
                attackCooldown = attackCooldown * 3 / 4;
            }
        }
        void CycleAttack(Player player)
        {
            NPC.netUpdate = true;
            Vector2 center = NPC.Center;
            Vector2 targetPosition = player.Center;
            Vector2 toTarget = targetPosition - center;
            switch (attackType)
            {
                default:
                    break;
                case ElectricDash:
                    DoElectricDash(center, toTarget);
                    break;
                case FeatherBarrage:
                    DoFeatherBarrage(center, toTarget, targetPosition);
                    break;
                case LanceDash:
                    DoLanceDash(player, targetPosition, center);
                    break;
                case StormCloud:
                    DoStormCloud(targetPosition);
                    break;
                case SwordsDance:
                    DoSwordsDance(targetPosition);
                    break;
                case BowShoot:
                    DoBowShoot(center, toTarget);
                    break;
                case SwordSwing:
                    DoBladeSwing(player);
                    break;
                case KnifeThrow:
                    DoKnifeThrow(player);
                    break;
            }
        }

        void DoElectricDash(Vector2 center, Vector2 toTarget)
        {
            if (attackTimer == 60)
            {
                NPC.velocity = Vector2.Normalize(toTarget) * 10;
            }
            if (attackTimer % 2 == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Zero, ModContent.ProjectileType<ElectricTrail>(),
                    damage, 0f, Main.myPlayer);
            }
            if (NPC.life < NPC.lifeMax / 2 && attackTimer == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 velocity = Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 1f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), center, velocity, ModContent.ProjectileType<LightningBolt>(),
                        damage, 0f, Main.myPlayer);
                }
            }
        }

        void DoFeatherBarrage(Vector2 center, Vector2 toTarget, Vector2 targetPosition)
        {
            if (attackTimer > 240)
            {
                return;
            }
            Item novaBook = ModContent.GetInstance<PlumeCodex>().Item;
            if (attackTimer == 240)
            {
                SoundEngine.PlaySound(novaBook.UseSound);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 7f,
                    ModContent.ProjectileType<FeatherBlade>(), damage, 0f, Main.myPlayer);
            }
            // + pattern
            if (attackTimer < 240 && attackTimer > 180)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 dustPos = targetPosition + new Vector2(1.425f, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                    Dust dust = Dust.NewDustPerfect(dustPos, DustID.Electric);
                    dust.noGravity = true;
                }
            }
            // The actual attack
            if (attackTimer == 180)
            {
                SoundEngine.PlaySound(novaBook.UseSound);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projPos = targetPosition + new Vector2(1.425f, 0).RotatedBy(MathHelper.ToRadians(90 * i)) * 150;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos, Vector2.Normalize(targetPosition - projPos) * 7f,
                        ModContent.ProjectileType<FeatherBlade>(), damage, 0f, Main.myPlayer, 1f);
                }
            }
            if (attackTimer == 120)
            {
                SoundEngine.PlaySound(novaBook.UseSound);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, Vector2.Normalize(toTarget) * 7f,
                    ModContent.ProjectileType<FeatherBlade>(), damage, 0f, Main.myPlayer);
            }
            // X pattern
            if (attackTimer < 120 && attackTimer > 60)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vector2 dustPos = targetPosition + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150f;
                    Dust dust = Dust.NewDustPerfect(dustPos, DustID.Electric);
                    dust.noGravity = true;
                }
            }
            // the actual attack
            if (attackTimer == 60)
            {
                SoundEngine.PlaySound(novaBook.UseSound);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 projPos = targetPosition + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 150f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), projPos, Vector2.Normalize(targetPosition - projPos) * 7f,
                        ModContent.ProjectileType<FeatherBlade>(), damage, 0f, Main.myPlayer, 1f);
                }
            }
        }

        void DoLanceDash(Player player, Vector2 targetPosition, Vector2 center)
        {
            if (attackTimer > 30)
            {
                float speed = 8f;
                int direction = (NPC.Center.X > targetPosition.X ? 1 : -1);
                Vector2 toPosition = targetPosition + new Vector2(250 * direction, 0);
                NPC.Track(toPosition, speed, speed);
            }
            if (attackTimer == 30)
            {
                Vector2 playerY = new(0, player.velocity.Y);
                NPC.velocity = Vector2.Normalize(targetPosition + playerY * 12 - center) * 16;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), center, NPC.velocity * 0.75f,
                    ModContent.ProjectileType<StormLance>(), damage, 0, Main.myPlayer);
            }
        }

        void DoStormCloud(Vector2 targetPosition)
        {
            float speed = 8f;
            Vector2 toPosition = targetPosition + new Vector2(500 * (NPC.Center.X > targetPosition.X ? 1 : -1), -200);
            NPC.Track(toPosition, speed, speed);
            if (attackTimer == 320)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), targetPosition + new Vector2(0, -400), Vector2.Zero,
                    ModContent.ProjectileType<StormCloud>(), damage, 0, Main.myPlayer);
            }
            if (Main.getGoodWorld)
            {
                if (attackTimer % 6 == 0)
                {
                    DoBowShoot(NPC.Center, targetPosition - NPC.Center);
                }
            }
        }

        void DoSwordsDance(Vector2 targetPosition)
        {
            float speed = 8f;
            Vector2 toPosition = targetPosition + new Vector2(500 * (NPC.Center.X > targetPosition.X ? 1 : -1), -200);
            NPC.Track(toPosition, speed, speed);
            if (attackTimer == 9 * 60)
            {
                for (int i = 0; i < 7; i++)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), targetPosition, Vector2.Zero,
                        ModContent.ProjectileType<StormSword>(), damage, 0, Main.myPlayer, i);
                }
            }
        }

        void DoBowShoot(Vector2 center, Vector2 toTarget)
        {
            Player player = Main.player[NPC.target];
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;
            int rate = 12;
            bool shouldFire = attackTimer <= 36;
            if (attackType == StormCloud)
            {
                rate = 12;
                shouldFire = Main.rand.NextBool(10);
            }
            if (attackTimer % rate == 0 && shouldFire)
            {
                SoundEngine.PlaySound(SoundID.Item5);
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(5);
                toTarget.Normalize();
                center += Vector2.Normalize(toTarget) * 10f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = toTarget.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), center, perturbedSpeed * 16f,
                        ModContent.ProjectileType<FeatherBlade>(), damage, 3, Main.myPlayer);
                    proj.DamageType = DamageClass.Ranged;
                }
            }
        }

        void DoBladeSwing(Player player)
        {
            NPC.Track(player.Center, 6, 6);
            if (frameY == 0)
            {
                var swordItem = ModContent.GetInstance<ValkyrieBlade>().Item;
                SoundEngine.PlaySound(swordItem.UseSound);
            }
        }

        void DoKnifeThrow(Player player)
        {
            var vectorToPlayer = player.Center - NPC.Center;
            vectorToPlayer.Normalize();
            vectorToPlayer = vectorToPlayer.RotatedByRandom(MathHelper.ToRadians(10));
            if (attackTimer % 10 == 0)
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, vectorToPlayer * 16f,
                    ModContent.ProjectileType<HardlightKnifeHostile>(), damage, 0, Main.myPlayer);
            }
        }

        int transitionTime = 120;
        void DoPhase2Transition()
        {
            if (transitionTime == 120)
            {
                Player player = Main.player[NPC.target];
                bool isSlayer = player.GetModPlayer<SlayerPlayer>().slayerMode;

                UseDialogue(isSlayer ? SlayerMidFight : AlreadyDefeated ? ReMidFight : MidFight);
                NPC.dontTakeDamage = true;
                KillProjectiles();
                SoundEngine.PlaySound(SoundID.Thunder);
                for (int i = 0; i < 4; i++)
                {
                    Vector2 velocity = Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 1f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, ModContent.ProjectileType<LightningBolt>(), 18, 0f, Main.myPlayer);
                }
            }
            NPC.velocity *= 0;
            attackCooldown = transitionTime;
            if (transitionTime > 100)
            {
                frameX = 5;
            }
            else
            {
                frameX = 6;
            }
            if (--transitionTime == 0)
            {
                phase2 = true;
                NPC.dontTakeDamage = false;
            }
        }

        const int Summon = 1;
        const int MidFight = 2;
        const int Defeat = 3;
        const int ReSummon = 4;
        const int ReMidFight = 5;
        const int ReDefeat = 6;
        const int SlayerSummon = 7;
        const int SlayerMidFight = 8;
        const int Desperation = 9;
        const int Death = 10;

        void UseDialogue(int index)
        {
            string key;

            switch (index)
            {
                default: // Testing
                    key = "Placeholder Text";
                    break;

                case Summon:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.InitialSummon";
                    break;
                case MidFight:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.MidFight";
                    break;
                case Defeat:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.Defeat";
                    break;

                case ReSummon:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.InitialSummonRe";
                    break;
                case ReMidFight:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.MidFightRe";
                    break;
                case ReDefeat:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.DefeatRe";
                    break;

                case SlayerSummon:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.InitialSummonAlt";
                    break;
                case SlayerMidFight:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.MidFightAlt";
                    break;
                case Desperation:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.Desperation";
                    break;
                case Death:
                    key = "Mods.ShardsOfAtheria.NPCDialogue.NovaStellar.Death";
                    break;
            }

            NPC.UseDialogueWithKey(key, Color.DeepSkyBlue);
        }

        void KillProjectiles()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                var proj = Main.projectile[i];
                if (proj.type == ModContent.ProjectileType<ElectricTrail>() || proj.type == ModContent.ProjectileType<FeatherBlade>() ||
                    proj.type == ModContent.ProjectileType<LightningBolt>() || proj.type == ModContent.ProjectileType<StormSword>() ||
                    proj.type == ModContent.ProjectileType<StormCloud>() || proj.type == ModContent.ProjectileType<LightningBolt>() ||
                    proj.type == ModContent.ProjectileType<LightningBolt>() || proj.type == ModContent.ProjectileType<StormLance>())
                {
                    proj.Kill();
                }
            }
        }

        public override void DrawEffects(ref Color drawColor)
        {
            if (phase2 && Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(NPC.position, NPC.width + 4, NPC.height + 4, DustID.Electric, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
            if (desperation && Main.rand.NextBool(8))
            {
                int dust = Dust.NewDust(NPC.position - new Vector2(2f, 2f), NPC.width + 4, NPC.height + 4, DustID.Blood, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 drawOrigin = new(texture.Value.Width * 0.5f, NPC.height * 0.5f);
            Vector2 drawPos = NPC.Center - screenPos;
            if (attackTimer > 0)
            {
                switch (attackType)
                {
                    default:
                        if (phase2)
                        {
                            frameX = 1;
                        }
                        else
                        {
                            frameX = 0;
                        }
                        maxFrameY = 6;
                        break;
                    case FeatherBarrage:
                    case StormCloud:
                        if (phase2)
                        {
                            frameX = 2;
                        }
                        else
                        {
                            frameX = 0;
                        }
                        maxFrameY = 6;
                        break;
                    case BowShoot:
                        frameX = 3;
                        maxFrameY = 6;
                        break;
                    case SwordSwing:
                        frameX = 4;
                        maxFrameY = 6;
                        break;
                }
            }

            if (++NPC.frameCounter >= 5 && !Main.gameInactive)
            {
                if (++frameY >= maxFrameY)
                {
                    frameY = 0;
                }
                NPC.frameCounter = 0;
            }
            Rectangle frame = new(100 * frameX, 100 * frameY, 100, 100);
            spriteBatch.Draw(texture.Value, drawPos, frame, drawColor, NPC.rotation, frame.Size() / 2f, NPC.scale, effects, 0f);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            base.PostDraw(spriteBatch, screenPos, drawColor);
        }
    }
}
