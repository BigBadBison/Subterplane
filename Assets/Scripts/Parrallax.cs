using UnityEngine;

public class Parrallax : MonoBehaviour {
    private Transform parent;
    private Vector3 parentStartPos;
    private Vector3 targPos;
    private float startZ;

    [SerializeField] private float xFactor = 0;
    [SerializeField] private float yFactor = 0;
    
    private void Awake() {
        parent = transform.parent.transform;
        parentStartPos = parent.position;
        startZ = transform.localPosition.z;
    }

    void LateUpdate() {
        targPos = parent.position - parentStartPos;
        targPos.x *= xFactor;
        targPos.y *= yFactor;
        targPos.z = startZ;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targPos, 1f * Time.deltaTime);
    }
}
