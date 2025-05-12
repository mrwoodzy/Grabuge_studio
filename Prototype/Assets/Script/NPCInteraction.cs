using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionPromptUI; // L'UI contextuelle (ex : "Appuyez sur Entrée pour parler")
    private bool isPlayerInRange = false;

    void Start()
    {
        interactionPromptUI.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
            {
                TriggerDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPromptUI.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPromptUI.SetActive(false);
            isPlayerInRange = false;
        }
    }

    private void TriggerDialogue()
    {
        Debug.Log("Dialogue Started");
        // Tu peux lancer ton système de dialogue ici
    }
}
