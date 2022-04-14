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
    private GameManager manager;
    private GameObject player;

    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
