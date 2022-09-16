using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader.IO;
using System.Collections.Generic;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class GenesisAndRagnarok : ModItem
    {
        public int combo = 0;
        public int comboTimer;
        public int upgrades = 0;

        public override void OnCreate(ItemCreationContext context)
        {
            upgrades = 0;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["upgrades"] = upgrades;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("upgrades"))
                upgrades = tag.GetInt("upgrades");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Genesis and Ragnarok");
            Tooltip.SetDefault("If an attack is not followed up after an attack, the combo will reset\n" +
                "Use RMB to bring up a shield, release RMB to throw the shield\n" +
                "This shield grants 20 defense and is capable of parrying and reflecting some projectiles");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (upgrades == 0)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", "Preforms a combo of a spear thrust and whip swing"));
            }
            if (upgrades == 1)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", "Preforms a combo of a spear thrust, whip swing and spear throw"));
            }
            if (upgrades == 2)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", "Preforms a combo of a spear thrust, whip swing and spear throw\n" +
                    "Able to swing shield with LMB before it returns"));
            }
            if (upgrades == 3)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", "Preforms a combo of a spear thrust, whip swing and spear throw\n" +
                    "Able to swing shield with LMB before it returns\n" +
                    "Attacks set enemies on fire"));
            }
            if (upgrades == 4)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", "Preforms a combo of a spear thrust, whip swing, spear throw, and sword swing\n" +
                    "Able to swing shield with LMB before it returns\n" +
                    "Attacks set enemies on fire"));
            }
            if (upgrades == 5)
            {
                tooltips.Add(new TooltipLine(Mod, "Tooltip", "Preforms a combo of a spear thrust, whip swing, spear throw, and sword swing\n" +
                    "Able to swing shield with LMB before it fully returns\n" +
                    "Attacks inflict frostburn and create extra ice projectiles"));
            }
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 3f;
            Item.crit = 6;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemRarityID.Red;
            Item.value = Item.sellPrice(0, 4);
            combo = 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 18)
                .AddIngredient(ModContent.ItemType<ChargedFeather>(), 7)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void HoldItem(Player player)
        {
            int comboTimerMax = 60;
            // If the item is not being used, continue
            if (player.itemAnimation == 0)
            {
                // If combo is not the spear, decrement the timer
                if (combo > 0)
                    comboTimer--;

                // If the timer is 0, reset the timer and combo
                if (comboTimer == 0)
                {
                    comboTimer = comboTimerMax;
                    combo = 0;
                }
            }
            else comboTimer = comboTimerMax;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.UseSound = SoundID.Item15;
                Item.shoot = ModContent.ProjectileType<Ragnarok_Shield>();
                Item.shootSpeed = 0;
            }
            else
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] > 0)
                {
                    Item.shoot = ProjectileID.None;
                    Item.shootSpeed = 4.5f;
                    Item.UseSound = null;
                }
                else
                {
                    switch (combo)
                    {
                        case 0:
                            Item.shoot = ModContent.ProjectileType<Genesis_Spear>();
                            Item.shootSpeed = 3.7f;
                            Item.UseSound = SoundID.Item1;
                            break;
                        case 1:
                            Item.shoot = ModContent.ProjectileType<Genesis_Whip>();
                            Item.shootSpeed = 24f;
                            Item.UseSound = SoundID.Item116;
                            break;
                        case 2:
                            if (upgrades >= 1)
                            {
                                Item.shoot = ModContent.ProjectileType<Genesis_Spear2>();
                                Item.shootSpeed = 30f;
                                Item.UseSound = SoundID.Item71;
                            }
                            break;
                        case 3:
                            if (upgrades >= 4)
                            {
                                Item.shoot = ModContent.ProjectileType<Genesis_Sword>();
                                Item.shootSpeed = 30f;
                                Item.UseSound = SoundID.DD2_MonkStaffSwing;
                            }
                            break;
                    }
                }
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<Genesis_Spear>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<Genesis_Whip>()] < 1
                    && player.ownedProjectileCounts[ModContent.ProjectileType<Genesis_Spear2>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] < 1
                    && player.ownedProjectileCounts[ModContent.ProjectileType<Genesis_Sword>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] < 1
                    && player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] < 1;
        }

        public override bool? UseItem(Player player)
        {
            int comboExtra = upgrades >= 1 ? 1 : 0;
            int comboExtra2 = upgrades >= 4 ? 1 : 0;
            if (combo == 1 + comboExtra + comboExtra2)
                combo = 0;
            else combo++;
            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (upgrades == 1)
                damage += .5f;
            if (upgrades == 2)
                damage += 1f;
            if (upgrades == 3)
                damage += 2f;
            if (upgrades == 4)
                damage += 3f;
            if (upgrades == 5)
                damage += 5.4f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<Genesis_Whip>())
            {
                float ai4 = (Main.rand.NextFloat() - 0.5f) * ((float)Math.PI / 4f);
                Projectile.NewProjectile(source, player.Center, Vector2.Normalize(Main.MouseWorld - player.Center), type, damage, knockback, player.whoAmI, 0f, upgrades >= 2 ? 0 : ai4);
                return false;
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] > 0)
                return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}