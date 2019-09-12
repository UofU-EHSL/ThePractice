using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshCollider))]
[System.Serializable]
public class Meshes
{
    public MeshCollider meshCollider;
    public GameObject[] vert;
    public int[] triangles;
}
public class MeshGenerator : MonoBehaviour
{
    public Meshes[] meshes;
    void Start()
    {
        foreach (Meshes single_mesh in meshes) {
            CreateMesh(single_mesh);
        }
    }

    private void CreateMesh(Meshes single_mesh)
    {
        Vector3[] vertices = {
            single_mesh.vert[0].transform.localPosition,
            single_mesh.vert[1].transform.localPosition,
            single_mesh.vert[2].transform.localPosition,
            single_mesh.vert[3].transform.localPosition,
            single_mesh.vert[4].transform.localPosition,
            single_mesh.vert[5].transform.localPosition,
            single_mesh.vert[6].transform.localPosition,
        };

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = single_mesh.triangles;
        mesh.RecalculateNormals();
        single_mesh.meshCollider.sharedMesh = mesh;
    }
}