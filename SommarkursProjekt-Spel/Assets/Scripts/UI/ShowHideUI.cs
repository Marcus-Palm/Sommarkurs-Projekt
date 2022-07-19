using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace summerProject.UI
{

    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] KeyCode toggleKey = KeyCode.Escape;
        [SerializeField] GameObject uiContainer = null;

        private void Start()
        {
            uiContainer.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                ToggleUI();
            }
        }

        public void ToggleUI()
        {
            uiContainer.SetActive(!uiContainer.activeSelf);
        }
    }
}
