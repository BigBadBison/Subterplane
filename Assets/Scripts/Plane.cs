using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public GameState gameState;
    
    [SerializeField]
    private GameObject lights;
    
    [SerializeField]
    private float thrustForce;
    [SerializeField]
    private float brakeForce;
    [SerializeField]
    private float pitchForce;

    float thrustInput;
    float pitchInput;

    private PlanePhysics planePhysics;
    private TrailController trailController;
    
    private Rigidbody2D rb;
    
    [SerializeField]
    private float vXInit = 20;

    private bool destroyed = false;
    private bool gameOver = false;
    
    private void Awake() {
        GlobalLight.plane = this;
        rb = GetComponent<Rigidbody2D>();
        planePhysics = GetComponent<PlanePhysics>();
        trailController = GetComponentInChildren<TrailController>();

        rb.velocity = Vector2.right * vXInit;
    }

    void Update () {
        pitchInput = Input.GetAxis("Horizontal");
        thrustInput = Input.GetAxis("Vertical");
        if (rb.velocity.magnitude < 0.05f && !gameOver) {
            gameOver = true;
            gameState.GameOver();
            Invoke(nameof(Disable), 2f);
        }
    }

    private void FixedUpdate()
    {
        if (!destroyed) {
            ApplyThrust();
            ApplyPitch();            
        }
        planePhysics.ApplyForces();
    }

    void ApplyThrust() {
        trailController.Thrusting(thrustInput);
        rb.AddRelativeForce(Vector2.right * (thrustInput * (thrustInput > 0f ? thrustForce : brakeForce)));
    }
    
    void ApplyPitch() {
        rb.AddTorque(-pitchInput * pitchForce);
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        trailController.SmokeOn();
        destroyed = true;
    }

    public void SetLights(bool status) {
        lights.SetActive(status);
    }
    
    void Disable() {
        rb.bodyType = RigidbodyType2D.Static;
        trailController.Disable();
        enabled = false;
    }
}
