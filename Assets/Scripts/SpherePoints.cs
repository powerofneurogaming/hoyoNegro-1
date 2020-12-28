using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePoints : MonoBehaviour
{
    public Vector3 center;
    public int PointCount = 200;
    public float TargetRadius = 10;
    public float radius = 1;
    public float sizeScale = 30;
    public bool expand;

    public Material redMat;
    public Material defaultMat;
    
    public Transform PointsParent;

    public List<GameObject> Spheres = new List<GameObject>();

    Vector3 VisualSphere;

    // Start is called before the first frame update
    void Start()
    {
        //get closest bone
        GameObject[] bones = GameObject.FindGameObjectsWithTag("bones");
        float distance = 10000;
        foreach (GameObject obj in bones)
        {
            if (Vector3.Distance(obj.transform.position, transform.position) < distance)
            {
                target = obj;
                distance = Vector3.Distance(obj.transform.position, transform.position);
            }
        }

        VisualSphere = target.transform.position - transform.position.normalized * TargetRadius + transform.position;
        PointsParent = transform.Find("Points");
        center = transform.position;
        Debug.Log(transform.position);
        foreach(Vector3 point in PointsOnSphere(PointCount))
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = point;
            Spheres.Add(obj);
            obj.transform.SetParent(PointsParent);
            obj.tag="rPoint";
        }
        PointsParent.transform.localPosition = Vector3.zero;
    }
    //private void OnDrawGizmos()
    //{
    //    foreach(Vector3 point in PointsOnSphere(PointCount))
    //    {
    //        Gizmos.DrawSphere(point*radius, radius/sizeScale);
    //    }
    //}

    Vector3[] PointsOnSphere(int n)
    {
        List<Vector3> upts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / n;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = radius;
        float phi = 0;

        for (var k = 0; k < n; k++)
        {
            y = k * off - 1 + (off / 2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            x += transform.position.x;
            y += transform.position.y;
            z += transform.position.z;
            upts.Add(new Vector3(x, y, z));
        }
        Vector3[] pts = upts.ToArray();
        return pts;
    }

    float velocity = 0;
    float hvelocity = 0;
    float TargetHeight = 0;
    float height;
    // Update is called once per frame
    void Update()
    {
        height = Mathf.SmoothDamp(height, TargetHeight, ref hvelocity, 0.35f);
        //PointsParent.transform.position = new Vector3(PointsParent.transform.position.x, height, PointsParent.transform.position.z);
        

        center = PointsParent.transform.position;
        radius = Mathf.SmoothDamp(radius, expand ? TargetRadius : 1, ref velocity, 0.35f);
        //LocateObject();
        foreach(GameObject point in Spheres)
        {
            point.transform.position = (point.transform.position - center).normalized * radius+center;
            point.transform.localScale = Vector3.one * radius / sizeScale;
            if (Vector3.Distance(point.transform.position, VisualSphere) < sphereRadius)
            {
                point.GetComponent<MeshRenderer>().material = redMat;
            }
            else
            {
                point.GetComponent<MeshRenderer>().material = defaultMat;
            }
        }

        //if (radius > TargetRadius * 0.9f)
        //{
        //    LocateObject();
        //}

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Elevate(Camera.main.transform.GetComponent<Collider>());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Lower(Camera.main.transform.GetComponent<Collider>());
        }

    }
    float sphereRadius;
    float ViewAngle = 30;
    public GameObject target;
    void LocateObject(float height)
    {
        
        sphereRadius = 2 * TargetRadius * Mathf.Sin(ViewAngle / 2 * Mathf.Deg2Rad);
        Debug.Log(sphereRadius);
        VisualSphere = (target.transform.position - transform.position+Vector3.up*height).normalized * TargetRadius + transform.position;
        //Vector3 center =
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "MainCamera")
    //    {
    //        Elevate(other);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag=="MainCamera")
    //    {
    //        Lower(other);
    //    }
    //}

    public void Elevate(Collider other)
    {
        TargetHeight = other.transform.position.y-transform.position.y;
        expand = true;
        LocateObject(TargetHeight);
    }

    public void Lower(Collider other)
    {
        TargetHeight = 0f;
        expand = false;
    }

}
