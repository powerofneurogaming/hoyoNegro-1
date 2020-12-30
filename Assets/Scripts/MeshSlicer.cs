using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSlicer : MonoBehaviour
{
    public int[] Triangles;
    public Vector3[] Vertices;
    List<Vector3> currTriVerts = new List<Vector3>();
    public Material litMat;
    public GameObject Triangle;
    Transform Holder;
    public Vector3 spin;
    public Transform target;
    public float AccAngle=30f;
    public GameObject collisionSphere;
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        //Debug.Log(mesh.bounds);
        Holder = transform.Find("Triangles");
        Triangles = mesh.triangles;
        Vertices = mesh.vertices;
        Debug.Log(Triangles.Length);
        Debug.Log(Vertices.Length);
        //generate the meshes
        for(int TriCount = 0; TriCount < Triangles.Length; TriCount++)
        {
            currTriVerts.Add(Vertices[Triangles[TriCount]]);
            if (TriCount % 3 == 2)
            {
                if (TriCount != 0)
                {
                    //calculate spawn location
                    float x = 0; float y = 0; float z = 0;
                    x = (currTriVerts[0].x + currTriVerts[1].x + currTriVerts[2].x)/3+transform.position.x;
                    y = (currTriVerts[0].y + currTriVerts[1].y + currTriVerts[2].y)/3+transform.position.y;
                    z = (currTriVerts[0].z + currTriVerts[1].z + currTriVerts[2].z)/3+transform.position.z;

                    //create mesh
                    var obj = Instantiate(Triangle, new Vector3(x, y, z), Quaternion.identity);
                    obj.GetComponent<MeshRenderer>().material = new Material(litMat);
                    float scale = obj.transform.localScale.x;
                    Mesh TriMesh = obj.GetComponent<MeshFilter>().mesh;
                    Debug.Log(currTriVerts[0]); Debug.Log(currTriVerts[1]); Debug.Log(currTriVerts[2]);
                    obj.transform.position = Vector3.zero;
                    TriMesh.vertices = new Vector3[] { currTriVerts[0]/scale+transform.position, currTriVerts[1]/scale+transform.position, currTriVerts[2]/scale+transform.position};
                    TriMesh.triangles = new int[] { 0, 1, 2 };
                    obj.transform.SetParent(Holder);
                    obj.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);

                    //recalculate bounds to avoid culling behavior
                    //Debug.Log(obj.GetComponent<MeshRenderer>().bounds);
                    TriMesh.RecalculateBounds();
                    //Debug.Log(obj.GetComponent<MeshRenderer>().bounds);
                    obj.AddComponent<MeshCollider>();
                }
                currTriVerts.Clear();
                //currTriVerts.Add(Vertices[Triangles[TriCount]]);
            }
        }
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;

        spin = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        target = GameObject.FindGameObjectWithTag("bones").transform;
        //LocateObject(0);
    }

    float velocity = 0;
    // Update is called once per frame
    void Update()
    {
        LocateObject(0);
        Holder.transform.Rotate(spin * Time.deltaTime*5);
        Holder.transform.localScale=Mathf.SmoothDamp(Holder.transform.localScale.x, Expanded ? TargetRadius : 1, ref velocity, 0.35f)*Vector3.one;
        //foreach (Transform t in Holder)
        //{
        //    Debug.Log(t.GetComponent<Renderer>().material.color);
        //    Vector3 planeVec = t.position - transform.position;
        //    Vector3 targetVec = target.position - transform.position;
        //    bool inside = Vector3.Angle(planeVec, targetVec) < AccAngle;
        //    t.GetComponent<MeshRenderer>().material.SetColor("_Color", inside ? Color.green : Color.blue);
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Toggle();
        //}
    }

    float sphereRadius;
    float TargetRadius = 10;
    Vector3 VisualSphere;
    void LocateObject(float height)
    {
        collisionSphere.transform.position = (target.position - transform.position).normalized * TargetRadius+transform.position;
        sphereRadius = 2 * TargetRadius * Mathf.Sin(AccAngle / 2 * Mathf.Deg2Rad);
        collisionSphere.transform.localScale = Vector3.one * sphereRadius;
        Debug.Log(sphereRadius);
        VisualSphere = (target.transform.position - transform.position).normalized * TargetRadius + transform.position;
        //Vector3 center =
    }

    bool Expanded = false;
    float radius = 1f;
    public void Toggle()
    {
        Expanded = !Expanded;
        radius = Expanded ? 10f : 1f;
    }
}
