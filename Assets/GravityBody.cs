using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour {

    private GravityAttractor _planet;
    private Rigidbody _rb;
    
    private void Awake()
    {
        _planet = GameObject.FindGameObjectWithTag("World").GetComponent<GravityAttractor>();
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        _planet.Attract(transform);
    }
}
