﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Items.Placeable.Banner;
using ShardsOfAtheria.Projectiles.NPCProj.Variant.HarpyFeather;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public class CrimsonHarpy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = 1f,
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPC.AddElement(1);
            NPC.AddElement(3);
            NPC.AddRedemptionElement(12);
            NPC.AddRedemptionElementType("Humanoid");
            NPC.AddRedemptionElementType("Blood");
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.damage = 22;
            NPC.defense = 15;
            NPC.lifeMax = 80;
            AnimationType = NPCID.Harpy;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<CrimsonHarpyBanner>();
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersAqua);
        }

        public override void AI()
        {
            NPC.ai[0] += 1f;
            if (NPC.ai[0] == 30f || NPC.ai[0] == 60f || NPC.ai[0] == 90f)
            {
                if (Main.rand.NextBool(3) && NPC.ai[0] == 30f)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 6f,
                        ProjectileID.GoldenShowerHostile, 13, 0f, Main.myPlayer);
                    NPC.ai[0] = 91;
                }
                else if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    int num729 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 6f,
                        ModContent.ProjectileType<Blood>(), 13, 0f, Main.myPlayer);
                    Main.projectile[num729].timeLeft = 300;
                }
            }
            else if (NPC.ai[0] >= 400 + Main.rand.Next(400))
            {
                NPC.ai[0] = 0f;
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            string key = this.GetLocalizationKey("Bestiary");
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement(key)
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!(spawnInfo.Player.ZoneHallow || spawnInfo.Player.ZoneCrimson ||
                spawnInfo.Player.ZoneTowerNebula || spawnInfo.Player.ZoneTowerVortex ||
                spawnInfo.Player.ZoneTowerSolar || spawnInfo.Player.ZoneTowerStardust ||
                Main.pumpkinMoon || Main.snowMoon) && spawnInfo.Player.ZoneCrimson &&
                spawnInfo.Player.ZoneOverworldHeight)
                return .05f;
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule hardmode = new LeadingConditionRule(new Conditions.IsHardmode());

            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.ViciousMushroom, 1, 3, 6));
            hardmode.OnSuccess(ItemDropRule.Common(ItemID.Ichor, 5, 1, 3));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 10; i++)
            {
                var vector = Main.rand.NextVector2CircularEdge(4f, 4f);
                vector *= 1f - Main.rand.NextFloat(0.66f);
                Projectile.NewProjectile(NPC.GetSource_OnHit(NPC), NPC.Center, vector, ProjectileID.BloodNautilusShot, 13, 0);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.Ichor, 60);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            NPC.BasicInWorldGlowmask(spriteBatch, texture.Value, drawColor, screenPos, effects);
            return false;
        }
    }
}