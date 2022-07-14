using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using summerProject.Dialogue;
using TMPro;
using UnityEngine.UI;
using System;

namespace summerProject.UI
{
    public class DialogueUI : MonoBehaviour
    {

        PlayerConversant playerConversant;
        [SerializeField]
        TextMeshProUGUI AIText;
        [SerializeField]
        Button nextButton;
        [SerializeField]
        GameObject AIResponce; 

        [SerializeField]
        Transform choiceRoot;
        [SerializeField]
        GameObject choicePrefab;
        [SerializeField]
        GameObject speakerName;
        [SerializeField]
        Button quitButton;
        

        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => playerConversant.Next());
            quitButton.onClick.AddListener(() => playerConversant.QuitDialogue());

            UpdateUI();
        }



        

        
        void UpdateUI()
        {
            gameObject.SetActive(playerConversant.isActive());
            if (!playerConversant.isActive())
            {
                return;
            }
           
            AIResponce.SetActive(!playerConversant.IsChoosing());

            choiceRoot.gameObject.SetActive(playerConversant.IsChoosing());

            ChangeSpeakerName(playerConversant.GetSpeakerName());

            if (playerConversant.IsChoosing())
            {
                BuildChoiceList();
                
            }
            else
            {
                AIText.text = playerConversant.GetText();
                nextButton.gameObject.SetActive(playerConversant.HasNext());
                //ChangeSpeakerName(playerConversant.GetSpeakerName());
            }

            
        }

        private void ChangeSpeakerName(string name)
        {
            TextMeshProUGUI speaker = speakerName.GetComponent<TextMeshProUGUI>();
            speaker.text = name;
        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetChoises())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);

                TextMeshProUGUI textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();

                textComp.text = choice.GetButtonLabel(); // use choise.GetText() for original design;

                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    playerConversant.SelectChoice(choice);
                    
                });
            }
        }
    }
}
