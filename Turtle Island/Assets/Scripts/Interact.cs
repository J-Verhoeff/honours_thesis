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
    // public variables to see input values in the editor
    [Header("Interact Values")]
    public bool interact;
    public bool convoNext;
    public bool convoPrev;
    public bool fire;

    // Functions to get input for the interact button
    public void InteractInput(bool newInteractState) {
        interact = newInteractState;
    }
    public void OnInteract(InputValue value) {
        InteractInput(value.isPressed);
    }

    // Functions to handle UI interaction using a gamepad
    public void ConvoNextInput(bool newConvoNextState) {
        convoNext = newConvoNextState;
    }
    public void OnConvoNext(InputValue value) {
        ConvoNextInput(value.isPressed);
    }
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

    private void Update() {
        if(ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive) {
            if(convoNext) {
                ConversationManager.Instance.SelectNextOption();
            } else if(convoPrev) {
                ConversationManager.Instance.SelectPreviousOption();
            } else if(gameObject.GetComponent<StarterAssets.StarterAssetsInputs>().jump) {
                ConversationManager.Instance.PressSelectedOption();
            }
        }
    }
}
