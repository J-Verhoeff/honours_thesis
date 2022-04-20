/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Dr. Randy Fortier
 * 
 * This script is forperforming the fishing minigame
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour {
    [Header("Game Objects")]
    [SerializeField] private Transform standingLocation;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private GameObject fishPrefab;

    [Header("Mouse Settings")]
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private float spearRange = 20f;
    [SerializeField] private LayerMask fishLayer;

    [Header("Minigame settings")]
    [SerializeField] private int amountNeaded = 3;

    // Spawner settings
    private int currently_spawned = 0;
    private int max_spawnable = 1;
    private int currentAmount = 0;

    private GameManager manager;
    private GameObject player;
    private bool canInput = true;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() {
        player.transform.position = standingLocation.position;
        player.transform.rotation = standingLocation.rotation;
        manager.FirstPersonCamera();
        manager.LockPlayerPosition();
        manager.EnableSpear();
        SetCursor();
        HidePlayer();
        SetProgress();
        manager.SetHelpText("Press Q to exit");
    }

    private void Update() {
        if(currently_spawned < max_spawnable && currentAmount < amountNeaded) {
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

    // set progress
    private void SetProgress() {
        manager.SetProgressText(currentAmount + "/" + amountNeaded);
    }

    public bool GetAction() {
        return player.GetComponent<Interact>().fire;
    }

    public void CatchFish() {
        currently_spawned--;
        currentAmount++;
        SetProgress();
    }

    IEnumerator WaitForSpawn() {
        canInput = false;
        yield return new WaitForSeconds(2);
        canInput = true;
    }
}
