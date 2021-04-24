using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLight : MonoBehaviour {
    [SerializeField]
    private float intensityGradient;

    [SerializeField]
    private GameObject lights;
    
    private Camera cam;
    private Light2D light;
    
    
    void Awake()
    {
        cam = Camera.main;
        light = GetComponent<Light2D>();
    }

    void Update() {
        light.intensity = 1 - cam.transform.position.x * intensityGradient;
        if (light.intensity < 0.5f) {
            lights.SetActive(true);
        }
    }
}
