using System;
using Unity.Collections;
using UnityEngine;

public class SimplePlanePhysics : PlanePhysics {
    
    [SerializeField]
    private float aeroFactor;
    
    private float stallAngle = 15f;
    private float criticalAngle = 20f;

    [SerializeField]
    private AnimationCurve liftCurve;

    // debugging
    [SerializeField]
    private float lift;

    protected override void Awake() {
        base.Awake();
        
        liftCurve = new AnimationCurve();
        liftCurve.preWrapMode = WrapMode.PingPong;
        liftCurve.postWrapMode = WrapMode.PingPong;
        liftCurve.AddKey(new Keyframe(0, 0, 0, 1 / 10f));
        liftCurve.AddKey(new Keyframe(stallAngle, 1.2f, 0, 0));
        liftCurve.AddKey(new Keyframe(criticalAngle, 0.8f, 0, 0));
        liftCurve.AddKey(new Keyframe(criticalAngle, 0f, 0, 0));
        liftCurve.AddKey(new Keyframe(90f, 0f, 0f, 0));
    }

    public override void ApplyForces() {
        base.ApplyForces();
        ApplyLift();
    }

    void ApplyLift() {
        lift = CalculateLift();
        rb.AddForce(Vector2.up * CalculateLift());
    }

    float CalculateLift() {
        float vRel = Vector2.Dot(rb.velocity, transform.right);
        float cLift = 0f;
        if (Mathf.Abs(angleOfAttack) < criticalAngle)
        {
            cLift = Mathf.Sign(angleOfAttack) * liftCurve.Evaluate(angleOfAttack);
        }
        
        return cLift * aeroFactor * vRel * vRel;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rb.transform.up * lift / 100);
    }
}
