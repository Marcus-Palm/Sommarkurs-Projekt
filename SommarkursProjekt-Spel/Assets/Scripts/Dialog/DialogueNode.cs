using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace summerProject.Dialogue
{

    
    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        private bool isPlayerSpeaking = false;
        [SerializeField]
        private string text;
        [SerializeField]
        private string buttonLabel;
        [SerializeField]
        private List<string> children = new List<string>();
        [SerializeField]
        private Rect rect = new Rect( 0, 0, 200, 150);
        [SerializeField]
        string onEnterAction;
        [SerializeField]
        string onExitAction; // could make this an array if I want to trigger multiple actions.

        public Rect GetRect()
        {
            return rect;
        }

        public string GetText()
        {
            return text;
        }

        public string GetButtonLabel()
        {
            return buttonLabel;
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }

        public string GetOnExitAction()
        {
            return onExitAction;
        }

#if UNITY_EDITOR
        public void SetPos(Vector2 newPos)
        {
            Undo.RecordObject(this, "Move Dialog Node");
            rect.position = newPos;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);

            }
        }

        public void SetButtonText(string newText)
        {
            if (newText != buttonLabel)
            {
                Undo.RecordObject(this, "Update Dialogue Button Text");
                buttonLabel = newText;
                EditorUtility.SetDirty(this);

            }
        }


        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this); 
        }

        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }


#endif
    }
}
