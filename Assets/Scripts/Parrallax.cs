using UnityEngine;

public class Parrallax : MonoBehaviour {
    [SerializeField] private Transform tracked;
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private float[] xFactor;
    
    private Vector3 trackedStartPos;
    private float startZ;
    
    private void Awake() {
        trackedStartPos = tracked.position;
        startZ = transform.localPosition.z;
    }

    void LateUpdate() {
        Vector3 offset = tracked.position - trackedStartPos;
        for (int i = 0; i < xFactor.Length; i++) {
            Vector3 targPos = offset;
            targPos.x *= xFactor[i];
            targPos.z = startZ;
            if (Vector3.Distance(backgrounds[i].localPosition, targPos) > 10f) {
                backgrounds[i].localPosition = targPos;
            }
            // backgrounds[i].localPosition = Vector3.Lerp(backgrounds[i].localPosition, targPos, 1f * Time.deltaTime);
            backgrounds[i].localPosition = targPos;
        }
    }
}
