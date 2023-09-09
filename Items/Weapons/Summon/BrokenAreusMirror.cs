using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class BrokenAreusMirror : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;

            Item.damage = 18;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 0;
            Item.crit = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item82;
            Item.noMelee = true;

            Item.shootSpeed = 0;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 10000;
            Item.shoot = ModContent.ProjectileType<Projectiles.Minions.BrokenAreusMirror>();

            Item.buffType = ModContent.BuffType<AreusMirrorBuff>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Minions.BrokenAreusMirror>()] >= 1)
            {
                type = ModContent.ProjectileType<AreusMirrorShard>();
            }
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = player.Center + new Vector2(0, -20);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            if (type == ModContent.ProjectileType<Projectiles.Minions.BrokenAreusMirror>())
            {
                // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
                var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer, 1);
                projectile.originalDamage = Item.damage;
            }
            else if (type == ModContent.ProjectileType<AreusMirrorShard>())
            {
                for (int i = 0; i < 6; i++)
                {
                    // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
                    var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage / 3, knockback, Main.myPlayer);
                    projectile.originalDamage = Item.damage / 3;
                }
            }

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient<Jade>(5)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}