using System;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class PlanePhysics : MonoBehaviour {
    protected Rigidbody2D rb;

    [SerializeField]
    protected float angleOfAttack;
    
    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void ApplyForces() {
        angleOfAttack = GetAngleOfAttack();
    }
    
    protected float GetAngleOfAttack() {
        return Vector2.SignedAngle(rb.velocity, transform.right);
    }
}
