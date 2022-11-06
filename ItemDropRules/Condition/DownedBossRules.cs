﻿using ShardsOfAtheria.Players;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace ShardsOfAtheria.ItemDropRules.Conditions
{

    // Very simple drop condition: drop after Skeletron's defeat
    public class DownedSkeletron : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return NPC.downedBoss3;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops after Skeletron's defeat";
        }
    }

    // Very simple drop condition: drop after Golem's defeat
    public class DownedGolem : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return NPC.downedGolemBoss;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops after Golem's defeat";
        }
    }

    // Very simple drop condition: drop after Lunatic Cultist's defeat
    public class DownedLunaticCultist : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return NPC.downedAncientCultist;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops after Lunatic Cultist's defeat";
        }
    }
    // Very simple drop condition: drop after Moon Lord's defeat
    public class DownedMoonLord : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return NPC.downedMoonlord;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops after Moon Lord's defeat";
        }
    }
}