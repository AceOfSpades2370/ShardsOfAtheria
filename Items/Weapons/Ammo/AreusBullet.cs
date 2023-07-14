using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class AreusBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 1200;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<AreusBulletProj>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.MusketBall, 100)
                .AddIngredient(ModContent.ItemType<AreusShard>())
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}