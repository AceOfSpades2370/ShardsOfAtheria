using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class WarriorChip : ClassChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            damageClass = DamageClass.Melee;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
        }
    }
}