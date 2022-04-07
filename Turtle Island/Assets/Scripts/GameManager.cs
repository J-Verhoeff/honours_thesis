using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    [Header("Game Settings")]
    [SerializeField] private float loadTime = 3f;

    // UI elements
    [Header("UI Elements")]
    [SerializeField] private GameObject chatBox;
    [SerializeField] private TextMeshProUGUI chat;
    [SerializeField] private GameObject buttons;
    [SerializeField] private TextMeshProUGUI prompt;

    public GameObject currentNPC = null;


    private void Start() {
        buttons.SetActive(false);
        chatBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        hidePrompt();
    }

    public void displayChatbox(string textToDisplay) {
        chatBox.SetActive(true);
        chat.text = textToDisplay;
    }

    public void hideChatBox() {
        chat.text = "";
        chatBox.SetActive(false);
        currentNPC = null;
    }

    public void updateChatbox(string textToDisplay) {
        if (chatBox.activeInHierarchy) {
            chat.text = textToDisplay;
        }
    }

    public void hideButtons() {
        buttons.SetActive(false);
    }

    public void showButtons() {
        buttons.SetActive(true);
    }

    public void setPrompt(string textToDisplay) {
        prompt.text = textToDisplay;
    }

    public void hidePrompt() {
        prompt.text = "";
    }

    public void Yes() {
        currentNPC.GetComponent<NPC>().StartActivity();
    }

    public void No() {
        currentNPC.GetComponent<NPC>().CancelActivity();
    }

    public void HowTo() {
        currentNPC.GetComponent<NPC>().DisplayActivityInstructions();
    }
}
