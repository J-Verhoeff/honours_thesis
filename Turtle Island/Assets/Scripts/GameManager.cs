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
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {
    [Header("Game Settings")]
    [SerializeField] private float loadTime = 3f;
    [SerializeField] private float cameraDistance = 16f;

    // UI elements
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI prompt;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image progressBar;

    [Header("Game Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private GameObject spear;

    private float playerMoveSpeed;

    private void Awake() {
        playerMoveSpeed = player.GetComponent<StarterAssets.ThirdPersonController>().MoveSpeed;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        hidePrompt();
        DisableSpear();
        loadingScreen.SetActive(false);
    }

    public void setPrompt(string textToDisplay) {
        prompt.text = textToDisplay;
    }

    // Hide the prompt
    public void hidePrompt() {
        prompt.text = "";
    }

    // Load scene
    private AsyncOperation LoadScene(string sceneName) {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded) {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
        return null;
    }

    // Unload a scene
    private AsyncOperation UnloadScene(string sceneName) {
        if (SceneManager.GetSceneByName(sceneName).isLoaded) {
            return SceneManager.UnloadSceneAsync(sceneName);
        }
        return null;
    }

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

    // Change Scene
    public void ChangeScene(string currentScene, string newScene) {
        List<AsyncOperation> scenes = new List<AsyncOperation>();
        scenes.Add(UnloadScene(currentScene));
        scenes.Add(LoadScene(newScene));
        StartCoroutine(LoadingScreen(scenes));
    }

    // Load minigame
    public void LoadMiniGame(string newScene) {
        ChangeScene("Island", newScene);
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


}
