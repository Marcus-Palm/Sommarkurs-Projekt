using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace summerProject.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {

        [SerializeField]
        Dialogue testDialogue;

        Dialogue currentDialogue;
        DialogueNode currentNode = null;
        bool isChoosing = false;

        public event Action onConversationUpdated;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2);
            StartDialogue(testDialogue);
        }

        public void StartDialogue(Dialogue newDialogue)
        {
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            onConversationUpdated();

        }

        public void QuitDialogue()
        {
            currentDialogue = null;
            currentNode = null;
            isChoosing = false;
            onConversationUpdated();
        }



        public bool isActive()
        {
            return currentDialogue != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if(currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoises()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }

        internal string GetSpeakerName()
        {
            if (currentNode.IsPlayerSpeaking() || isChoosing)
            {
                return currentDialogue.GetPlayerName();
            }
            else
            {
                return currentDialogue.GetNPCName();
            }

        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            isChoosing = false;
            onConversationUpdated();
             /*
             *Next(); <-- this was a step to skip to next dialogbox without displaying what the player said. 
             * But in my game I added buton labels so the player can have their own share of the conversation.
             */
        }

        public void Next()
        {
            int numberOfPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if(numberOfPlayerResponses > 0)
            {
                isChoosing = true;
                onConversationUpdated();
                return;
            }


            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            currentNode = children[randomIndex];
            onConversationUpdated();
        }

        

        public bool HasNext()
        {
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
    }
}
