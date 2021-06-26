using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.Items.Weapons
{
    public class Pandora : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left Click to thrust a spear, <right> to fire an ice bolt\n" +
                "''Destiny of destruction awaits''");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("SM:GoldBars", 7);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddIngredient(ItemID.IceBlock, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noMelee = true;
                item.noUseGraphic = false;
                Item.staff[item.type] = true;
                item.useTime = 20;
                item.useAnimation = 20;
                item.UseSound = SoundID.Item30;
                item.damage = 87;
                item.magic = true;
                item.melee = false;
                item.mana = 6;
                item.knockBack = 3;
                item.shoot = ModContent.ProjectileType<IceBolt>();
                item.shootSpeed = 15;
            }
            else
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.useTime = 30;
                item.useAnimation = 30;
                item.UseSound = SoundID.Item1;
                item.damage = 107;
                item.melee = true;
                item.magic = false;
                item.mana = 0;
                item.knockBack = 6;
                item.shoot = ModContent.ProjectileType<PandoraProjectile>();
                item.shootSpeed = 2.3f;
                return player.ownedProjectileCounts[item.shoot] < 1;
            }
            return base.CanUseItem(player);
        }
    }
}