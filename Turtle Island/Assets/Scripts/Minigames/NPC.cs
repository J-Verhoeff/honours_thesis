/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Randy Fortier
 * 
 * This script handles the dialogue between the player and a NPC
 * Uses the  Dialogue Editotr plugin
 */
using UnityEngine;
using DialogueEditor;

public class NPC : MonoBehaviour {
    [Header("General Settings")]
    [SerializeField] private GameManager manager; // The game manager object
    [SerializeField] private Interact playerInteract; // The interact script tied to the player
    [SerializeField] private GameObject player; // The player game oject

    [Header("NPC Settings")]
    [SerializeField] private NPCConversation conversation; // The dialogue object from the Dialogue Editor plugin
    [SerializeField] private string NPCPrompt = "Press Q to talk"; // Button prompt for when close to an NPC

    private void OnTriggerStay(Collider other) {
        // If player is close by, display the prompt
        if (other.CompareTag("Player")) {
            manager.setPrompt(NPCPrompt);
            // Check for interact button press, and then start the conversation
            if (playerInteract.interact && !ConversationManager.Instance.IsConversationActive) {
                manager.setPrompt("Press Q to talk");
                if (playerInteract.interact) {
                    ConversationManager.Instance.StartConversation(conversation);
                }
            }
        }
    }

    // Close a conversation if the player moves away from the NPC
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            manager.hidePrompt();
            if (ConversationManager.Instance.IsConversationActive) {
                ConversationManager.Instance.EndConversation();
            }
        }
    }

    // Callback functions for when a conversation starts and ends
    private void ConversationStart() {
        // lock camera and unlock mouse
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<StarterAssets.ThirdPersonController>().LockCameraPosition = true;
    }

    private void ConversationEnd() {
        // unlock camera and lock mouse
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<StarterAssets.ThirdPersonController>().LockCameraPosition = false;
    }

    private void OnEnable() {
        ConversationManager.OnConversationStarted += ConversationStart;
        ConversationManager.OnConversationEnded += ConversationEnd;
    }

    private void OnDisable() {
        ConversationManager.OnConversationStarted -= ConversationStart;
        ConversationManager.OnConversationEnded -= ConversationEnd;
    }
}
