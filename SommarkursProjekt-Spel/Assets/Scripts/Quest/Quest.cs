using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace summerProject.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Summer Project/Quests/Create New Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<string> objectives = new List<string>();

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Count;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objectives;
        }

        public bool HasObjective(string objective)
        {
            return objectives.Contains(objective);
        }
    }
}

