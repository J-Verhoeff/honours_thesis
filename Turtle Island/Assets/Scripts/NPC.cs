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

    [Header("Chat Settings")]
    [SerializeField] private float chatDelay = 2f;
    [SerializeField] NPCConversation conversation;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            manager.setPrompt("Press Q to talk");
            if (playerInteract.interact) {
                // Debug.Log("Trigger");
                Cursor.lockState = CursorLockMode.None;
                player.GetComponent<StarterAssets.ThirdPersonController>().LockCameraPosition = true;
                ConversationManager.Instance.StartConversation(conversation);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            ConversationManager.Instance.EndConversation();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    public void StartActivity() {
        StartCoroutine("LoadSceneAsync", activitySceneName);
    }

    public void DisplayActivityInstructions() {
        Debug.Log("Display How to");
    }

    public void CancelActivity() {
        Debug.Log("Canceled Activity");
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
