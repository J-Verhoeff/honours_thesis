/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Dr. Randy Fortier
 * 
 * This Script handles movement of the fish
 */
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

   private int currentIndex = 0;
   public List<Transform> waypoints;

    private Fishing manager;

    private void Awake() {
        manager = GameObject.Find("FishingManager").GetComponent<Fishing>();
    }


    private void Update() {
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
