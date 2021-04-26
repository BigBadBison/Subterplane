using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float xOffset;
    public float yOffset;

    [SerializeField] private Transform target;

    private void Update() {
        Vector3 pos = target.position;
        pos.x += xOffset;
        pos.y += yOffset;
        pos.z = transform.position.z;
        transform.position = pos;
    }

    public void Follow(Transform newTarget) {
        target = newTarget;
    }
}
