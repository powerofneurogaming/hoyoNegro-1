﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoneInfomation : Singleton<BoneInfomation>
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

    
    
    public TextMeshProUGUI BearText;
    public TextMeshProUGUI ElephantText;
    public TextMeshProUGUI NaiaText;
    public TextMeshProUGUI SlothText;
    public TextMeshProUGUI TigerText;
    public TextMeshProUGUI WolfText;
    public List<TextMeshProUGUI> TextList;
    public int[] boneCount = new int[] { 0, 0, 0, 0, 0, 0 };
    private void Start()
    {
        InfoTransforms = new List<Transform> { BearInfo, ElephantInfo, NaiaInfo, SlothInfo, TigerInfo, WolfInfo };
        TextList = new List<TextMeshProUGUI> { BearText, ElephantText, NaiaText, SlothText, TigerText, WolfText };
        foreach(var t in TextList) { t.text = "0/5"; }
    }

    public void CollectBone(Bone bone)
    {
        InfoTransforms[bone.AnimalInt].GetChild(bone.indexer).gameObject.SetActive(true);
        boneCount[bone.AnimalInt]++;
        TextList[bone.AnimalInt].text = boneCount[bone.AnimalInt].ToString() + "/5";
        bone.gameObject.SetActive(false);
    }
}
