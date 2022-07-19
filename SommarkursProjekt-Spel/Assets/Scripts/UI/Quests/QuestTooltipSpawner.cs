using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using summerProject.UI.toolTips;
using summerProject.Quests;

namespace summerProject.UI.Quests
{

    public class QuestTooltipSpawner : MyTooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
            Quest quest = GetComponent<QuestItemUI>().GetQuest();
            tooltip.GetComponent<QuestTooltipUI>().Setup(quest); 
        
        }

   
    }
}
