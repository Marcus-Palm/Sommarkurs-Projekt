using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace summerProject.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Summer Project/Quests/Create New Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] string[] objectives;

        public string GetTitle()
        {
            return name;
        }

        public int GetObjectiveCount()
        {
            return objectives.Length;
        }
    }
}

