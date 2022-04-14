/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Dr. Randy Fortier
 * 
 * This script handles the dialogue between the player and a NPC
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueEditor;

public class NPC : MonoBehaviour {
    [Header("General Settings")]
    [SerializeField] private GameManager manager;
    [SerializeField] private Interact playerInteract;
    [SerializeField] private string activitySceneName = "";

    [Header("NPC Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private NPCConversation conversation;
    [SerializeField] private string NPCPrompt = "Press Q to talk";

    [Header("Chat Settings")]
    [SerializeField] private float chatDelay = 2f;
    
    private void OnTriggerStay(Collider other) {
        // If player is close by, display the prompt
        if (other.CompareTag("Player")) {
            manager.setPrompt(NPCPrompt);
            // Check for interact button press, and then start the conversation
            if(playerInteract.interact && !ConversationManager.Instance.IsConversationActive) {
                ConversationManager.Instance.StartConversation(conversation);
            }
        }
    }

    // Close a conversation if the player moves away from the NPC
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            manager.hidePrompt();
            if(ConversationManager.Instance.IsConversationActive) {
                ConversationManager.Instance.EndConversation();
            }
        }
    }

    // Callback functions for when a conversation starts and ends
    private void ConversationStart() {
        // lock camera and unlock nouse
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

    public void StartActivity() {
        StartCoroutine("LoadSceneAsync", activitySceneName);
    }

    public void DisplayActivityInstructions() {
        Debug.Log("Display How to");
    }

    public void CancelActivity() {
        //Debug.Log("Canceled Activity");
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<StarterAssets.ThirdPersonController>().LockCameraPosition = false;
        StartCoroutine("ChatDelay", chatDelay);
    }

    IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while(!asyncLoad.isDone) {
            yield return null;
        }
    }
}
