/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Dr. Randy Fortier
 * 
 * This script is the game manager to handle general game logic
 */
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {
    [Header("Game Settings")]
    [SerializeField] private float loadTime = 3f;

    // UI elements
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI prompt; // UI element for the player prompt


    private void Start() {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor on startup
        hidePrompt();
    }

    // Set the prompt to a passed string
    public void setPrompt(string textToDisplay) {
        prompt.text = textToDisplay;
    }

    // Hide the prompt
    public void hidePrompt() {
        prompt.text = "";
    }

    // Load scene
    public void loadScene(string sceneName) {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded) {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    // Unload a scene
    public void UnloadScene(string sceneName) {
        if(SceneManager.GetSceneByName(sceneName).isLoaded) {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
