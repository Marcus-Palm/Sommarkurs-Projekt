using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace summerProject.Quests
{
    
    public class QuestStatus 
    {
        Quest quest;
        List<string> compleatdeObjectives = new List<string>();

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompleatedCount()
        {
            return compleatdeObjectives.Count;
        }

        public bool IsObjectiveComplete(string objective)
        {
            return compleatdeObjectives.Contains(objective);
        }

        public void CompleteObjective(string objective)
        {
            if (quest.HasObjective(objective))
            {
                compleatdeObjectives.Add(objective);
            }
        }
    }
}
