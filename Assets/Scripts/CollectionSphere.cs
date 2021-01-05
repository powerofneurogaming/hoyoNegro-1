using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * 3;
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        float counter = 0;
        float target = 2;
        while (counter < target)
        {
            counter += Time.deltaTime;
            transform.localScale = Vector3.one+Vector3.one * Mathf.Sin(counter / target * Mathf.PI)*3;
            //transform.localScale = Vector3.one * Mathf.Lerp(3, 1, counter / target);
            yield return null;
        }
        if (targetBone == null)
        {
            Debug.Log("No Bone Found");
        }
        else
        {
            targetBone.transform.position = transform.position;
            targetBone.GetComponent<Bone>().collectable = true;
            targetBone.GetComponent<MeshRenderer>().enabled = true;
            ScannerInteraction.instance.CollectorCount++;
            Destroy(gameObject);
        }
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject targetBone;
    float distance = 100;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bones")
        {
            if (Vector3.Distance(transform.position, other.transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, other.transform.position);
                targetBone = other.gameObject;
            }
        }
    }
}
