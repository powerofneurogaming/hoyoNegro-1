using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSlicer : MonoBehaviour
{
    public int[] Triangles;
    public Vector3[] Vertices;
    List<Vector3> currTriVerts = new List<Vector3>();
    public Material litMat;
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Triangles = mesh.triangles;
        Vertices = mesh.vertices;
        Debug.Log(Triangles.Length);
        Debug.Log(Vertices.Length);
        for(int TriCount = 0; TriCount < Triangles.Length; TriCount++)
        {
            if (TriCount % 3 == 0)
            {
                if (TriCount != 0)
                {
                    var obj = new GameObject("triangle");
                    obj.AddComponent<MeshFilter>();
                    obj.AddComponent<MeshRenderer>();
                    obj.GetComponent<MeshRenderer>().material = new Material(litMat);
                    Mesh TriMesh = obj.GetComponent<MeshFilter>().mesh;
                    TriMesh.vertices = new Vector3[] { currTriVerts[0], currTriVerts[1], currTriVerts[2] };
                    TriMesh.uv = new Vector2[] { (Vector2)currTriVerts[0], (Vector2)currTriVerts[1], (Vector2)currTriVerts[2] };
                    TriMesh.triangles = new int[] { 0, 1, 2 };
                }
                currTriVerts.Clear();
                currTriVerts.Add(Vertices[Triangles[TriCount]]*1000);
            }
            else
            {
                currTriVerts.Add(Vertices[Triangles[TriCount]]*1000);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
