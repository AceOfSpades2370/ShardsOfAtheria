﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.PetItems;
using ShardsOfAtheria.Items.SoulCrystals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SlayerNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            Player lastPlayerToHitThisNPC = npc.AnyInteractions() ? Main.player[npc.lastInteraction] : null;
            if (lastPlayerToHitThisNPC != null)
            {
                if (!lastPlayerToHitThisNPC.IsSlayer())
                {
                    return;
                }
                int numPlayers = Main.CurrentFrameFlags.ActivePlayersCount;
                if (npc.type == NPCID.KingSlime)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainKing = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.KingSlimeMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.NinjaHood);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.NinjaShirt);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.NinjaPants);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SlimeGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SlimeHook);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RoyalGel);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.KingSlimePetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<KingSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Solidifier, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.KingSlimeTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.KingSlimeMasterTrophy);
                }
                if (npc.type == NPCID.EyeofCthulhu)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainEOC = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Binoculars);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EoCShield);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeOfCthulhuPetItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.AviatorSunglasses);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<EyeSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrimtaneOre, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrimsonSeeds, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DemoniteOre, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CorruptSeeds, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeofCthulhuTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeofCthulhuMasterTrophy);
                }
                if (npc.type == NPCID.BrainofCthulhu)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainBOC = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BoneRattle);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainOfConfusion);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainOfCthulhuPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<BrainSoulCrystal>());
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<StrangeTissueSample>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrimtaneOre, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TissueSample, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainofCthulhuTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BrainofCthulhuMasterTrophy);
                }
                if (npc.boss && Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainEOW = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        //NormalMode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EaterMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EatersBone);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WormScarf);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<WormBloom>());

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EaterOfWorldsPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<EaterSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DemoniteOre, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ShadowScale, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EaterofWorldsTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EaterofWorldsMasterTrophy);
                }
                if (npc.type == NPCID.QueenBee)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainBee = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HiveWand);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeHat);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeShirt);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeePants);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HoneyedGoggles);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Nectar);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HoneyComb);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeKeeper);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeesKnees);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HiveBackpack);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenBeePetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<BeeSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BottledHoney, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeeWax, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Beenade, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenBeeTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenBeeMasterTrophy);
                }
                if (npc.type == NPCID.SkeletronHead)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainSkull = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BookofSkulls);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronHand);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BoneGlove);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SkullSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronMasterTrophy);
                }
                if (npc.type == NPCID.Deerclops)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainDeerclops = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DeerclopsMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ChesterPetItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Eyebrella);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DontStarveShaderItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PewMaticHorn);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WeatherPain);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HoundiusShootius);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LucyTheAxe);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BoneHelm);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DeerclopsPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DeerclopsSoulCrystal>());
                    }

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DeerclopsTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DeerclopsMasterTrophy);
                }
                if (npc.type == NPCID.WallofFlesh)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainWall = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FleshMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Pwnhammer);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RangerEmblem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WarriorEmblem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SorcererEmblem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SummonerEmblem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BreakerBlade);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ClockworkAssaultRifle);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LaserRifle);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BadgersHat);

                        // Expert Mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DemonHeart);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WallOfFleshGoatMountItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<WallSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WallofFleshTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WallofFleshMasterTrophy);
                }
                if (npc.type == NPCID.QueenSlimeBoss)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainQueen = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrystalNinjaHelmet);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrystalNinjaChestplate);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.CrystalNinjaLeggings);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GelBalloon);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Smolstar);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeHook);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeMountSaddle);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.VolatileGelatin);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimePetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<QueenSoulCrystal>());
                    }

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.QueenSlimeMasterTrophy);
                }
                if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail && npc.boss)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainMechWorm = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DestroyerMask);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MechanicalWagonPiece);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DestroyerPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DestroyerSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SoulofMight, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DestroyerTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DestroyerMasterTrophy);
                }
                if (npc.boss && Array.IndexOf(new int[] { NPCID.Spazmatism, NPCID.Retinazer }, npc.type) > -1)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainTwins = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TwinMask);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MechanicalWheelPiece);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TwinsPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<TwinsSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SoulofSight, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RetinazerTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SpazmatismTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TwinsMasterTrophy);
                }
                if (npc.type == NPCID.SkeletronPrime)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPrime = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPrimeMask);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MechanicalBatteryPiece);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPrimePetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<PrimeSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SoulofFright, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HallowedBar, 333);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPrimeTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SkeletronPrimeMasterTrophy);
                }
                if (npc.type == NPCID.Plantera)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPlant = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PlanteraMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TempleKey);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Seedling);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GrenadeLauncher);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.VenusMagnum);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.NettleBurst);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LeafBlower);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FlowerPow);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.WaspGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Seedler);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PygmyStaff);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ThornHook);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TheAxe);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SporeSac);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PlanteraPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<PlantSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PlanteraTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PlanteraMasterTrophy);
                }
                if (npc.type == NPCID.Golem)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainGolem = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Picksaw);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Stynger);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SunStone);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EyeoftheGolem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HeatRay);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.StaffofEarth);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemFist);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PossessedHatchet);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.ShinyStone);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<GolemSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BeetleHusk, 1000);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.StyngerBolt, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GolemMasterTrophy);
                }
                if (npc.type == NPCID.DukeFishron)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainDuke = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BubbleGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Flairon);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RazorbladeTyphoon);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.TempestStaff);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Tsunami);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FishronWings);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DukeFishronMask);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DukeFishronPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<DukeSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DukeFishronTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.DukeFishronMasterTrophy);
                }
                if (npc.type == NPCID.HallowBoss)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainEmpress = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenMask);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenMagicItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PiercingStarlight);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RainbowWhip);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenRangedItem);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RainbowWings);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SparkleGuitar);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RainbowCursor);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EmpressBlade);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.EmpressFlightBooster);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<EmpressSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.HallowBossDye, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FairyQueenMasterTrophy);
                }
                if (npc.type == NPCID.CultistBoss)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainLunatic = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BossMaskCultist);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LunaticCultistPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<LunaticSoulCrystal>());
                    }
                    if (ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        ModContent.GetInstance<ShardsDownedSystem>().slainPillarNebula = true;
                        ModContent.GetInstance<ShardsDownedSystem>().slainPillarSolar = true;
                        ModContent.GetInstance<ShardsDownedSystem>().slainPillarStardust = true;
                        ModContent.GetInstance<ShardsDownedSystem>().slainPillarVortex = true;
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentNebula, 1000);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentSolar, 1000);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentStardust, 1000);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentVortex, 1000);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 1000);
                    }

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.AncientCultistTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LunaticCultistMasterTrophy);
                }
                if (npc.type == NPCID.LunarTowerNebula)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPillarNebula = true;
                    if (!ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentNebula, 1000);
                        for (int i = 0; i < 10; i++)
                        {
                            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 25);
                        }
                    }
                }
                if (npc.type == NPCID.LunarTowerSolar)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPillarSolar = true;
                    if (!ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentSolar, 1000);
                        for (int i = 0; i < 10; i++)
                        {
                            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 25);
                        }
                    }
                }
                if (npc.type == NPCID.LunarTowerStardust)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPillarStardust = true;
                    if (!ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentStardust, 1000);
                        for (int i = 0; i < 10; i++)
                        {
                            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 25);
                        }
                    }
                }
                if (npc.type == NPCID.LunarTowerVortex)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainPillarVortex = true;
                    if (!ModLoader.TryGetMod("NoMorePillars", out Mod foundMod))
                    {
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FragmentVortex, 1000);
                        for (int i = 0; i < 10; i++)
                        {
                            Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<FragmentEntropy>(), 25);
                        }
                    }
                }
                if (npc.type == NPCID.MoonLordCore)
                {
                    ModContent.GetInstance<ShardsDownedSystem>().slainMoonLord = true;
                    for (int i = 0; i < numPlayers; i++)
                    {
                        // Normal mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.BossMaskMoonlord);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.PortalGun);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Meowmere);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Terrarian);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.StarWrath);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.SDMG);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LastPrism);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LunarFlareBook);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.RainbowCrystalStaff);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MoonlordTurretStaff);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.FireworksLauncher);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.Celeb2);
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MeowmereMinecart);

                        // Expert mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.GravityGlobe);

                        // Master mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MoonLordPetItem);

                        // Slayer mode
                        Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<LordSoulCrystal>());
                    }
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.LunarOre, 1000);

                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MoonLordTrophy);
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemID.MoonLordMasterTrophy);
                }
            }
        }

        public override bool PreAI(NPC npc)
        {
            int Type = npc.type;
            Color color = Color.White;

            if (Type == NPCID.KingSlime && ModContent.GetInstance<ShardsDownedSystem>().slainKing)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("King Slime was slain..."), color);
                npc.active = false;
                return false;
            }
            else if (Type == NPCID.EyeofCthulhu && ModContent.GetInstance<ShardsDownedSystem>().slainEOC)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Eye of Cthulhu was slain..."), color);
                npc.active = false;
                return false;
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainBOC)
            {
                if (Type == NPCID.BrainofCthulhu)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Brain of Cthulhu was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.Creeper && ModContent.GetInstance<ShardsDownedSystem>().slainBOC)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainEOW)
            {
                if (Type == NPCID.EaterofWorldsHead)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Eater of Worlds was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.EaterofWorldsBody || Type == NPCID.EaterofWorldsTail)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (Type == NPCID.QueenBee && ModContent.GetInstance<ShardsDownedSystem>().slainBee)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Queen Bee was slain..."), color);
                npc.active = false;
                return false;
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainSkull)
            {
                if (Type == NPCID.SkeletronHead || Type == NPCID.DungeonGuardian)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Skeletron was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.SkeletronHand)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (Type == NPCID.Deerclops && ModContent.GetInstance<ShardsDownedSystem>().slainDeerclops)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Deerclops was slain..."), color);
                npc.active = false;
                return false;
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainWall)
            {
                if (Type == NPCID.WallofFlesh)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("the Wall of Flesh was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.WallofFleshEye)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (Type == NPCID.QueenSlimeBoss && ModContent.GetInstance<ShardsDownedSystem>().slainQueen)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Queen Slime was slain..."), color);
                npc.active = false;
                return false;
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainMechWorm)
            {
                if (Type == NPCID.TheDestroyer)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Destroyer was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.TheDestroyerBody || Type == NPCID.TheDestroyerTail)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainTwins)
            {
                if (Type == NPCID.Spazmatism)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Spazmatism was slain..."), color);
                    npc.active = false;
                    return false;
                }
                if (Type == NPCID.Retinazer)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Retinazer was slain..."), color);
                    npc.active = false;
                    return false;
                }
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainPrime)
            {
                if (Type == NPCID.SkeletronPrime)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Skeletron Prime was slain... (Again, how???)"), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.PrimeCannon || Type == NPCID.PrimeLaser || Type == NPCID.PrimeSaw || Type == NPCID.PrimeVice)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainPlant)
            {
                if (Type == NPCID.Plantera)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Plantera was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.PlanterasHook || Type == NPCID.PlanterasTentacle)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainGolem)
            {
                if (Type == NPCID.Golem)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Golem was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.GolemFistLeft || Type == NPCID.GolemFistRight || Type == NPCID.GolemHead)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (Type == NPCID.DukeFishron && ModContent.GetInstance<ShardsDownedSystem>().slainDuke)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Duke Fishron was slain..."), color);
                npc.active = false;
                return false;
            }
            else if (Type == NPCID.HallowBoss && ModContent.GetInstance<ShardsDownedSystem>().slainEmpress)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Empress of Light was slain..."), color);
                npc.active = false;
                return false;
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainLunatic)
            {
                if (Type == NPCID.CultistBoss)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Lunatic Cultist was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.CultistTablet)
                {
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.CultistArcherBlue)
                {
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.CultistArcherWhite)
                {
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.CultistDevote)
                {
                    npc.active = false;
                    return false;
                }
            }
            else if (Type == NPCID.LunarTowerNebula && ModContent.GetInstance<ShardsDownedSystem>().slainPillarNebula)
            {
                npc.active = false;
                return false;
            }
            else if (Type == NPCID.LunarTowerSolar && ModContent.GetInstance<ShardsDownedSystem>().slainPillarSolar)
            {
                npc.active = false;
                return false;
            }
            else if (Type == NPCID.LunarTowerStardust && ModContent.GetInstance<ShardsDownedSystem>().slainPillarStardust)
            {
                npc.active = false;
                return false;
            }
            else if (Type == NPCID.LunarTowerVortex && ModContent.GetInstance<ShardsDownedSystem>().slainPillarVortex)
            {
                npc.active = false;
                return false;
            }
            else if (ModContent.GetInstance<ShardsDownedSystem>().slainMoonLord)
            {
                if (Type == NPCID.MoonLordCore)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("The Moon Lord was slain..."), color);
                    npc.active = false;
                    return false;
                }
                else if (Type == NPCID.MoonLordHand || Type == NPCID.MoonLordHead)
                {
                    npc.active = false;
                    return false;
                }
            }
            return base.PreAI(npc);
        }
    }
}
