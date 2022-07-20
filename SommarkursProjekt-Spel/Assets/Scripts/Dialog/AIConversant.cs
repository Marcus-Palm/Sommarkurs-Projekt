using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace summerProject.Dialogue
{

    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue;
        

        [SerializeField] GameObject conversationPrompt;

        GameObject player;

        bool inConverastionRange = false;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (inConverastionRange && Input.GetKeyDown(KeyCode.E))
            {
                player.gameObject.GetComponent<PlayerConversant>().StartDialogue(this,dialogue);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player" && dialogue != null)
            {
                inConverastionRange = true;

                conversationPrompt.SetActive(true);

                player = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Player" && conversationPrompt.activeInHierarchy)
            {
                inConverastionRange = false;

                conversationPrompt.SetActive(false);

            }
        }
    }
}
