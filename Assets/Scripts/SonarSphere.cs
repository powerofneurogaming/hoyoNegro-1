using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarSphere : MonoBehaviour
{
    // Start is called before the first frame update
    float maxRange = 100f;
    float expandTimer = 2;
    float timer = 0;
    void Start()
    {
        
    }
    float velocity;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.localScale = Mathf.Lerp(1, maxRange, timer / expandTimer)*Vector3.one;
        if (timer > expandTimer)
        {
            //ScannerInteraction.instance.PlaySonarSound();
            Destroy(gameObject);
        }
    }
}
