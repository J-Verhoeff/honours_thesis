using UnityEngine;

public class Fishing : MonoBehaviour {
    [Header("Game Objects")]
    [SerializeField] GameObject boat;
    [SerializeField] GameObject spear;
    [SerializeField] GameObject paddle;
    

    private GameManager manager;
    private Animator animator;
    private bool isRowing = false;


    private void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start() {
        spear.SetActive(false);
        animator.SetBool("Rowing", true);
    }
}
