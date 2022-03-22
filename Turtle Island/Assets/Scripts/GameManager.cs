using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    // Private Game objects
    // UI elements
    [SerializeField] private GameObject chatBox;
    [SerializeField] private TextMeshProUGUI chat;

    private void Start() {
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
    }

    public void updateChatbox(string textToDisplay) {
        if (chatBox.activeInHierarchy) {
            chat.text = textToDisplay;
        }
    }
}
