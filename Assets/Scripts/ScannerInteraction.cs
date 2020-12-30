using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerInteraction : Singleton<ScannerInteraction>
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
    int ProbMax = 3;
    int ProbeCount = 3;
    float DismantleTimer=2;
    float DismantleCounter = 0;

    public bool project = false;
    float sonarCD = 5f;
    GameObject placingSphere;
    public GameObject Sonar;
    public GameObject currSonarTarget;
    // Update is called once per frame
    void Update()
    {

        //collection input
        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(CamTrs.position,CamTrs.forward,out hit, 5))
            {
                if (CollectBone(hit))
                {
                    Bone bone = hit.transform.GetComponent<Bone>();
                    BoneInfomation.instance.CollectBone(bone);
                }
            }
        }


        //sonar input
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(Sonar, transform.position, Quaternion.identity);
            Transform closest = null; float iniDist = 1000;
            if (currSonarTarget == null)
            {
                GameObject[] bones = GameObject.FindGameObjectsWithTag("bones");
                foreach (var v in bones)
                {
                    if (v.transform.GetSiblingIndex() == 2 || v.transform.GetSiblingIndex() == 3)
                    {
                        if (Vector3.Distance(transform.position, v.transform.position) < iniDist)
                        {
                            iniDist = (Vector3.Distance(transform.position, v.transform.position));
                            closest = v.transform;
                            currSonarTarget = closest.gameObject;
                        }
                    }
                }
            }
            iniDist = Vector3.Distance(currSonarTarget.transform.position, transform.position);
            if (iniDist < 50)
            {
                int soundCount = 5 - (Mathf.FloorToInt(iniDist / 10));
                PlaySonarSound(2, soundCount);
                if (iniDist < 10)
                {
                    currSonarTarget.GetComponent<MeshRenderer>().enabled = true;
                    currSonarTarget.GetComponent<Bone>().collectable = true;
                    currSonarTarget = null;
                }
            }
        }
        
        
        //triangulation sphere
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (placingSphere == null)
            {
                if (ProbeCount > 0)
                {
                    project = true;
                    placingSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                Destroy(placingSphere);
                project = false;
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
                //placingSphere.GetComponent<SpherePoints>().enabled = true;
                placingSphere = null;
                ProbeCount--;
            }
            else if(Physics.Raycast(CamTrs.position,CamTrs.forward,out hit, 5))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.gameObject.tag == "Sphere")
                {
                    Debug.Log("called");
                    var SphereScript = hit.transform.Find("Icosphere").GetComponent<MeshSlicer>();
                    SphereScript.Toggle();
                }
            }
        }
        if (Input.GetKey(KeyCode.F))
        {
            if(Physics.Raycast(CamTrs.position,CamTrs.forward,out hit, 5))
            {
                if (hit.transform.tag == "Sphere")
                {
                    DismantleCounter += Time.deltaTime;
                    Debug.Log(DismantleCounter);
                    if (DismantleCounter > DismantleTimer)
                    {
                        Destroy(hit.transform.gameObject);
                        ProbeCount++;
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            DismantleCounter = 0;
        }

        //debug section
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            foreach(var v in BoneInfomation.instance.holders)
            {
                foreach(Transform z in v)
                {
                    z.gameObject.SetActive(true);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            foreach(var v in BoneInfomation.instance.holders)
            {
                for(int i = 0; i < 2; i++)
                {
                    v.GetChild(i).gameObject.SetActive(false);
                }
                for(int i= 2; i < v.childCount; i++)
                {
                    v.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            foreach (var v in BoneInfomation.instance.holders)
            {
                for (int i = 0; i < 4; i++)
                {
                    v.GetChild(i).gameObject.SetActive(false);
                }
                for (int i = 4; i < v.childCount; i++)
                {
                    v.GetChild(i).gameObject.SetActive(true);
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

    bool CollectBone(RaycastHit hit)
    {
        bool collected = false;
        if (hit.transform.GetComponent<Bone>() != null)
        {
            if (hit.transform.GetComponent<Bone>().collectable)
            {
                collected = true;
            }
        }
        return collected;
    }

     void PlaySonarSound(float time, int count)
    {
        StartCoroutine(SonarFeedback(time, count));
    }

    IEnumerator SonarFeedback(float time, int count)
    {
        var audios = GetComponents<AudioSource>();
        Debug.Log(audios[1].clip.name);
        yield return new WaitForSeconds(time);
        for(int i = 0; i < count; i++)
        {
            audios[1].Play();
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }
}
