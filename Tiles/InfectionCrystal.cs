﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SagesMania.Items.Placeable;

namespace SagesMania.Tiles
{
    public class InfectionCrystal : ModTile
    {
        public override void SetDefaults()
        {
            TileID.Sets.Ore[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileValue[Type] = 285;

            drop = ModContent.ItemType<CrystalInfection>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Infection Crystal");
            AddMapEntry(new Color(255, 140, 0), name);

            dustType = DustID.Honey;
            minPick = 100;
            soundType = SoundID.Tink;
            soundStyle = 1;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = .5f;
            b = 0f;
        }
    }
}