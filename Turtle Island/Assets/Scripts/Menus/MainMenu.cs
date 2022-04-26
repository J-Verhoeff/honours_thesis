/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Randy Fortier
 * 
 * This script handles main menu
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class MainMenu : MonoBehaviour {
    // Ui Elements on the menu
    [Header("UI Elements")]
    [SerializeField] private GameObject menuCanvas; // main menu buttons
    [SerializeField] private GameObject controlsCanvas; // controls display
    [SerializeField] private GameObject loadingCanvas; // loading screen
    [SerializeField] private Image progressBar; // loading screen progress bar

    [Header("Scene Management")]
    [SerializeField] private string mainScene = "Gameplay"; // main scene for game
    [SerializeField] private List<string> additionalScenes; //names of any additional scenes that need to be loaded
    [SerializeField] private float loadingTime = 1f; // loading time for testing load screen

    // list of async operations for loading scenes
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    private void Start() {
        menuCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
        loadingCanvas.SetActive(false);
    }

    // Load the games scenes and start the game
    public void StartGame() {
        menuCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
        scenesToLoad.Add(SceneManager.LoadSceneAsync(mainScene));
        for(int i = 0; i < additionalScenes.Count; i++) {
            scenesToLoad.Add(SceneManager.LoadSceneAsync(additionalScenes[i], LoadSceneMode.Additive));
        }
        StartCoroutine(LoadingScreen());
    }

    // Async function for the loading screen
    IEnumerator LoadingScreen() {
        float totalProgress = 0;
        // Fills the progress bar based on the progress of the load scene function
        for(int i = 0; i < scenesToLoad.Count; i++) {
            while(!scenesToLoad[i].isDone) {
                totalProgress += scenesToLoad[i].progress;
                progressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return new WaitForSeconds(loadingTime);
            }
        }
    }

    // Display the controls when the menu button is pressed
    public void Controls() {
        menuCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    // Go back to main menu when button is pressed
    public void Back() {
        menuCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
        loadingCanvas.SetActive(false);
    }

    // Exit the application
    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
