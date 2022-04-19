/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Dr. Randy Fortier
 * 
 * This script is forperforming the fishing minigame
 */

using UnityEngine;

public class Fishing : MonoBehaviour {
    [Header("Game Objects")]
    [SerializeField] private Transform standingLocation;

    [Header("Mouse Settings")]
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    private GameManager manager;
    private GameObject player;

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
    }

    private void SetCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}
