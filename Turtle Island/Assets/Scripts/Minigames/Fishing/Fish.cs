/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Randy Fortier
 * 
 * This Script handles movement of the fish
 */
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // move speed of the fish

   private int currentIndex = 0; // current index of the waypoints
   public List<Transform> waypoints; // List of waypoints to move between

    private Fishing manager; // The fishing minigame manager

    private void Awake() {
        manager = GameObject.Find("FishingManager").GetComponent<Fishing>();
    }


    private void Update() {
        // Move between the different waypoints
        if (waypoints.Count > 0) {
            if (transform.position == waypoints[currentIndex].position) {
                currentIndex++;
                if (currentIndex >= waypoints.Count) {
                    currentIndex = 0;
                }
            }
            MoveToWayPoint();
        }
    }

    // Function to move towards the current waypoint in the list
    private void MoveToWayPoint() {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentIndex].position, moveSpeed * Time.deltaTime);
        transform.LookAt(waypoints[currentIndex]);
        transform.Rotate(0f, 90f, 0f);
    }

    private void OnMouseOver() {
        Debug.Log("Here");
        if(manager.GetAction()) {
            manager.CatchFish();
            Destroy(gameObject);
        }
    }
}
