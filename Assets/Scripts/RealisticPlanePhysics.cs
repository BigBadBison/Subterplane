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

    private TrailController trailController;
    
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

        trailController = GetComponentInChildren<TrailController>();

        liftCurve = AeroCurves.RealisticLift(stallAngle, criticalAngle, maxLiftCoefficient);
        dragCurve = AeroCurves.RealisticDrag(2f);
    }

    public override void ApplyForces() {
        base.ApplyForces();
        vRel = Vector2.Dot(rb.velocity, transform.right);
        ApplyLift();
        ApplyDrag();
    }

    void ApplyLift() {
        lift = CalculateLift();
        trailController.CurrentLift(lift);
        rb.AddForce(Vector2.Perpendicular(rb.velocity).normalized * lift);
    }
    
    void ApplyDrag() {
        drag = CalculateDrag();
        rb.AddForce(-rb.velocity.normalized * drag);
    }

    float CalculateLift() {
        float cLift = Mathf.Sign(angleOfAttack) * liftCurve.Evaluate(angleOfAttack);
        return cLift * aeroFactor * vRel * vRel;
    }

    float CalculateDrag() {
        float cDrag = dragCurve.Evaluate(angleOfAttack);
        return Mathf.Sign(vRel) * cDrag * aeroFactor * vRel * vRel;
    }

    public void Disable() {
        
    }

//     private void OnDrawGizmos() {
//         Gizmos.color = Color.red;
//         Gizmos.DrawRay(transform.position, Vector2.Perpendicular(rb.velocity).normalized * lift);
//         Gizmos.color = Color.blue;
//         Gizmos.DrawRay(transform.position, -rb.velocity.normalized * drag);
//     }
}
