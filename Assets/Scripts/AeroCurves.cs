using UnityEngine;

public static class AeroCurves
{
    public static AnimationCurve RealisticLift(float stallAngle, float criticalAngle, float maxLiftCoefficient) {
        AnimationCurve liftCurve = new AnimationCurve();
        liftCurve.preWrapMode = WrapMode.PingPong;
        liftCurve.postWrapMode = WrapMode.PingPong;
        liftCurve.AddKey(new Keyframe(0, 0, 0, 1 / 10f));
        liftCurve.AddKey(new Keyframe(stallAngle, maxLiftCoefficient, 0, 0));
        liftCurve.AddKey(new Keyframe(criticalAngle, 0.8f, 0, 0));
        liftCurve.AddKey(new Keyframe(45f, 1.1f, 0, 0));
        liftCurve.AddKey(new Keyframe(90f, 0f, -1.4f / 45f, 0));
        return liftCurve;
    }

    public static AnimationCurve SimpleLift(float stallAngle, float criticalAngle, float maxLiftCoefficient) {
        AnimationCurve liftCurve = new AnimationCurve();
        liftCurve.preWrapMode = WrapMode.PingPong;
        liftCurve.postWrapMode = WrapMode.PingPong;
        liftCurve.AddKey(new Keyframe(0, 0, 0, 1 / 10f));
        liftCurve.AddKey(new Keyframe(stallAngle, maxLiftCoefficient, 0, 0));
        liftCurve.AddKey(new Keyframe(criticalAngle, maxLiftCoefficient, 0f, 0f));
        liftCurve.AddKey(new Keyframe(criticalAngle + stallAngle, 0f, -0.1f, 0));
        return liftCurve;
    }
    
    public static AnimationCurve RealisticDrag(float maxDrag) {
        AnimationCurve dragCurve = new AnimationCurve();
        dragCurve.preWrapMode = WrapMode.PingPong;
        dragCurve.postWrapMode = WrapMode.PingPong;
        dragCurve.AddKey(new Keyframe(0, 0, 0, 0));
        dragCurve.AddKey(new Keyframe(90f, maxDrag,0, 0));
        return dragCurve;
    }
}
