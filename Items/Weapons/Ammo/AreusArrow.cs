using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Globals;

namespace ShardsOfAtheria.Items.Weapons.Ammo
{
    public class AreusArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2f;
            Item.value = 1200;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<AreusArrowProj>();
            Item.shootSpeed = 3f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.WoodenArrow, 100)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddIngredient(ModContent.ItemType<AreusShard>())
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}