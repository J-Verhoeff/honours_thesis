/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Randy Fortier
 * 
 * This script is for performing the fishing minigame
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour {
    [Header("Game Objects")]
    [SerializeField] private Transform standingLocation; // Location to move the player to when Entering the minigame
    [SerializeField] private Transform returnLocation; // Location to return player to when exiting
    [SerializeField] private List<Transform> waypoints; // List of waypoints for fish to travel between (TO-DO: Change to have multuple sets of waypoints)
    [SerializeField] private GameObject fishPrefab; // The fish prefab being used for the mini game

    [Header("Mouse Settings")]
    [SerializeField] private Texture2D cursorTexture; // Crosshair texture for spear fishing
    [SerializeField] private Vector2 hotSpot = Vector2.zero; // Mouse hotspot
    [SerializeField] private float spearRange = 20f; // Range of the spear
    [SerializeField] private LayerMask fishLayer; // The layer the fish is on

    [Header("Minigame settings")]
    [SerializeField] private int amountNeaded = 3; // The amount of fish that need to be caught

    // Spawner settings
    private int currently_spawned = 0;
    private int max_spawnable = 1;
    private int currentAmount = 0;

    private GameManager manager; // Game manager
    private GameObject player; // The player
    private bool canInput = true; // Input flag to delay button input

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() {
        // Set the players location
        player.transform.position = standingLocation.position;
        player.transform.rotation = standingLocation.rotation;
        // Switch to the first person camera mode
        manager.FirstPersonCamera();
        manager.LockPlayerPosition();
        // Enable the spear
        manager.EnableSpear();
        SetCursor();        
        SetProgress();
        manager.SetHelpText("Press Q to exit");
        HidePlayer(); // hiding the player model to test minigame
    }

    private void Update() {
        if(currently_spawned < max_spawnable && currentAmount < amountNeaded) {
            // Spawn a fish if conditions are met
            SpawnFish();            
        }

        // Code to cheat for demo
        if(player.GetComponent<Interact>().interact && currently_spawned != 0 && canInput) {
            CatchFish();
            Destroy(GameObject.FindGameObjectWithTag("Fish"));
            StartCoroutine(WaitForSpawn());
        }
    }

    // Spawn a fish
    private void SpawnFish() {
        GameObject fish = Instantiate(fishPrefab, waypoints[0].position, waypoints[0].rotation, transform);
        fish.GetComponent<Fish>().waypoints = waypoints;
        currently_spawned++;
    }

    private void SetCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    // Function to hide player model for testing
    private void HidePlayer() {
        player.transform.Find("Geometry").gameObject.SetActive(false);
    }
    // Function to show player model for testing
    private void ShowPlayer() {
        player.transform.Find("Geometry").gameObject.SetActive(true);
    }

    // set the progress ui element
    private void SetProgress() {
        manager.SetProgressText(currentAmount + "/" + amountNeaded);
    }

    // Get the fire trigger from the player 
    public bool GetAction() {
        return player.GetComponent<Interact>().fire;
    }

    // Catch a fish
    public void CatchFish() {
        // despawn the fish and update the progress
        currently_spawned--;
        currentAmount++;
        SetProgress();
    }

    // Wait to spawn a fish after catching one
    IEnumerator WaitForSpawn() {
        canInput = false;
        yield return new WaitForSeconds(2);
        canInput = true;
    }
}
