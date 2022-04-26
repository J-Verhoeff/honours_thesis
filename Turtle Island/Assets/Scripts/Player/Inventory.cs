/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Randy Fortier
 * 
 * This Script handles player inventory for the crafting mini game
 */
using UnityEngine;
using System.Collections.Generic;
using DialogueEditor;

public class Inventory : MonoBehaviour {
    public Dictionary<string, int> itemsNeeded; // Dictionary of the required items for crafting and thier amounts
    public Dictionary<string, int> itemsObtained; // Dictionary for the currently obtatined items and the amount obtained

    private GameManager manager; // The game manager
    private bool isCrafting = false; // Flag to check if the player is currently crafting

    private void Awake() {
        // Create the dictionaries
        itemsNeeded = new Dictionary<string, int>();
        itemsObtained = new Dictionary<string, int>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        if(ConversationManager.Instance.IsConversationActive && isCrafting) {
            // Set the parameter for Dialogue Editor conversation for crafting
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

    // Add a new needed item to the inventory, set the amount obtatined to 0
    //  Have to add incremently since Dialogue Editor plugin only supports 1 
    //  parameter for event functions
    // (TO-DO: find a way set the amount with only one function call)
    public void AddNewItem(string itemName) {
        if (itemsNeeded.ContainsKey(itemName)) {
            itemsNeeded[itemName]++;
        } else {
            itemsNeeded.Add(itemName, 1);
            itemsObtained.Add(itemName, 0);
        }
        UpdateInventoryList();
    }

    // When a pickup is obtained, add the item to the inventory
    public void ObtainItem(string itemName) {
        if(itemsObtained.ContainsKey(itemName)) {
            itemsObtained[itemName]++;
        } else {
            itemsObtained.Add(itemName, 1);
        }
        UpdateInventoryList();
    }

    // Update the inventory UI element with the currently required items and amounts obtained
    private void UpdateInventoryList() {
        string text = "";
        foreach(var item in itemsNeeded) {
            text += item.Key + ": " + itemsObtained[item.Key] + "/" + item.Value + "\n";
        }
        manager.SetProgressText(text);
    }

    // Function to set the crafting flag
    public void StartCrafting() {
        isCrafting = true;
    }

    // Empty the dictionaries representing the players inventory
    public void EmptyInventory() {
        isCrafting = false;
        itemsNeeded.Clear();
        itemsObtained.Clear();
        UpdateInventoryList();
    }
}
