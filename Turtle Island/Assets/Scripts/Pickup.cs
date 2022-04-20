/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Dr. Randy Fortier
 * 
 * This Script handles picking up an item for the 
 * crafting mini game
 */
using UnityEngine;

public class Pickup : MonoBehaviour {
    [Header("Game Objects")]
    [SerializeField] private Inventory inventory;

    [Header("Item Settings")]
    [SerializeField] private string label;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            if(inventory.itemsNeeded.ContainsKey(label)) {
                inventory.ObtainItem(label);
                Destroy(gameObject);
            }
        }
    }
}
