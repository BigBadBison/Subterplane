using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour {

    [SerializeField]
    private Transform[] backgrounds;
    
    private Camera cam;
    private int curr;
    private Bounds _bounds;
    private Transform currBackground;
    private Transform prevBackground;

    void Awake() {
        _bounds = GetComponentInChildren<SpriteRenderer>().bounds;
        cam = Camera.main;
        curr = 1;
        prevBackground = backgrounds[0];
        currBackground = backgrounds[1];
    }

    void Update()
    {
        if(cam.transform.position.x >= currBackground.position.x) {
            curr += 1;
            curr %= backgrounds.Length;
            Vector3 pos = backgrounds[curr].localPosition;
            pos.x += _bounds.extents.x * 2;
            prevBackground.localPosition = pos;
            prevBackground = currBackground;
            currBackground = backgrounds[curr];
        }
    }
}
