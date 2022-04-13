using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    [Header("Game Settings")]
    [SerializeField] private float loadTime = 3f;

    // UI elements
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI prompt;

    public GameObject currentNPC = null;


    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        hidePrompt();
    }

    public void setPrompt(string textToDisplay) {
        prompt.text = textToDisplay;
    }

    public void hidePrompt() {
        prompt.text = "";
    }
}
