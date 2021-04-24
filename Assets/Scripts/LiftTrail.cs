using UnityEngine;

public class LiftTrail : MonoBehaviour {
    
    private TrailRenderer trail;
    
    [SerializeField]
    private float maxWidth = 1f;

    
    public float MaxLift {
        set => maxLift = value;
    }

    private float maxLift;
    
    private void Awake() {
        trail = GetComponent<TrailRenderer>();
    }

    public void CurrentLift(float currentLift) {
        trail.widthMultiplier = Mathf.Lerp(0, maxWidth, currentLift / maxLift);
    }
}
