/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Randy Fortier
 * 
 * This script handles Pause Menu
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject uiElements; // ui elements for the pause menu
    [SerializeField] private GameObject player; // the player game object

    private bool paused = false;

    private void Start() {
        uiElements.SetActive(false);
    }

    private void Update() {
        if(player.GetComponent<Interact>().pause && !paused) {
            uiElements.SetActive(true);
            PauseGame();
        }
    }

    // Function to pause the game
    public void PauseGame() {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        paused = true;
    }

    // Resume game from pause state
    public void ResumeGame() {
        Time.timeScale = 1f;
        Debug.Log("here");
        Cursor.lockState = CursorLockMode.Locked;
        uiElements.SetActive(false);
        paused = false;
    }

    // Function to exit and return to main menu
    public void ExitToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
