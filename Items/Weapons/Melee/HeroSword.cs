using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class HeroSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("'The sword of a long forgotten hero'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() 
		{
			Item.width = 62;
			Item.height = 62;

			Item.damage = 160;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 6;
			Item.crit = 6;

			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;

			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 2, 50);
			Item.shoot = ModContent.ProjectileType<Projectiles.Weapon.Melee.HeroSword>();
			Item.shootSpeed = 1;
		}

		public override void AddRecipes() 
		{
			CreateRecipe()
				.AddIngredient(ItemID.BrokenHeroSword)
				.AddRecipeGroup(RecipeGroupID.IronBar, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}