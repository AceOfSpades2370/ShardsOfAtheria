using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Audio;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class HandCanon : SlayerItem
    {
        public int charge;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Charges passively when not in use\n" +
                "After 5 seconds of charging damage is increased and fire a burst of 3 grenades\n" +
                "'Demoman TF2'");

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 24;

            Item.damage = 65;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;
            Item.crit = 5;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.noMelee = true;

            Item.shootSpeed = 20f;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.value = 22500;
            Item.shoot = ProjectileID.Grenade;
            Item.useAmmo = ItemID.Grenade;
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation);
        }

        public override void UpdateInventory(Player player)
        {
            if (++charge == 300)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
                CombatText.NewText(player.getRect(), Color.SkyBlue, "Charge ready!");
            }
            if (charge > 301)
            {
                Item.useAnimation = 30;
                charge = 301;
            }
            else Item.useAnimation = 10;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(player, ref damage);
            if (charge >= 300)
                damage += 1f;
        }

        public override bool? UseItem(Player player)
        {
            charge = 0;
            SoundEngine.PlaySound(Item.UseSound.Value);
            return base.UseItem(player);
        }
    }
}