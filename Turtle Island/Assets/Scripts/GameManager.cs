using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    // Private Game objects
    // UI elements
    [Header("UI Elements")]
    [SerializeField] private GameObject chatBox;
    [SerializeField] private TextMeshProUGUI chat;
    [SerializeField] private GameObject buttons;

    public GameObject currentNPC {
        get {
            return currentNPC;
        }
        set {
            currentNPC = value;
        }
    }    

    private void Start() {
        buttons.SetActive(false);
        chatBox.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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
