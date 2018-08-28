using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public CanvasGroup dialogueCanvasGroup;
	public DialogueImplementation dialogueSystem;
    public float playerSpeed;

    private Rigidbody _rb;

    private Animator _sprite;
    private Vector2 __2DSpeed;
    private float lastDirection;

	private bool _canInteract = false;
	private bool _isInDialogue = false;
	private DialogueInteractiveBehaviour _interactableNPC = null;

	void Start () {
        _rb = GetComponent<Rigidbody>();
        _sprite = GetComponentInChildren<Animator>();
    }
	
	void Update () {
        _rb.velocity = Vector3.zero;

		if (!_isInDialogue) {

            _rb.velocity = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * playerSpeed;

            // Animation Int 0 is UP, going Clockwise (C'EST DEGEULASSE COMME CODE BISOUS)
            __2DSpeed = new Vector2(_rb.velocity.x, _rb.velocity.z);
            lastDirection = Vector2.SignedAngle(Vector2.up, __2DSpeed);
           
            _sprite.SetFloat("Dir", lastDirection);



        }

		if(Input.GetButtonDown("Interact"))
		{
            //print("Interact");
			if (_canInteract && !_isInDialogue) {
				_isInDialogue = true;
				StartCoroutine(dialogueCanvasGroup.Fade(0.5f, true));
				dialogueSystem.SetDialogueToRun(_interactableNPC.dialogue);
				dialogueSystem.RunDialogue();
				StartCoroutine(WaitForDialogueEndAndFade());
			}
		}
    }

	private IEnumerator WaitForDialogueEndAndFade() {
		while (!dialogueSystem.IsDialogueEnded()) {
			yield return null;
		}
		StartCoroutine(dialogueCanvasGroup.Fade(0.5f, false));
		_isInDialogue = false;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.transform.CompareTag("NPC")) {
			_canInteract = true;
			_interactableNPC = other.GetComponent<DialogueInteractiveBehaviour>();
			Debug.Log("Enter an interaction zone of an NPC");
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.transform.CompareTag("NPC")) {
			_canInteract = false;
			_interactableNPC = null;
			Debug.Log("Enter an interaction zone of an NPC");
		}
	}
}
