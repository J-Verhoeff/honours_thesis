using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    [Header("NPC Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private List<string> dialogue;

    private Interact playerInteract;   
    private GameManager manager; 
    
    private int currentIndex = 0;
    private bool isChatting = false;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start() {
        playerInteract = player.GetComponent<Interact>();
    }

    private void Update() {
        if (isChatting && playerInteract.interact) {
            if (currentIndex < dialogue.Count) {
                currentIndex++;
                manager.updateChatbox(dialogue[currentIndex]);
            } else {
                manager.hideChatBox();
                isChatting = false;
                currentIndex = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player") && playerInteract.interact && !isChatting) {
            manager.displayChatbox(dialogue[currentIndex]);
            isChatting = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            currentIndex = 0;
            manager.hideChatBox();
            isChatting = false;
        }
    }
}
