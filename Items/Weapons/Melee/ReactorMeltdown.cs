using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class ReactorMeltdown : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElec();
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;

            Item.damage = 162;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2;
            Item.crit = 7;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 2, 20);
            Item.shoot = ModContent.ProjectileType<ReactorMeltdownProj>();
        }
    }
}