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
        [NonSerialized]
        GUIStyle nodeStyle;
        [NonSerialized]
        GUIStyle playerNodeStyle;
        [NonSerialized]
        DialogueNode draggingNode = null;
        [NonSerialized]
        Vector2 draggingOffset;
        [NonSerialized]
        DialogueNode creatingNode = null;
        [NonSerialized]
        DialogueNode removingNode = null;

        [NonSerialized]
        DialogueNode linkingParentNode = null;

        Vector2 scrollPos;

        [NonSerialized]
        bool draggingCanvas = false;
        [NonSerialized]
        Vector2 draggingCanvasOffset;

        const float canvasSize = 5000;
        const float backgroundSize = 50;


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
            nodeStyle.normal.background = EditorGUIUtility.Load("Assets/Scripts/Dialog/Editor/Resources/blueBox.png") as Texture2D;
            nodeStyle.padding = new RectOffset(15, 15, 10, 10);

            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("Assets/Scripts/Dialog/Editor/Resources/orangeBox.png") as Texture2D;
            playerNodeStyle.padding = new RectOffset(15, 15, 10, 10);

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

                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTex = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize); 
                GUI.DrawTextureWithTexCoords(canvas, backgroundTex, texCoords); 

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    
                    DrawConnections(node);
                }

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                    
                }

                EditorGUILayout.EndScrollView();

                if(creatingNode != null)
                {
                    
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (removingNode != null)
                {
                   
                    selectedDialogue.DeleteNode(removingNode);
                    removingNode = null;
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
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPos);
                if(draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPos;
                    Selection.activeObject = selectedDialogue;
                }



            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                
                draggingNode.SetPos(Event.current.mousePosition + draggingOffset);

                GUI.changed = true;

            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPos = draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if(Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                draggingCanvas = false;
            }
        }


        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.IsPlayerSpeaking())
            {
                style = playerNodeStyle;
            }

            GUILayout.BeginArea(node.GetRect(), style);

            EditorGUILayout.LabelField("Text",EditorStyles.whiteBoldLabel);
            node.SetText(EditorGUILayout.TextField(node.GetText()));

            if (node.IsPlayerSpeaking())
            {
                EditorGUILayout.LabelField("Button Text", EditorStyles.whiteBoldLabel);
                node.SetButtonText(EditorGUILayout.TextField(node.GetButtonLabel()));
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+"))
            {
                creatingNode = node;

            }
            DrawLinkButton(node);
            if (GUILayout.Button("X"))
            {
                removingNode = node;

            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinkButton(DialogueNode node)
        {
            if (linkingParentNode == null)
            {

                if (GUILayout.Button("Link"))
                {
                    linkingParentNode = node;
                }

            }
            else if(linkingParentNode == node)
            {
                if (GUILayout.Button("Cancel"))
                {
                    linkingParentNode = null;
                }
            }
            else if (linkingParentNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("Unlink"))
                {
                   
                    linkingParentNode.RemoveChild(node.name);
                    linkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("Child"))
                {
                    
                    linkingParentNode.AddChild(node.name);
                    linkingParentNode = null;
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPos = new Vector2(node.GetRect().xMax, node.GetRect().center.y);

            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPos = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPos - startPos;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier(startPos, endPos, startPos + controlPointOffset, endPos - controlPointOffset, Color.white, null, 4f);
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach(DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.GetRect().Contains(point))
                {
                    foundNode = node;
                }
                
            }
            return foundNode;
        }
    }
}

