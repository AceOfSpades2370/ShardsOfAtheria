using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusGhostLantern : ModItem
    {
        public int poes = 0;
        int poeSpawnTimer = 0;

        public override void SetStaticDefaults()
        {
            Item.AddAreus(true, true);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            poes = 10;
            Item.width = 26;
            Item.height = 40;
            Item.scale = 0.6f;

            Item.damage = 42;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3f;
            Item.mana = 2;

            Item.useTime = 5;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.holdStyle = ItemHoldStyleID.HoldLamp;

            Item.shootSpeed = 7f;
            Item.rare = ItemDefaults.RarityHardmodeDungeon;
            Item.value = Item.sellPrice(0, 1, 25);
            Item.shoot = ModContent.ProjectileType<ElectricFlame>();
        }

        public override void UpdateInventory(Player player)
        {
            string key = this.GetLocalizationKey("DisplayName");
            string name = Language.GetTextValue(key) + " (" + poes + ")";
            Item.SetNameOverride(name);

            if (++poeSpawnTimer >= 240 + Main.rand.Next(240) - player.Shards().combatTimer / 3)
            {
                bool validPosition = false;
                var vector = player.Center + Main.rand.NextVector2Circular(10, 10) * 50;
                while (!validPosition)
                {
                    vector = player.Center + Main.rand.NextVector2Circular(10, 10) * 50;
                    validPosition = !Collision.SolidCollision(vector, 20, 20) && Collision.CanHit(player.position, player.width, player.height, vector, 20, 20);
                }
                Projectile.NewProjectile(Item.GetSource_FromThis(), vector, Vector2.Zero, ModContent.ProjectileType<ElectricPoe>(), player.GetWeaponDamage(Item), 0);
                poeSpawnTimer = 0;
            }
        }

        public override bool CanUseItem(Player player)
        {
            return poes > 0;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.itemAnimation == player.itemAnimationMax)
            {
                if (!player.Overdrive() || !Main.rand.NextBool(5))
                {
                    poes--;
                }
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}