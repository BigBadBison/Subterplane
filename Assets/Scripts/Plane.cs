using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField]
    private float thrustForce;
    [SerializeField]
    private float brakeForce;
    [SerializeField]
    private float pitchForce;

    float thrustInput;
    float pitchInput;

    private PlanePhysics planePhysics;
    
    private Rigidbody2D rb;
    
    [SerializeField]
    private float vXInit = 20;
    
    [SerializeField]
    private float angleOfAttack;

    private bool applyPhysics;
    
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        planePhysics = GetComponent<PlanePhysics>();

        rb.velocity = Vector2.right * vXInit;
    }

    void Update () {
        pitchInput = Input.GetAxis("Horizontal");
        thrustInput = Input.GetAxis("Vertical");
        applyPhysics = !Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        if (!applyPhysics) {
            return;
        }
        
        ApplyThrust();
        ApplyPitch();
        planePhysics.ApplyForces();
    }

    
    void ApplyThrust() {
        rb.AddRelativeForce(Vector2.right * (thrustInput * (thrustInput > 0f ? thrustForce : brakeForce)));
    }
    
    void ApplyPitch() {
        rb.AddTorque(-pitchInput * pitchForce);
    }
}
