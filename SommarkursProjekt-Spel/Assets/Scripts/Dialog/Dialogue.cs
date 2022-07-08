using System;
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

        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
        private void Awake()
        {
            
            if(nodes.Count == 0)
            {
                DialogueNode rootNode = new DialogueNode();
                rootNode.nodeID = Guid.NewGuid().ToString();
            
                nodes.Add(rootNode);
            }
            OnValidate();
        }

#endif      

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.nodeID] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            
            foreach(string childID in parentNode.children)
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
            
        }

        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = new DialogueNode();
            newNode.nodeID = Guid.NewGuid().ToString();
            parent.children.Add(newNode.nodeID);
            nodes.Add(newNode);
            OnValidate();
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            nodes.Remove(nodeToDelete);
            OnValidate();
            RemoveDanglingChildren(nodeToDelete);
        }

        private void RemoveDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.children.Remove(nodeToDelete.nodeID);
            }
        }
    }
}


