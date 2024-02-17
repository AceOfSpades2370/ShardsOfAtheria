using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class ThunderValkyrieMask : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.vanity = true;

            Item.rare = ItemDefaults.RarityBossMasks;
            Item.value = ItemDefaults.ValueBossMasks;
        }
    }
}
