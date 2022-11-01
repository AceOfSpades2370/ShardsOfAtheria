﻿using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ShardsOfAtheria
{
    [BackgroundColor(164, 153, 190)]
    [Label("Client Side")]
    public class ShardsConfigClientSide : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Mechanics")]
        [Label("[i:305] Mega Gem Core Gravitation")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles Gravitation buff from Mega Gem Core")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool megaGemCoreGrav;

        [Label("[i:583] Instant Soul Crystal Absorption")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the wait for absorbing Soul Crystals")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        public bool instantAbsorb;

        [Label("[i:149] View All entries")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the ability to view all Necronomicon entries\nregardless of whether or not the Soul Crystal is absorbed or not")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(false)]
        public bool entryView;

        [Label("[i:890] Biometal Equip Sound")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the sound played when Biometal is equipped")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool biometalSound;

        [Label("[i:3617] Sinful Armament dialogue")] // A label is the text displayed next to the option. This should usually be a short description of what it does.
        [Tooltip("Toggles the dialogue when using Sinful Armament")] // A tooltip is a description showed when you hover your mouse over the option. It can be used as a more in-depth explanation of the option.
        [DefaultValue(true)]
        public bool sinfulArmamentText;
    }
}