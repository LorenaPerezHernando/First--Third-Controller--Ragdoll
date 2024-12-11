using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class StoneNoise : MonoBehaviour
{
    public float noiseScale = 0.3f;
    public float noiseIntensity = 1.0f;

    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        noiseIntensity = Time.deltaTime;
        for (int i = 0; i < vertices.Length; i++)
        {
            float noise = Mathf.PerlinNoise(vertices[i].x * noiseScale, vertices[i].y * noiseScale) * noiseIntensity;
            vertices[i] += mesh.normals[i] * noise;
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    #region Properties
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}