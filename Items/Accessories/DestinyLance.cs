﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class DestinyLance : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(1);
            Item.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 26;
            Item.accessory = true;

            Item.damage = 42;
            Item.knockBack = 6;
            Item.crit = 2;

            Item.shoot = ModContent.ProjectileType<SpearOfDestiny>();
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = 57500;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.ownedProjectileCounts[Item.shoot] == 0)
            {
                Vector2 velocity = Vector2.Zero;
                if (player == Main.LocalPlayer) velocity = player.Center.DirectionTo(Main.MouseWorld);
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center, velocity, Item.shoot, player.GetWeaponDamage(Item), player.GetWeaponKnockback(Item));
            }
        }
    }
}