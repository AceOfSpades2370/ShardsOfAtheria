﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories.GemCores
{
	[AutoloadEquip(EquipType.Wings)]
	public class SuperEmeraldCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Counts as wings\n" +
				"15% increased movement speed\n" +
				"Bundle of Balloons, Panic Necklace, Frostspark Boots, Lava Waders and Flippers effects\n" +
				"Grants flight, slowfall and immunity to cold debuffs");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.wingTimeMax = 4 * 60;
			player.panic = true;
			player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 420;
			player.accFlipper = true;
			player.accRunSpeed = 6.75f;
			player.rocketBoots = 3;
			player.moveSpeed += 1;
			player.iceSkate = true;
			player.doubleJumpCloud = true;
			player.doubleJumpBlizzard = true;
			player.doubleJumpSandstorm = true;
			player.jumpBoost = true;
			player.GetModPlayer<SMPlayer>().superEmeraldCore = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GreaterEmeraldCore>());
			recipe.AddIngredient(ItemID.FragmentNebula, 5);
			recipe.AddIngredient(ItemID.FragmentStardust, 5);
			recipe.AddIngredient(ItemID.FrostsparkBoots);
			recipe.AddIngredient(ItemID.BundleofBalloons);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var list = SagesMania.EmeraldTeleportKey.GetAssignedKeys();
			string keyname = "Not bound";

			if (list.Count > 0)
			{
				keyname = list[0];
			}

			tooltips.Add(new TooltipLine(mod, "Damage", $"Allows teleportation on press of '[i:{keyname}]'"));
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

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
		{
			speed = 9f;
			acceleration *= 2.5f;
		}
	}
}