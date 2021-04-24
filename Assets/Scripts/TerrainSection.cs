using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
public class TerrainSection : MonoBehaviour {
    static float groundOffset = 25f;

    Mesh mesh;
    EdgeCollider2D eCollider;

    List<Vector3> vertices;
    List<int> triangles;
    List<Vector2> ePoints;

    public static float Spacing {
        get => spacing;
        set => spacing = value;
    }
    
    static float spacing = 15f;
    
    public bool Inverted {
        get => inverted < 0;
        set { inverted = value ? -1 : 1; }
    }

    int inverted = 1;

    private void Awake() {
        gameObject.GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        eCollider = gameObject.GetComponent<EdgeCollider2D>();

        mesh.name = "Terrain Mesh";

        vertices = new List<Vector3>();
        triangles = new List<int>();
        ePoints = new List<Vector2>();
    }

    public void Generate(float[] heights) {
        Clear();

        for (int i = 0; i < heights.Length - 1; i++) {
            AddSegment(i, heights[i], heights[i + 1]);
        }

        if (Inverted) {
            vertices.Reverse();
        }
        
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        eCollider.points = ePoints.ToArray();
    }

    void Clear() {
        mesh.Clear();
        vertices.Clear();
        triangles.Clear();
        ePoints.Clear();
    }

    void AddSegment(int i, float h0, float h1) {
        Vector3 p0 = new Vector3(i * spacing, h0, 0);
        Vector3 p1 = new Vector3((i + 1) * spacing, h1, 0);
        Vector3 g0 = new Vector3(i * spacing, h0 - inverted * groundOffset, 0);
        Vector3 g1 = new Vector3((i + 1) * spacing, h1 - inverted * groundOffset, 0);
        if (i == 0) {
            ePoints.Add(p0);
        }
        ePoints.Add(p1);

        vertices.Add(g0);
        vertices.Add(p0);
        vertices.Add(p1);
        vertices.Add(g1);

        int offset = i * 4;

        triangles.Add(offset + 0);
        triangles.Add(offset + 1);
        triangles.Add(offset + 2);

        triangles.Add(offset + 0);
        triangles.Add(offset + 2);
        triangles.Add(offset + 3);
    }
}