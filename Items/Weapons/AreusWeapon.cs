﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	// This class handles everything for our custom damage class
	// Any class that we wish to be using our custom damage class will derive from this class, instead of ModItem
	public abstract class AreusWeapon : ModItem
	{
		public override bool CloneNewInstances => true;
		public int areusResourceCost = 0;

		// Because we want the damage tooltip to show our custom damage, we need to modify it
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (areusResourceCost > 0)
			{
				tooltips.Add(new TooltipLine(mod, "Areua Resource Cost", $"Uses {areusResourceCost} areus charge"));
			}
		}

		// Make sure you can't use the item if you don't have enough resource and then use resourceCost otherwise.
		public override bool CanUseItem(Player player)
		{
			var exampleDamagePlayer = player.GetModPlayer<SMPlayer>();

			if (exampleDamagePlayer.areusResourceCurrent >= areusResourceCost)
			{
				exampleDamagePlayer.areusResourceCurrent -= areusResourceCost;
				return true;
			}
			return false;
		}

		public override void HoldItem(Player player)
		{
			player.GetModPlayer<SMPlayer>().areusWeapon = true;
		}
	}
}