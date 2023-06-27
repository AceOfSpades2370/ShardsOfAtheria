﻿using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.AreusChips;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.Consumable;
using ShardsOfAtheria.Items.DedicatedItems.Webmillio;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.ModCondition.ItemDrop;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Melee.AreusSwordProjs;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static ShardsOfAtheria.Utilities.ShardsHelpers;

namespace ShardsOfAtheria.NPCs.Town.TheAtherian
{
    // [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class Atherian : ModNPC
    {
        public override string Texture => "ShardsOfAtheria/NPCs/Town/TheAtherian/Atherian" + (SoA.AprilFools ? "_AprilFools" : "");

        public override void SetStaticDefaults()
        {
            if (SoA.AprilFools)
            {
                Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has

                NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
                NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
            }
            else
            {
                Main.npcFrameCount[Type] = 26;
                NPCID.Sets.ExtraFramesCount[Type] = 9;
                NPCID.Sets.AttackFrameCount[Type] = 5;
            }
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

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

            // Set Atherian's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in ExampleMod/Localization/en-US.lang
            NPC.Happiness
                //Biomes
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Hate)
                //NPCs
                .SetNPCAffection(NPCID.Stylist, AffectionLevel.Love)
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like);
        }

        internal void SetupShopQuotes(Mod shopQuotes)
        {
            shopQuotes.Call("AddNPC", Mod, Type);
            shopQuotes.Call("SetColor", Type, new Color(165, 140, 190));
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;

            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 60;
            NPC.defense = 10;
            NPC.lifeMax = 1000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            if (SoA.AprilFools)
            {
                AnimationType = NPCID.Guide;
            }
            else
            {
                AnimationType = NPCID.Stylist;
            }
            NPC.ElementMultipliers() = new[] { 0.5f, 1.0f, 0.4f, 2.0f };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.ShardsOfAtheria.NPCs.Atherian.Bestuary"))
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            int num = NPC.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                    continue;
                ShardsDownedSystem shardsDowned = SoA.DownedSystem;
                if (NPC.downedBoss2 && (!(shardsDowned.slainSenterra || shardsDowned.slainGenesis || shardsDowned.slainValkyrie) ||
                    SoA.ServerConfig.cluelessNPCs) && !shardsDowned.slainAtherian)
                    return true;
            }
            return false;
        }

        public override bool PreAI()
        {
            if (!SoA.ServerConfig.cluelessNPCs)
            {
                string keyBase = "ShardsOfAtheria.NPCs.Atherian.Dialogue.";
                if (SoA.DownedSystem.slainSenterra && SoA.DownedSystem.slainGenesis &&
                    SoA.DownedSystem.slainValkyrie)
                {
                    NPC.active = false;
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(keyBase + "AllDeath"), Color.Red);
                    return false;
                }
                else if (SoA.DownedSystem.slainSenterra || SoA.DownedSystem.slainGenesis)
                {
                    NPC.active = false;
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(keyBase + "GoddessDeath"), Color.Red);
                    return false;
                }
                else if (SoA.DownedSystem.slainValkyrie)
                {
                    NPC.active = false;
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(keyBase + "NovaDeath"), Color.Red);
                    return false;
                }
            }
            return base.PreAI();
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>() { "Jordan", "Damien", "Jason", "Kevin", "Rain", "Sage", "Archimedes" };
        }

        const string DialogueKeyBase = "Mods.ShardsOfAtheria.NPCs.Atherian.Dialogue.";
        public override string GetChat()
        {
            WeightedRandom<string> chat = new();
            Player player = Main.LocalPlayer;

            if ((!player.GetModPlayer<SlayerPlayer>().slayerMode || SoA.ServerConfig.cluelessNPCs) &&
                player.HasItem(ModContent.ItemType<GenesisAndRagnarok>()))
            {
                ShardsPlayer shardsPlayer = player.Shards();
                int upgrades = shardsPlayer.genesisRagnarockUpgrades;
                if (upgrades == 0)
                {
                    return Language.GetTextValue(DialogueKeyBase + "BaseGenesisAndRagnarok");
                }
            }

            chat.Add(Language.GetTextValue(DialogueKeyBase + "AtheriaComment"));
            bool goldCrown = false;
            for (int i = 0; i < player.armor.Count(); i++)
            {
                Item item = player.armor[i];
                if (item.type == ItemID.GoldCrown)
                {
                    chat.Add(Language.GetTextValue(DialogueKeyBase + "GoldCrownCompliment"));
                    goldCrown = true;
                    break;
                }
            }
            if (!goldCrown)
            {
                chat.Add(Language.GetTextValue(DialogueKeyBase + "GoldCrownThought"));
            }
            if (ShardsDownedSystem.downedValkyrie)
            {
                chat.Add(Language.GetTextValue(DialogueKeyBase + "ExComment"));
            }
            int guide = NPC.FindFirstNPC(NPCID.Guide);
            if (guide >= 0)
            {
                chat.Add(Language.GetTextValue(DialogueKeyBase + "CSGOReference", Main.npc[guide].GivenName));
            }
            chat.Add(Language.GetTextValue(DialogueKeyBase + "MorshuMoment", player.name));
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Upgrade";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }
            else
            {
                Player player = Main.LocalPlayer;
                ShardsPlayer shardsPlayer = player.Shards();
                int upgrades = shardsPlayer.genesisRagnarockUpgrades;

                if (player.Slayer().slayerMode && !SoA.ServerConfig.cluelessNPCs)
                {
                    Main.npcChatText = Language.GetTextValue(DialogueKeyBase + "RefuseUpgrade");
                }
                else if (PlayerHasItem<GenesisAndRagnarok>(player) && upgrades < 5)
                {
                    int result = ModContent.ItemType<GenesisAndRagnarok>();
                    var materials = new UpgrageMaterial[0];
                    switch (upgrades)
                    {
                        case 0:
                            materials = new[] {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()])
                            };
                            break;
                        case 1:
                            materials = new[] {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()]),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.ChlorophyteBar], 14)
                            };
                            break;
                        case 2:
                            materials = new[] {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()]),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.BeetleHusk], 16)
                            };
                            break;
                        case 3:
                            materials = new[] {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()]),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.FragmentSolar], 18)
                            };
                            break;
                        case 4:
                            materials = new[] {
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<GenesisAndRagnarok>()], 0),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<MemoryFragment>()]),
                                new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.LunarBar], 20)
                            };
                            break;
                    }
                    UpgradeItem<GenesisAndRagnarok>(player, materials);
                }
                else if (PlayerHasItem<AreusKatana>(player))
                {
                    UpgrageMaterial[] materials = {
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusKatana>()]),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.BeetleHusk], 20),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.SoulofFright], 14)
                    };
                    UpgradeItem<TheMourningStar>(player, materials);
                }
                else if (PlayerHasItem<War>(player))
                {
                    UpgrageMaterial[] materials = {
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<War>()], 0),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.HallowedBar], 20)
                    };
                    UpgradeItem<War>(player, materials);
                }
                else if (PlayerHasItem<AreusRailgun>(player))
                {
                    UpgrageMaterial[] materials = {
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusRailgun>()]),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.Flamethrower]),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.BeetleHusk], 14)
                    };
                    UpgradeItem<AreusFlameCannon>(player, materials);
                }
                else if (PlayerHasItem<AreusGambit>(player))
                {
                    UpgrageMaterial[] materials = {
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusGambit>()]),
                        //new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusGlaive>()]),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusDagger>()]),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusMagnum>()]),
                        //new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusRailgun>()]),
                        //new UpgrageMaterial(ContentSamples.ItemsByType[ModContent.ItemType<AreusBow>()]),
                        new UpgrageMaterial(ContentSamples.ItemsByType[ItemID.BeetleHusk], 15)
                    };
                    UpgradeItem<AreusGauntlet>(player, materials);
                }
                else
                {
                    Main.npcChatText = Language.GetTextValue(DialogueKeyBase + "NoUpgradableItem");
                    return;
                }
            }
        }

        bool PlayerHasItem<T>(Player player) where T : ModItem
        {
            return player.HasItem(ModContent.ItemType<T>());
        }

        /// <summary>
        /// Insert useful summary here
        /// </summary>
        /// <param name="player"> Player with the item to be upgraded</param>
        /// <param name="materials"> an array of UpgradeMaterials</param>
        public void UpgradeItem<T>(Player player, params UpgrageMaterial[] materials) where T : ModItem
        {
            ShardsPlayer shardsPlayer = player.Shards();

            int result = ModContent.ItemType<T>();
            Main.npcChatCornerItem = result;
            if (CanUpgradeItem(player, materials))
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    player.inventory[player.FindItem(materials[i].item.type)].stack -= materials[i].requiredStack;
                }
                if (materials[0].item.ModItem is GenesisAndRagnarok)
                {
                    shardsPlayer.genesisRagnarockUpgrades++;
                }
                if (materials[0].item.ModItem is War war)
                {
                    (player.inventory[player.FindItem(materials[0].item.type)].ModItem as War).upgraded = true;
                }
                SoundEngine.PlaySound(SoundID.Item37); // Reforge/Anvil sound
                if (result > 0)
                {
                    if (result == ModContent.ItemType<GenesisAndRagnarok>())
                    {
                        string key = "";
                        switch (shardsPlayer.genesisRagnarockUpgrades)
                        {
                            case 1:
                                key = DialogueKeyBase + "UpgradeGenesisAndRagnarok1";
                                break;
                            case 2:
                                key = DialogueKeyBase + "UpgradeGenesisAndRagnarok2";
                                break;
                            case 3:
                                key = DialogueKeyBase + "UpgradeGenesisAndRagnarok3";
                                break;
                            case 4:
                                key = DialogueKeyBase + "UpgradeGenesisAndRagnarok4";
                                break;
                            case 5:
                                key = DialogueKeyBase + "UpgradeGenesisAndRagnarok5";
                                break;
                        }
                        Main.npcChatText = Language.GetTextValue(key);
                    }
                    else if (result == ModContent.ItemType<War>())
                    {
                        Main.npcChatText = Language.GetTextValue(DialogueKeyBase + "UpgradeAreusWeapon");
                    }
                    else
                    {
                        Item.NewItem(NPC.GetSource_FromThis(), NPC.getRect(), result);
                        if (result == ModContent.ItemType<TheMourningStar>())
                        {
                            Main.npcChatText = Language.GetTextValue(DialogueKeyBase + "UpgradeAreusWeapon");
                        }
                    }
                }
            }
            else
            {
                string insufficient = "I need the following items:\n";
                for (int i = 0; i < materials.Length; i++)
                {
                    Item item = null;
                    if (player.HasItem(materials[i].item.type))
                    {
                        item = player.inventory[player.FindItem(materials[i].item.type)];
                    }
                    insufficient += Language.GetTextValue(DialogueKeyBase + "NotEnoughMaterial", i, materials[i].item.Name, materials[i].item.type,
                        materials[i].requiredStack, item == null ? 0 : item.stack) + "\n";
                    Main.npcChatText = insufficient;
                }
            }
        }

        public bool CanUpgradeItem(Player player, UpgrageMaterial[] upgrageMaterials)
        {
            int requiredItems = upgrageMaterials.Length;
            int playerItems = 0;
            for (int i = 0; i < requiredItems; i++)
            {
                if (player.HasItem(upgrageMaterials[i].item.type))
                {
                    Item item = player.inventory[player.FindItem(upgrageMaterials[i].item.type)];
                    if (item.stack >= upgrageMaterials[i].requiredStack)
                    {
                        playerItems++;
                    }
                }
            }
            return playerItems >= requiredItems;
        }

        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, "Shop")
                .Add<ValkyrieCrest>(new Condition("Mods.ShardsOfAtheria.Condotions.NotSlayer",
                    () => !Main.LocalPlayer.Slayer().slayerMode))
                .Add<AreusShard>()
                .Add<AreusArmorChip>()
                .Add<RushDrive>()
                .Add<AreusProcessor>()
                .Add<ResonatorRing>()
                .Add<Bytecrusher>(new Condition("Mods.ShardsOfAtheria.Conditions.AnyMech",
                    () => NPC.downedMechBossAny))
                .Add<AreusKey>(new Condition("Mods.ShardsOfAtheria.Condotions.DefeatPlantera",
                    () => NPC.downedPlantBoss))
                .Add<AnastasiasPride>(new Condition("Mods.ShardsOfAtheria.Condotions.DefeatPlantera",
                    () => NPC.downedGolemBoss));
            npcShop.Register();
        }

        public override void OnKill()
        {
            Player lastPlayerToHitThisNPC = NPC.AnyInteractions() ? Main.player[NPC.lastInteraction] : null;
            if (lastPlayerToHitThisNPC != null)
            {
                if (lastPlayerToHitThisNPC.Slayer().slayerMode)
                {
                    SoA.DownedSystem.slainAtherian = true;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule isSlayer = new(new IsSlayerMode());

            isSlayer.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ValkyrieCrest>()));
            npcLoot.Add(isSlayer);
        }

        // Make this Town NPC teleport to the King and/or Queen statue when triggered.
        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = NPC.downedMoonlord ? 200 : 60;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<ElectricBlade>();
            attackDelay = 20;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}