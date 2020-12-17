using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBot : MonoBehaviour
{
    public Transform waypointsTrs;
    List<Vector3> waypoints = new List<Vector3>();
    int waypointIndex = 0;
    float TriggerDistance = 20;
    float PowerUpCooldown = 10;
    float PowerUpTimer = 0;
    List<GameObject> powerups = new List<GameObject>();
    public Transform PowerupSpawnTrs;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in waypointsTrs)
        {
            waypoints.Add(t.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //navigation
        if (Vector3.Distance(transform.position, waypoints[waypointIndex]) < TriggerDistance)
        {
            waypointIndex++;
            waypointIndex = waypointIndex % waypoints.Count;
        }
        else
        {
            Vector3 targetDir = waypoints[waypointIndex] - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1 * Time.deltaTime, 0);
            transform.LookAt(transform.position + newDir);
            
        }
        transform.position += transform.forward * Time.deltaTime * 5;
        PowerUpTimer += Time.deltaTime;

        //pop out power ups
        if (PowerUpTimer > PowerUpCooldown)
        {
            bool canSpawn = true;
            foreach(GameObject obj in powerups)
            {
                if (Vector3.Distance(transform.position, obj.transform.position) < 5)
                {
                    canSpawn = false;
                }
            }
            if (canSpawn)
            {
                GameObject newPower = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                newPower.transform.position = PowerupSpawnTrs.position;
                powerups.Add(newPower);
                PowerUpTimer = 0;
            }

        }



    }
}
