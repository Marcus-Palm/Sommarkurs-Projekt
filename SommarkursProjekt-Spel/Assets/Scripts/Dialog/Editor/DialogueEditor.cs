using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace summerProject.Dialogue.Editor
{

    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;

        [MenuItem("Window/Summer Project/Dialogue Editor")]
        public static void ShowEditorWindow()
        {

            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");

        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue; 
            if(dialogue != null)
            {
                ShowEditorWindow(); 
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            Dialogue dialogue = Selection.activeObject as Dialogue;

            if(dialogue != null)
            {
                selectedDialogue = dialogue;
                Repaint();
            }
            
        }

        private void OnGUI()
        {
            if (selectedDialogue != null)
            {
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    EditorGUILayout.LabelField(node.text);
                }
            
            }
            else
            {
                EditorGUILayout.LabelField("Dialogue not selected");
            }
            
            
        }
    }
}

