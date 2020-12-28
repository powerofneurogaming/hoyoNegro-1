using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Start is called before the first frame update

    int x;
    int y;
    int z;

    void Start()
    {
        x = Random.Range(-5, 5);
        y = Random.Range(-15, 15);
        z = Random.Range(-5, 5);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = new Vector3(x, y, z) * Time.deltaTime;
        transform.Rotate(rot);
    }
}
