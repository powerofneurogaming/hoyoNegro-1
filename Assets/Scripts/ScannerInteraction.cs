using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerInteraction : Singleton<ScannerInteraction>
{
    public float ProjectionDistacne = 5;
    public GameObject spherePrefab;
    public LayerMask layerIgnore;
    AudioSource collectSound;
    Transform CamTrs;
    // Start is called before the first frame update
    void Start()
    {
        CamTrs = Camera.main.transform;
        SonarText.text = "0";
        ProbcountText.text = ProbeCount.ToString() + "/" + ProbeMax.ToString();
        CollectorCountText.text = CollectorCount.ToString() + "/" + CollectorMax.ToString();
        collectSound = transform.GetComponent<AudioSource>();
    }


    //use q to place
    //use E to interact
    int ProbeMax = 3;
    int ProbeCount = 3;
    float DismantleTimer=1;
    float DismantleCounter = 0;

    public int CollectorMax = 3;
    public int CollectorCount = 3;

    public bool project = false;
    float sonarCD = 5f;
    GameObject placingSphere = null;
    public GameObject Sonar;
    public GameObject currSonarTarget=null;
    public GameObject currProbeTarget=null;

    public GameObject CollectionOrb;
    int collected = 0;
    public int stage = 0;

    bool sonarReady = true;
    float sonarCooldownTime = 5f;

    public Text SonarText;
    public Text ProbcountText;
    public Text CollectorCountText;

    public GameObject probeTips;
    public GameObject collectorTips;
    public GameObject placingTips;
    public GameObject DeconstructionTips;
    public Image DeconstructionBar;

    public GameObject HUD;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HUD.SetActive(false);
        }

        //collect input
        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(CamTrs.position,CamTrs.forward,out hit, 5))
            {
                if (CollectBone(hit))
                {
                    Bone bone = hit.transform.GetComponent<Bone>();
                    BoneInfomation.instance.CollectBone(bone);
                    currProbeTarget = null;
                    collected++;
                    stage = Mathf.FloorToInt(collected / 10);
                    collectSound.Play();
                }
            }
        }


        //sonar input
        if (Input.GetKeyDown(KeyCode.R) && sonarReady)
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
            StartCoroutine(SonarCooldown(sonarCooldownTime));
        }
        
        //triangulation sphere
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (placingSphere == null)
            {
                if (ProbeCount > 0)
                {
                    placingSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
                    project = true;
                }
            }
            else if (placingSphere.tag == "Sphere")
            {
                Destroy(placingSphere);
                placingSphere = null;
                project = false;
            }
            else
            {
                if (ProbeCount > 0)
                {
                    Destroy(placingSphere);
                    placingSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
                    project = true;
                }
            }
        }

        if (project)
        {
            ProjectSPhere(ProjectionDistacne);
        }

        //general interaction key
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (project)
            {
                project = false;
                //placingSphere.GetComponent<SpherePoints>().enabled = true;
                
                if (placingSphere.tag=="Sphere")
                {
                    ProbeCount--;
                    ProbcountText.text = ProbeCount.ToString() + "/" + ProbeMax.ToString();
                    placingSphere.transform.GetComponent<SphereCollider>().enabled = true;
                    placingSphere.transform.Find("Icosphere").Find("Sphere").GetComponent<SphereCollider>().enabled = true;
                }
                else
                {
                    CollectorCount--;
                    CollectorCountText.text = CollectorCount.ToString() + "/" + CollectorMax.ToString();
                    placingSphere.GetComponent<SphereCollider>().enabled = true;
                    placingSphere.GetComponent<CollectionSphere>().enabled = true;
                }
                placingSphere = null;
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
                else if(hit.transform.gameObject.tag == "powerup")
                {
                    if (hit.transform.name.Contains("Scanner"))
                    {
                        ProbeCount++;
                        ProbeMax++;
                    }
                    else
                    {
                        CollectorMax++;
                        CollectorCount++;
                    }
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        //pickup key
        if (Input.GetKey(KeyCode.F) && !project)
        {
            if(Physics.Raycast(CamTrs.position,CamTrs.forward,out hit, 5))
            {
                if (hit.transform.tag == "Sphere" || hit.transform.tag=="collector")
                {
                    DismantleCounter += Time.deltaTime;
                    Debug.Log(DismantleCounter);
                    if (DismantleCounter > DismantleTimer)
                    {
                        Destroy(hit.transform.gameObject);
                        if (hit.transform.tag == "Sphere")
                        {
                            ProbeCount++;
                        }
                        else
                        {
                            CollectorCount++;
                        }
                    }
                }
            }
        }
        
        if (Input.GetKeyUp(KeyCode.F))
        {
            DismantleCounter = 0;
        }

        //collector key
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (placingSphere == null)
            {
                if (CollectorCount > 0)
                {
                    placingSphere = Instantiate(CollectionOrb, transform.position, Quaternion.identity);
                    project = true;
                }
            }
            else if (placingSphere.tag == "collector")
            {
                Destroy(placingSphere);
                placingSphere = null;
                project = false;
            }
            else
            {
                if (CollectorCount > 0)
                {
                    Destroy(placingSphere);
                    placingSphere = Instantiate(CollectionOrb, transform.position, Quaternion.identity);
                    project = true;
                }
            }
        }

        probeTips.SetActive(false);
        collectorTips.SetActive(false);
        placingTips.SetActive(false);
        DeconstructionTips.SetActive(false);
        //tooltip section
        if(Physics.Raycast(CamTrs.position,CamTrs.forward,out hit, 5))
        {
            Debug.Log(hit.transform.name);
            if (DismantleCounter > 0)
            {
                DeconstructionTips.SetActive(true);
                DeconstructionBar.fillAmount = DismantleCounter / DismantleTimer;
            }
            else if (project)
            {
                placingTips.SetActive(true);
            }
            else if (hit.transform.tag == "Sphere")
            {
                probeTips.SetActive(true);
            }
            else if (hit.transform.tag == "collector")
            {
                collectorTips.SetActive(true);
            }
            
        }

        CollectorCountText.text = CollectorCount.ToString() + "/" + CollectorMax.ToString();
        ProbcountText.text = ProbeCount.ToString() + "/" + ProbeMax.ToString();

        //debug section
        if (Input.GetKeyDown(KeyCode.Keypad0))
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
        if (Physics.Raycast(CamTrs.position,CamTrs.forward,out hit,distance,~layerIgnore) && hit.transform.gameObject != placingSphere)
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
    public Transform sonarSound;
    IEnumerator SonarFeedback(float time, int count)
    {
        sonarSound.transform.position = currSonarTarget.transform.position;
        var audios = sonarSound.GetComponent<AudioSource>();
        yield return new WaitForSeconds(time);
        for(int i = 0; i < count; i++)
        {
            audios.Play();
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }
    public Image sonarkeyUI;
    IEnumerator SonarCooldown(float time)
    {
        float counter = 0;
        sonarReady = false;
        while (counter < time)
        {
            counter += Time.deltaTime;
            sonarkeyUI.fillAmount = counter / time;
            SonarText.text = (time - counter).ToString("0.##");
            yield return null;
        }
        sonarReady = true;
        yield return null;
    }
}
