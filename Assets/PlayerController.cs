﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public CanvasGroup dialogueCanvasGroup;
	public DialogueImplementation dialogueSystem;

    private Rigidbody _rb;

	private bool _canInteract = false;
	private bool _isInDialogue = false;

	void Start () {
        _rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        _rb.velocity = Vector3.zero;

		if (!_isInDialogue) {
			if (Input.GetKey(KeyCode.Z)) {
				_rb.velocity = transform.forward * 15f;
			} else if (Input.GetKey(KeyCode.S)) {
				_rb.velocity = transform.forward * -15f;
			}

			if (Input.GetKey(KeyCode.Q)) {
				_rb.velocity = transform.right * -15f;
			} else if (Input.GetKey(KeyCode.D)) {
				_rb.velocity = transform.right * 15f;
			}
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			if (_canInteract && !_isInDialogue) {
				_isInDialogue = true;
				StartCoroutine(dialogueCanvasGroup.Fade(0.5f, true));
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
			// TODO : Keep a ref to the last NPC we've crossed path
			_canInteract = true;
			Debug.Log("Enter an interaction zone of an NPC");
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.transform.CompareTag("NPC")) {
			_canInteract = false;
			Debug.Log("Enter an interaction zone of an NPC");
		}
	}
}
