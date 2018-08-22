using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody _rb;

	void Start () {
        _rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        _rb.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.Z))
        {
            _rb.velocity = transform.forward * 15f;
        } else if (Input.GetKey(KeyCode.S))
        {
            _rb.velocity = transform.forward * -15f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _rb.velocity = transform.right * -15f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _rb.velocity = transform.right * 15f;
        }
    }

    private void LateUpdate()
    {
        
    }
}
