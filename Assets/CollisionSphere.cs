using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "rPoint" && inParent(other))
        {
            other.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "rPoint" && inParent(other))
        {
            other.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }
    }

    bool inParent(Collider other)
    {
        return other.transform.parent.parent == transform.parent;
    }
}
