using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHandler : MonoBehaviour
{
    [SerializeField]
    private TerrainChunk terrainPrefab;
    
    [SerializeField]
    private Texture2D noiseSource;
    
    [SerializeField]
    private int terrainLength = 25;
    [SerializeField]
    private int chunks = 2;
    [SerializeField]
    private float spacing = 1f;
    
    [SerializeField]
    private float angle = -5;
    [SerializeField]
    private float angleFactor = 1;
    [SerializeField]
    private float featureFactor = 0.5f;    
    [SerializeField]
    private float height = 17.5f;
    [SerializeField]
    private float heightFactor = 2f;
    
    [SerializeField]
    private float heightDifficulty = 0.95f;
    [SerializeField]
    private float angleDifficulty = 0.15f;
    
    private Camera cam;
    
    int pos = 0;
    TerrainChunk[] terrainChunks;
    int cur_idx = 0;
    
    float[] groundPoints;
    float[] ceilingPoints;

    private Vector3 nextStartPos;
    
    private void Awake() {
        TerrainSection.Spacing = spacing;
        
        UnityEngine.Random.InitState(0);
        cam = Camera.main;
        terrainChunks = new TerrainChunk[chunks];
        groundPoints = new float[terrainLength];
        ceilingPoints = new float[terrainLength];
        ceilingPoints[terrainLength - 1] = height;
        nextStartPos = Vector3.zero;

        for (int i = 0; i < chunks; i++) {
            TerrainChunk instance = Instantiate(terrainPrefab, transform, true);
            terrainChunks[i] = instance;
            Generate();
        }
    }
    
    void Update()
    {
        if(cam.transform.position.x + cam.orthographicSize * 2 >= pos * spacing) {
            Generate();
        }
    }

    
    void Generate() {
        float slope = Mathf.Tan((angle + RandomGetNextPoint(angleFactor)) * Mathf.Deg2Rad);
        TerrainChunk chunk = terrainChunks[cur_idx];
        chunk.transform.localPosition = nextStartPos;
        groundPoints[0] = 0;
        ceilingPoints[0] = ceilingPoints[terrainLength - 1] - groundPoints[terrainLength - 1];

        for (int i = 1; i < terrainLength; i++)
        {
            groundPoints[i] = slope * (i * spacing) + GetPerlinPoint(pos, 0) * featureFactor;
            ceilingPoints[i] = groundPoints[i] + height + GetPerlinPoint(pos, 1) * heightFactor;
            pos++;
        }
        
        terrainChunks[cur_idx].Generate(groundPoints, ceilingPoints);

        nextStartPos.x += terrainLength * spacing - 1;
        nextStartPos.y += groundPoints[terrainLength - 1];
        
        cur_idx++;
        cur_idx %= chunks;

        AdjustDifficulty();
    }

    void AdjustDifficulty() {
        height *= heightDifficulty;
        angle -= angleDifficulty;
    }
    
    float RandomGetNextPoint(float factor) {
        return UnityEngine.Random.Range(-factor, factor) / 2f;
    }
    
    float GetPerlinPoint(int pos, int ch) {
        float scale = 255f;
        float u = (pos % scale) / scale;
        float v = pos / scale;
        var c = noiseSource.GetPixelBilinear(u, v);
        if (ch == 0) {
            return c.r - 0.5f;
        }
        if(ch == 1) {
            return c.g - 0.5f;
        }
        return c.b;
    }
}
