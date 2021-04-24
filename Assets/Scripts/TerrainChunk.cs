using UnityEngine;

public class TerrainChunk : MonoBehaviour {
    private TerrainSection topTerrain;
    private TerrainSection bottomTerrain;

    private void Awake() {
        TerrainSection[] terrains = GetComponentsInChildren<TerrainSection>();
        topTerrain = terrains[0];
        topTerrain.Inverted = true;
        bottomTerrain = terrains[1];
    }

    public void Generate(float[] groundPoints, float[] ceilingPoints) {
        bottomTerrain.Generate(groundPoints);
        topTerrain.Generate(ceilingPoints);
    }
}