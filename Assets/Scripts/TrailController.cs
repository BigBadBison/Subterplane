using UnityEngine;

public class TrailController : MonoBehaviour {
    
    private SpriteRenderer liftSprite;
    
    private TrailRenderer liftTrail;
    private TrailRenderer exhaustTrail;
    private ParticleSystem smokeTrail;
    
    [SerializeField]
    private float minLift = 1f;
    [SerializeField]
    private float maxLift = 10f;

    [SerializeField]
    private Color exhaustingColor;
    
    private Color exhaustDefaultColor;
    private float exhaustDefaultWidth;
    
    private void Awake() {
        liftSprite = GetComponentInChildren<SpriteRenderer>();
        smokeTrail = GetComponentsInChildren<ParticleSystem>(true)[0];
        var trails = GetComponentsInChildren<TrailRenderer>();
        liftTrail = trails[0];
        exhaustTrail = trails[1];
        
        exhaustDefaultColor = exhaustTrail.startColor;
        exhaustDefaultWidth = exhaustTrail.widthMultiplier;
    }

    public void SmokeOn() {
        smokeTrail.gameObject.SetActive(true);
    }

    public void Thrusting(float thrustInput) {
        exhaustTrail.widthMultiplier = exhaustDefaultWidth * 0.5f * (thrustInput + 2);
        exhaustTrail.startColor = Color.Lerp(exhaustDefaultColor, exhaustingColor, thrustInput);
    }
    
    public void CurrentLift(float currentLift) {
        Color color = liftSprite.color;
        float t = Mathf.InverseLerp(minLift, maxLift, currentLift);

        color.a = Mathf.Lerp(0, 1, t);
        
        liftSprite.color = color;
        liftTrail.startColor = color;
        float t2 = Mathf.InverseLerp(maxLift, 2 * maxLift, currentLift);
        liftTrail.time = Mathf.Lerp(0.05f, 0.3f, t2);
    }

    public void Disable() {
        liftSprite.gameObject.SetActive(false);
        liftTrail.gameObject.SetActive(false);
        exhaustTrail.gameObject.SetActive(false);
    }
}
