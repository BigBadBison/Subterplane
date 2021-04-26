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
    private float startAngle = -5;
    [SerializeField]
    private float angleFactor = 1;
    [SerializeField]
    private float featureFactor = 0.5f;    
    [SerializeField]
    private float startHeight = 17.5f;
    [SerializeField]
    private float heightFactor = 2f;
    
    [SerializeField]
    private float heightDifficulty = 0.95f;
    [SerializeField]
    private float angleDifficulty = 0.15f;
    
    private Camera cam;

    TerrainChunk[] terrainChunks;
    
    int pos = 0;
    int cur_idx = 0;
    private float angle;
    private float height;
    
    private int[] perlinOffsets;
    float[] groundPoints;
    float[] ceilingPoints;

    private Vector3 nextStartPos;

    public static int Seed {
        get => seed;
        set => seed = value;
    }

    private static int seed;
    
    private void Awake() {
        TerrainSection.Spacing = spacing;
        seed = Random.Range(100, 10000);
        cam = Camera.main;
        terrainChunks = new TerrainChunk[chunks];
        groundPoints = new float[terrainLength];
        ceilingPoints = new float[terrainLength];
        perlinOffsets = new[] {0, 0};
        for (int i = 0; i < chunks; i++) {
            TerrainChunk instance = Instantiate(terrainPrefab, transform, true);
            terrainChunks[i] = instance;
        }
        Restart();
    }
    
    void Update()
    {
        if(cam.transform.position.x - cam.orthographicSize * 2 >= (pos - terrainLength * 3) * spacing) {
            Generate();
        }
    }

    public void Restart() {
        pos = 0;
        cur_idx = 0;

        UnityEngine.Random.InitState(seed);
        for (int i = 0; i < perlinOffsets.Length; i++) {
            perlinOffsets[i] = Random.Range(0, 255 * 255);
        }
        ceilingPoints[terrainLength - 1] = height;
        nextStartPos = Vector3.zero;
        
        height = 20f;
        angle = 0f;
        
        for (int i = 0; i < chunks; i++) {
            Generate();
        }

        angle = startAngle;
        height = startHeight;
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
        pos += perlinOffsets[ch];
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
