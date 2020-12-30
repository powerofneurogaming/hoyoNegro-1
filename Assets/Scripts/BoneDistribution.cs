using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneDistribution : MonoBehaviour
{
    public GameObject BonePrefab;
    public GameObject PresetBoneList;
    static bool ready = false;
    // Start is called before the first frame update
    /// <summary>
    /// creates the altered bone distribution to have 5 bones for each type, naia have 2 since homo sapien
    /// </summary>
    void Start()
    {
        var holder = new GameObject("Bear");
        holder.transform.SetParent(transform);
        foreach(string s in BearNumbers)
        {
            var Bear = Instantiate(BonePrefab, PresetBoneList.transform.Find(s).position, Quaternion.identity);
            Bear.GetComponent<Bone>().type = Bone.boneType.WigiBear;
            Bear.transform.SetParent(holder.transform);
            Bear.GetComponent<Bone>().AnimalInt = 0;
            Bear.GetComponent<Bone>().indexer = Bear.transform.GetSiblingIndex();
            Bear.name = "bear" + Bear.transform.GetSiblingIndex().ToString();
        }
        holder = new GameObject("Elephant");
        holder.transform.SetParent(transform);
        foreach (string s in ElephantNumbers)
        {
            var Elephant = Instantiate(BonePrefab, PresetBoneList.transform.Find(s).position, Quaternion.identity);
            Elephant.GetComponent<Bone>().type = Bone.boneType.Elephant;
            Elephant.transform.SetParent(holder.transform);
            Elephant.GetComponent<Bone>().AnimalInt = 1;
            Elephant.GetComponent<Bone>().indexer = Elephant.transform.GetSiblingIndex();
            Elephant.name = "bear" + Elephant.transform.GetSiblingIndex().ToString();
        }
        holder = new GameObject("Naia");
        holder.transform.SetParent(transform);
        foreach (string s in NaiaNumbers)
        {
            var Naia = Instantiate(BonePrefab, PresetBoneList.transform.Find(s).position, Quaternion.identity);
            Naia.GetComponent<Bone>().type = Bone.boneType.Naia;
            Naia.transform.SetParent(holder.transform);
            Naia.GetComponent<Bone>().AnimalInt = 2;
            Naia.GetComponent<Bone>().indexer = Naia.transform.GetSiblingIndex();
            Naia.name = "bear" + Naia.transform.GetSiblingIndex().ToString();
        }
        holder = new GameObject("Sloth");
        holder.transform.SetParent(transform);
        foreach (string s in SlothNumbers)
        {
            var Sloth = Instantiate(BonePrefab, PresetBoneList.transform.Find(s).position, Quaternion.identity);
            Sloth.GetComponent<Bone>().type = Bone.boneType.Sloth;
            Sloth.transform.SetParent(holder.transform);
            Sloth.GetComponent<Bone>().AnimalInt = 3;
            Sloth.GetComponent<Bone>().indexer = Sloth.transform.GetSiblingIndex();
            Sloth.name = "bear" + Sloth.transform.GetSiblingIndex().ToString();
        }
        holder = new GameObject("Tiger");
        holder.transform.SetParent(transform);
        foreach (string s in TigerNumbers)
        {
            var Tiger = Instantiate(BonePrefab, PresetBoneList.transform.Find(s).position, Quaternion.identity);
            Tiger.GetComponent<Bone>().type = Bone.boneType.SaberToothTiger;
            Tiger.transform.SetParent(holder.transform);
            Tiger.GetComponent<Bone>().AnimalInt = 4;
            Tiger.GetComponent<Bone>().indexer = Tiger.transform.GetSiblingIndex();
            Tiger.name = "bear" + Tiger.transform.GetSiblingIndex().ToString();
        }
        holder = new GameObject("Wolf");
        holder.transform.SetParent(transform);
        foreach (string s in WolfNumbers)
        {
            var Wolf = Instantiate(BonePrefab, PresetBoneList.transform.Find(s).position, Quaternion.identity);
            Wolf.GetComponent<Bone>().type = Bone.boneType.Wolf;
            Wolf.transform.SetParent(holder.transform);
            Wolf.GetComponent<Bone>().AnimalInt = 5;
            Wolf.GetComponent<Bone>().indexer = Wolf.transform.GetSiblingIndex();
            Wolf.name = "bear" + Wolf.transform.GetSiblingIndex().ToString();
        }
        PresetBoneList.SetActive(false);
        GameObject.FindObjectOfType<BoneInfomation>().GrabObjects(transform); ;
        ready = true;
    }
    string[] BearNumbers = { "30", "31", "38", "40", "56" };
    string[] ElephantNumbers = { "02", "03", "04", "06", "07" };
    string[] NaiaNumbers = { "05","23","34","36", "48"};
    string[] SlothNumbers = { "20", "21", "22", "27", "35" };
    string[] TigerNumbers = { "24", "28", "32", "26", "39" };
    string[] WolfNumbers = { "01", "58", "41", "42", "43" };
    // Update is called once per frame
}
