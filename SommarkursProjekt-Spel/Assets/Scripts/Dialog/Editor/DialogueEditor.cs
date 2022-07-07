using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

namespace summerProject.Dialogue.Editor
{

    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;
        GUIStyle nodeStyle;
         
        DialogueNode draggingNode = null;

        Vector2 draggingOffset;

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

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("Packages/com.unity.2d.spriteshape/Editor/ObjectMenuCreation/DefaultAssets/Textures/Sprite Shape Corner.png") as Texture2D;
            nodeStyle.padding = new RectOffset(15, 15, 10, 10);
            
            //nodeStyle.border = new RectOffset(5, 5, 5, 5);
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
                ProcessEvents();
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    OnGUINode(node);

                }

            }
            else
            {
                EditorGUILayout.LabelField("Dialogue not selected");
            }

            
            
            
        }

        private void ProcessEvents()
        {
            if(Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition);
                if(draggingNode != null)
                {
                    draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedDialogue, "Move Dialog Node");
                draggingNode.rect.position = Event.current.mousePosition + draggingOffset;
                GUI.changed = true;

            }
            else if(Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
        }


        private void OnGUINode(DialogueNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Node:",EditorStyles.whiteBoldLabel);
            string newText = EditorGUILayout.TextField(node.text);
            string newNodeID = EditorGUILayout.TextField(node.nodeID);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedDialogue, "Update Dialogue Text");
                node.text = newText;
                node.nodeID = newNodeID;
            }
            GUILayout.EndArea();
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach(DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.rect.Contains(point))
                {
                    foundNode = node;
                }
                
            }
            return foundNode;
        }
    }
}

