using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    public class PerishSong : SlayerItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'I M  D E A D'");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 20;

            Item.damage = 220;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4;
            Item.crit = 20;
            Item.mana = 25;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item103;
            Item.autoReuse = true;

            Item.shootSpeed = 10;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.shoot = ModContent.ProjectileType<DeathNote>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.FragmentNebula, 32)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
        }
    }
}