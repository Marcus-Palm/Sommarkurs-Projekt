using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace summerProject.Dialogue
{

    [System.Serializable]
    public class DialogueNode 
    {
        public string nodeID;

        public string text;

        public string[] children;
    }
}
