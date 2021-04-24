using System;
using Unity.Collections;
using UnityEngine;

public class RealisticPlanePhysics : PlanePhysics {

    
    private float stallAngle = 15f;
    private float criticalAngle = 20f;
    private float maxLiftCoefficient = 1.2f;

    [SerializeField]
    private AnimationCurve liftCurve;
    [SerializeField]
    private AnimationCurve dragCurve;

    private LiftTrail lTrail;
    
    // debugging
    [SerializeField]
    private float lift;
    [SerializeField]
    private float drag;
    [SerializeField]
    private float vRel;
        
    [SerializeField]
    private float aeroFactor = 1;
    
    protected override void Awake() {
        base.Awake();

        lTrail = GetComponentInChildren<LiftTrail>();
        lTrail.MaxLift = maxLiftCoefficient;
        
        liftCurve = new AnimationCurve();
        liftCurve.preWrapMode = WrapMode.PingPong;
        liftCurve.postWrapMode = WrapMode.PingPong;
        liftCurve.AddKey(new Keyframe(0, 0, 0, 1 / 10f));
        liftCurve.AddKey(new Keyframe(stallAngle, maxLiftCoefficient, 0, 0));
        liftCurve.AddKey(new Keyframe(criticalAngle, 0.8f, 0, 0));
        liftCurve.AddKey(new Keyframe(45f, 1.1f, 0, 0));
        liftCurve.AddKey(new Keyframe(90f, 0f, -1.4f / 45f, 0));

        dragCurve = new AnimationCurve();
        dragCurve.preWrapMode = WrapMode.PingPong;
        dragCurve.postWrapMode = WrapMode.PingPong;
        dragCurve.AddKey(new Keyframe(0, 0, 0, 0));
        dragCurve.AddKey(new Keyframe(90f, 2f,0, 0));
    }

    public override void ApplyForces() {
        base.ApplyForces();
        vRel = Vector2.Dot(rb.velocity, transform.right);
        ApplyLift();
        ApplyDrag();
    }

    void ApplyLift() {
        lift = CalculateLift();
        lift *= Vector2.Dot(transform.up, Vector2.up);
        rb.AddForce(Vector2.up * lift);
        rb.AddRelativeForce(Vector2.up * CalculateLift());
    }
    
    void ApplyDrag() {
        drag = CalculateDrag();
        //rb.AddRelativeForce(Vector2.left * CalculateDrag());
    }
    
    float CalculateLift() {
        float cLift = Mathf.Sign(angleOfAttack) * liftCurve.Evaluate(angleOfAttack);
        lTrail.CurrentLift(cLift);
        return cLift * aeroFactor * vRel * vRel;
    }

    float CalculateDrag() {
        float cDrag = dragCurve.Evaluate(angleOfAttack);
        return Mathf.Sign(vRel) * cDrag * aeroFactor * vRel * vRel;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rb.transform.up * lift / 100);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, -rb.transform.right * drag / 100);
    }
}
