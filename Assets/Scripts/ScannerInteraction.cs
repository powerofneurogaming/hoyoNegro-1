using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerInteraction : MonoBehaviour
{
    public float ProjectionDistacne = 5;
    public GameObject spherePrefab;
    Transform CamTrs;
    // Start is called before the first frame update
    void Start()
    {
        CamTrs = Camera.main.transform;
    }


    //use q to place
    //use E to interact

    public bool project = false;
    GameObject placingSphere;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (placingSphere == null)
            {
                project = true;
                placingSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
            }
        }
        if (project)
        {
            ProjectSPhere(ProjectionDistacne);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (project)
            {
                project = false;
                placingSphere.GetComponent<SpherePoints>().enabled = true;
                placingSphere = null;
            }
            else if(Physics.Raycast(CamTrs.position,CamTrs.forward,out hit, 5))
            {
                if (hit.transform.gameObject.tag == "Sphere")
                {
                    Debug.Log("called");
                    var SphereScript = hit.transform.GetComponent<SpherePoints>();
                    if (SphereScript.expand)
                    {
                        SphereScript.Lower(transform.GetComponent<Collider>());
                    }
                    else
                    {
                        SphereScript.Elevate(transform.GetComponent<Collider>());
                    }
                }
            }
        }
    }
    RaycastHit hit;
    void ProjectSPhere(float distance)
    {
        Vector3 rayCastVector = CamTrs.forward;
        RaycastHit hit;
        if (Physics.Raycast(CamTrs.position,CamTrs.forward,out hit,distance) && hit.transform.gameObject != placingSphere)
        {
            placingSphere.transform.position = hit.point;
        }
        else
        {
            placingSphere.transform.position = CamTrs.position + CamTrs.forward * distance;
        }
    }
}
