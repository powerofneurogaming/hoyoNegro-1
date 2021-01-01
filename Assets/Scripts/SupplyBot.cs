using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBot : MonoBehaviour
{
    public Transform waypointsTrs;
    List<Vector3> waypoints = new List<Vector3>();
    int waypointIndex = 0;
    float TriggerDistance = 5;
    float PowerUpCooldown = 60;
    float PowerUpTimer = 0;
    List<GameObject> powerups = new List<GameObject>();
    public Transform PowerupSpawnTrs;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in waypointsTrs)
        {
            waypoints.Add(t.position);
        }
    }

    public float turnAmount = 0;
    public GameObject currTarget;
    public float turnrate;
    public float speed;
    public float scannerCooldown=60;
    public float collectorCooldown = 60;
    string[] dropSequence = new string[] { "scanner", "collector", "scanner", "scanner", "collector" };
    int dropIndex = 0;
    public GameObject ScannerPrefab;
    public GameObject CollectorPrefab;
    bool holdingPowerup;
    // Update is called once per frame
    void Update()
    {
        if (PowerupSpawnTrs.childCount == 0)
        {
            holdingPowerup = false;
        }
        if (!holdingPowerup)
        {
            //navigation
            if (Vector3.Distance(transform.position, waypoints[waypointIndex]) < TriggerDistance)
            {
                waypointIndex++;
                waypointIndex = waypointIndex % waypoints.Count;
            }
            currTarget = waypointsTrs.GetChild(waypointIndex).gameObject;

            Vector3 targetDir = waypoints[waypointIndex] - transform.position;
            turnAmount = Vector3.Cross(targetDir, transform.forward).y * -1 * turnrate;
            transform.Rotate(Vector3.up * turnAmount * Time.deltaTime);
            transform.position += transform.forward * Time.deltaTime * speed;
            PowerUpTimer += Time.deltaTime;
        }

        if (ScannerInteraction.instance.stage >= 0)
        {
            //pop out power ups
            if (PowerUpTimer > PowerUpCooldown)
            {
                dropIndex = dropIndex % dropSequence.Length;
                var obj = Instantiate(dropSequence[dropIndex] == "scanner" ? ScannerPrefab : CollectorPrefab, PowerupSpawnTrs.position, Quaternion.identity);
                obj.transform.SetParent(PowerupSpawnTrs);
                holdingPowerup = true;
                dropIndex++;
                PowerUpTimer = 0;
            }
        }
    }
}
