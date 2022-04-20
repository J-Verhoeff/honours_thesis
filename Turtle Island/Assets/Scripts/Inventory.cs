/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Dr. Randy Fortier
 * 
 * This Script handles player inventory for the crafting mini game
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DialogueEditor;

public class Inventory : MonoBehaviour {
    [SerializeField] private NPCConversation conversation;

    public Dictionary<string, int> itemsNeeded;
    public Dictionary<string, int> itemsObtained;

    private GameManager manager;
    private bool isCrafting = false;

    private void Awake() {
        itemsNeeded = new Dictionary<string, int>();
        itemsObtained = new Dictionary<string, int>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        if(ConversationManager.Instance.IsConversationActive && isCrafting) {
            ConversationManager.Instance.SetBool("IsCrafting", true);

            foreach(var item in itemsNeeded) {
                if(item.Value == itemsObtained[item.Key]) {
                    ConversationManager.Instance.SetBool("HasItems", true);
                } else {
                    ConversationManager.Instance.SetBool("HasItems", false);
                    break;
                }
            }
        }
    }

    public void AddNewItem(string itemName) {
        if (itemsNeeded.ContainsKey(itemName)) {
            itemsNeeded[itemName]++;
        } else {
            itemsNeeded.Add(itemName, 1);
            itemsObtained.Add(itemName, 0);
        }
        UpdateInventoryList();
    }

    public void ObtainItem(string itemName) {
        if(itemsObtained.ContainsKey(itemName)) {
            itemsObtained[itemName]++;
        } else {
            itemsObtained.Add(itemName, 1);
        }
        UpdateInventoryList();
    }

    private void UpdateInventoryList() {
        string text = "";
        foreach(var item in itemsNeeded) {
            text += item.Key + ": " + itemsObtained[item.Key] + "/" + item.Value + "\n";
        }
        manager.SetProgressText(text);
    }

    public void StartCrafting() {
        isCrafting = true;
    }

    public void EmptyInventpory() {
        isCrafting = false;
        itemsNeeded.Clear();
        itemsObtained.Clear();
        UpdateInventoryList();
    }
}
