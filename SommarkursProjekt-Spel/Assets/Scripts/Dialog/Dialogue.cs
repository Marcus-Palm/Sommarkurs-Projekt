using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace summerProject.Dialogue
{
    [CreateAssetMenu(fileName ="NewDialogue", menuName ="Summer Project/Dialouge/Create New Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();

#if UNITY_EDITOR
        private void Awake()
        {
            if(nodes.Count == 0)
            {
                 
                nodes.Add(new DialogueNode());
            }
        }

#endif      

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }
    }
}


