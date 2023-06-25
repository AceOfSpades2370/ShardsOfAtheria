using Microsoft.Xna.Framework;
using ShardsOfAtheria.NPCs.Boss.Elizabeth;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BossSummons
{
    public class AncientMedalion : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; // This helps sort inventory know that this is a boss summoning Item.

            // This is set to true for all NPCs that can be summoned via an Item (calling NPC.SpawnOnPlayer). If this is for a modded boss,
            // write this in the bosses file instead
            NPCID.Sets.MPAllowedEnemies[ModContent.NPCType<Death>()] = true;

            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            if (!SoA.ServerConfig.nonConsumeBoss)
            {
                Item.consumable = true;
            }
            Item.maxStack = 9999;
            Item.value = 50000;

            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;

            Item.rare = ItemRarityID.Blue;
        }

        // We use the CanUseItem hook to prevent a Player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return player.ZoneOverworldHeight && !NPC.AnyNPCs(ModContent.NPCType<Death>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (Main.dayTime)
                {
                    string text = "Please, try again later at night.";
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.White);
                    }
                    else
                    {
                        Main.NewText(text);
                    }
                    return true;
                }

                // If the Player using the item is the client
                // (explicitely excluded serverside here)
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<Death>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the Player is not in multiPlayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    // If the Player is in multiPlayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }
    }
}