using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneInfomation : MonoBehaviour
{

    Transform BearTrs;
    Transform ElephantTrs;
    Transform NaiaTrs;
    Transform SlothTrs;
    Transform TigerTrs;
    Transform WolfTrs;
    public List<Transform> holders = new List<Transform>();
    public int index;
    public void GrabObjects(Transform parentTrs)
    {
        BearTrs = parentTrs.Find("Bear");
        ElephantTrs = parentTrs.Find("Elephant");
        NaiaTrs = parentTrs.Find("Naia");
        SlothTrs = parentTrs.Find("Sloth");
        TigerTrs = parentTrs.Find("Tiger");
        WolfTrs = parentTrs.Find("Wolf");
        holders = new List<Transform> { BearTrs, ElephantTrs, NaiaTrs, SlothTrs, TigerTrs, WolfTrs };
        foreach(Transform trs in holders)
        {
            for(int i=2;i<trs.childCount;i++)
            {
                trs.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = false;
                trs.GetChild(i).gameObject.GetComponent<Bone>().collectable = false;
            }
        }

    }

    void ShowIndex(int index)
    {
        foreach(Transform trs in holders)
        {
            trs.GetChild(index).gameObject.SetActive(true);
        }
    }

    public Transform BearInfo;
    public Transform ElephantInfo;
    public Transform NaiaInfo;
    public Transform SlothInfo;
    public Transform TigerInfo;
    public Transform WolfInfo;
    public List<Transform> InfoTransforms;
    private void Start()
    {
        InfoTransforms = new List<Transform> { BearInfo, ElephantInfo, NaiaInfo, SlothInfo, TigerInfo, WolfInfo };
    }

    public void CollectBone(Bone bone)
    {
        InfoTransforms[bone.AnimalInt].GetChild(bone.indexer).gameObject.SetActive(true);
        bone.gameObject.SetActive(false);
    }
}
