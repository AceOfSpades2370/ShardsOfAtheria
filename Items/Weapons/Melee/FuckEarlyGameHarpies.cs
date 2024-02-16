using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class FuckEarlyGameHarpies : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Spears[Type] = true;
            Item.AddAreus(true);
            Item.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.damage = 6;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemRarityID.Gray;
            Item.value = 0;
            Item.shoot = ModContent.ProjectileType<BrokenAreusPartizan>();
        }
    }
}