using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour {
    [Header("General Settings")]
    [SerializeField] private GameManager manager;
    [SerializeField] private Interact playerInteract;
    [SerializeField] private string activitySceneName = "";

    [Header("NPC Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private List<string> dialogue;
    [SerializeField] private bool playerSelection;

    [Header("Chat Settings")]
    [SerializeField] private float chatDelay = 2f;
    
    private int currentIndex = 0;
    private bool isChatting = false;
    private bool chatPause = false;   

    private void Update() {
        if (isChatting && playerInteract.interact && !chatPause) {
            if (currentIndex < dialogue.Count - 1) {
                currentIndex++;
                manager.updateChatbox(dialogue[currentIndex]);
                //Debug.Log("Update");
                StartCoroutine("ChatDelay", chatDelay);
            } else {
                StartCoroutine("PlayerChoice", chatDelay);
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            manager.setPrompt("Press Q to talk");
            if (playerInteract.interact && !isChatting && !chatPause) {
                manager.hideButtons();
                manager.displayChatbox(dialogue[currentIndex]);
                manager.currentNPC = gameObject;
                isChatting = true;
                //Debug.Log("Trigger");
                StartCoroutine("ChatDelay", chatDelay);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            currentIndex = 0;            
            manager.hideChatBox();
            isChatting = false;
            manager.hidePrompt();
        }
    }

    IEnumerator ChatDelay(float delay) {
        chatPause = true;
        //Debug.Log("Delay");
        yield return new WaitForSeconds(delay);
        chatPause = false;
    }

    IEnumerator PlayerChoice(float delay) {
        currentIndex = 0;
        chatPause = true;
        if(!playerSelection) {
            manager.hideChatBox();
            isChatting = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<StarterAssets.ThirdPersonController>().LockCameraPosition = true;
            manager.showButtons();
        }

        yield return new WaitForSeconds(delay);
        chatPause = false;
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
        manager.hideChatBox();
        isChatting = false;
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
