using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour {
    [Header("Interact Values")]
    public bool interact;

    public void InteractInput(bool newInteractState) {
        interact = newInteractState;
    }

    public void OnInteract(InputValue value) {
        InteractInput(value.isPressed);
    }
}
