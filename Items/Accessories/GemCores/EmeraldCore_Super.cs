﻿using ShardsOfAtheria.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.GemCores
{
	[AutoloadEquip(EquipType.Wings)]
	public class EmeraldCore_Super : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Emerald Core");
			Tooltip.SetDefault("Counts as wings\n" +
				"15% increased movement speed\n" +
				"Bundle of Balloons, Panic Necklace, Terraspark Boots, and Flippers effects\n" +
				"Grants flight, slowfall and immunity to cold debuffs");

			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(180, 9f, 2.5f);

			SacrificeTotal = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;

			Item.rare = ItemRarityID.Lime;
			Item.value = Item.sellPrice(0, 3);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// Terraspark Boots
			player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 420;
			player.accRunSpeed = 6.75f;
			player.rocketBoots = 3;
			player.iceSkate = true;

			// Misc
			player.panic = true;
			player.accFlipper = true;
			player.hasJumpOption_Cloud = true;
			player.hasJumpOption_Blizzard = true;
			player.hasJumpOption_Sandstorm = true;
			player.jumpBoost = true;
			player.GetModPlayer<SoAPlayer>().superEmeraldCore = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<EmeraldCore_Greater>())
				.AddIngredient(ItemID.FragmentNebula, 5)
				.AddIngredient(ItemID.FragmentStardust, 5)
				.AddIngredient(ItemID.RodofDiscord)
				.AddIngredient(ItemID.BundleofBalloons)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var list = ShardsOfAtheria.EmeraldTeleportKey.GetAssignedKeys();
			string keyname = "Not bound";

			if (list.Count > 0)
			{
				keyname = list[0];
			}

			tooltips.Add(new TooltipLine(Mod, "Teleport", $"Allows teleportation on press of '[i:{keyname}]'"));
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 0.85f;
			ascentWhenRising = 0.15f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}
	}
}