﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories.GemCores
{
	[AutoloadEquip(EquipType.Wings)]
	public class GreaterEmeraldCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Counts as wings\n" +
				"10% increased movement speed\n" +
				"Increased jump height" +
				"Panic Necklace, Lava Waders and Flippers effects\n" +
				"Grants flight and slowfall");
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
			player.moveSpeed += .1f;
			player.jumpBoost = true;
			player.wingTimeMax = 4 * 60;
			player.panic = true;
			player.accFlipper = true;
			player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 420;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<EmeraldCore>());
			recipe.AddIngredient(ItemID.SoulofFlight, 10);
			recipe.AddIngredient(ItemID.Flipper);
			recipe.AddIngredient(ItemID.LavaWaders);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
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