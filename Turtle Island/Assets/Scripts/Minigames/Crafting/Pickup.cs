/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Randy Fortier
 * 
 * This Script handles picking up an item for the 
 * crafting mini game
 */
using UnityEngine;

public class Pickup : MonoBehaviour {
    [Header("Game Objects")]
    [SerializeField] private Inventory inventory; // The inventory object attached to the player

    [Header("Item Settings")]
    [SerializeField] private string label; // The label of the pickup, used for UI display

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            if(inventory.itemsNeeded.ContainsKey(label)) {
                // Add object to the players inventory and destroy the object
                inventory.ObtainItem(label);
                Destroy(gameObject);
            }
        }
    }
}
