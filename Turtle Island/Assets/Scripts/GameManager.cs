/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Randy Fortier
 * 
 * This script is the game manager to handle general game logic
 */
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {
    [Header("Game Settings")]
    [SerializeField] private float loadTime = 3f; // Load time used for testing the load screen
    [SerializeField] private float cameraDistance = 16f; // Set the camera distance for third person camera

    // UI elements
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI prompt; // The ui element for the button prompt
    [SerializeField] private GameObject loadingScreen; // Ui canvas for the loading screen
    [SerializeField] private Image progressBar; // The image for the progress bar
    [SerializeField] private TextMeshProUGUI progressText; // The text for showing progress in a minigame
    [SerializeField] private TextMeshProUGUI helpText; // Ui element gor the Help indicator

    [Header("Game Objects")]
    [SerializeField] private GameObject player; // Game object representing the player
    [SerializeField] private CinemachineVirtualCamera playerCamera; // The cinemachine camera
    [SerializeField] private GameObject spear; // Game object representing the spear for fishing minigame
    [SerializeField] private GameObject pickups; // game object for pickups for crafting minigame 

    private float playerMoveSpeed; // player movespeed variable used for string the players movespeed

    private void Awake() {
        // Get the players movespeed set in the character controller
        playerMoveSpeed = player.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked; // Lock the mouse
        hidePrompt();
        DisableSpear();
        loadingScreen.SetActive(false);
        SetHelpText("");
        SetProgressText("");
        HidePickups();
    }

    // Set the text of the prompt UI element
    public void setPrompt(string textToDisplay) {
        prompt.text = textToDisplay;
    }

    // Hide the prompt UI element
    public void hidePrompt() {
        prompt.text = "";
    }

    // Load a scene given a scene name
    private AsyncOperation LoadScene(string sceneName) {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded) {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        return null;
    }

    // Unload a scene given a scene name
    private AsyncOperation UnloadScene(string sceneName) {
        if (SceneManager.GetSceneByName(sceneName).isLoaded) {
            return SceneManager.UnloadSceneAsync(sceneName);
        }
        return null;
    }

    // Loading screen used when switching between different scenes
    IEnumerator LoadingScreen(List<AsyncOperation> scenes) {
        loadingScreen.SetActive(true);
        float totalProgress = 0;
        for (int i = 0; i < scenes.Count; i++) {
            while (!scenes[i].isDone) {
                totalProgress += scenes[i].progress;
                progressBar.fillAmount = totalProgress / scenes.Count;
                yield return new WaitForSeconds(loadTime);
            }
        }
        loadingScreen.SetActive(false);
    }

    // Change Scene from one scene to another, given a current scene name and a new scene name
    public void ChangeScene(string currentScene, string newScene) {
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        scenes.Add(UnloadScene(currentScene));
        scenes.Add(LoadScene(newScene));
        StartCoroutine(LoadingScreen(scenes));
    }

    // Load a minigame from the main Island scene
    public void LoadMiniGame(string newScene) {
        ChangeScene("Island", newScene);
    }

    // Unload a mini game and return to the main island
    public void UnloadMinigame(string currentScene) {
        ChangeScene(currentScene, "Island");
    }

    // set the camera to third person
    public void ThirdPersonCamera() {
        // set the player camera distance
        CinemachineComponentBase componentBase = playerCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is Cinemachine3rdPersonFollow) {
            (componentBase as Cinemachine3rdPersonFollow).CameraDistance = cameraDistance;
        }
    }

    // Set the player camera to first person
    public void FirstPersonCamera() {
        CinemachineComponentBase componentBase = playerCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is Cinemachine3rdPersonFollow) {
            (componentBase as Cinemachine3rdPersonFollow).CameraDistance = -0.5f;
        }
    }

    // Lock player in position
    public void LockPlayerPosition() {
        player.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed = 0;
    }

    // Unlock the players position
    public void UnLockPlayerPosition() {
        player.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed = playerMoveSpeed;
    }

    // Enable Spear game object
    public void EnableSpear() {
        spear.SetActive(true);
    }

    // Disable Spear game object
    public void DisableSpear() {
        spear.SetActive(false);
    }

    // set the progress text
    public void SetProgressText(string text) {
        progressText.text = text;
    }

    // set help Text
    public void SetHelpText(string text) {
        helpText.text = text;
    }

    // Show and hide pickups
    public void ShowPickups() {
        pickups.SetActive(true);
    }
    public void HidePickups() {
        pickups.SetActive(false);
    }
}
