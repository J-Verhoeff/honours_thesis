/**
 * Turtle Island
 * Ontario Tech Honours Thesis 2022
 * Author: Joshua Verhoeff
 * Supervisor: Dr. Randy Fortier
 * 
 * This script handles Interaction between the player and NPC's using
 * the Dialogue Editor asset pack and the new Unity input system
 */
using UnityEngine;
using UnityEngine.InputSystem;
using DialogueEditor;

public class Interact : MonoBehaviour {
    // Made public to view in editor
    [Header("Interact Values")]
    public bool interact; // Interact key is pressed
    public bool convoNext; // Conversation next for controller input
    public bool convoPrev; // Conversation previous for controller input
    public bool fire; // Fire key used for minigames

    // Functions to get input for the interact button
    public void InteractInput(bool newInteractState) {
        interact = newInteractState;
    }
    public void OnInteract(InputValue value) {
        InteractInput(value.isPressed);
    }

    // Functions to handle UI interaction using a gamepad
    // Get the conversation next input
    public void ConvoNextInput(bool newConvoNextState) {
        convoNext = newConvoNextState;
    }
    public void OnConvoNext(InputValue value) {
        ConvoNextInput(value.isPressed);
    }
    // get the conversation previous input
    public void ConvoPrevInput(bool newConvoPrevState) {
        convoPrev = newConvoPrevState;
    }
    public void OnConvoPrev(InputValue value) {
        ConvoPrevInput(value.isPressed);
    }

    // Functions for trigering the fire button
    public void FireInput(bool newFireState) {
        fire = newFireState;
    }
    public void OnFire(InputValue value) {
        FireInput(value.isPressed);
    }

    // Dialogue picker for the dialoge editor without a mouse
    private void Update() {
        if(ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive) {
            if(convoNext) {
                // Move to next option
                ConversationManager.Instance.SelectNextOption();
            } else if(convoPrev) {
                // Move to previous option
                ConversationManager.Instance.SelectPreviousOption();
            } else if(gameObject.GetComponent<StarterAssets.StarterAssetsInputs>().jump) {
                // Select a option (TO-DO: change input or disable jumping when selecting dialogue)
                ConversationManager.Instance.PressSelectedOption();
            }
        }
    }
}
