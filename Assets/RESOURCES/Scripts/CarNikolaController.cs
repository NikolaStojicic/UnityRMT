using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Utility;
/// <summary>
/// This probably should be tweaked, because now just works to prevent cars backend eachother and stop on trafficlight.
/// </summary>
public class CarNikolaController : MonoBehaviour
{
    private float timeSpentOnScene;
    private void Start()
    {
        timeSpentOnScene = 0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "TriggerBack")
            GetComponent<CarController>().m_Topspeed = 50f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "TriggerBack")
            GetComponent<CarController>().m_Topspeed = 0.0000001f;
    }
    private void OnDestroy()
    {
        GameObject.FindObjectOfType<UI>().addTime(timeSpentOnScene);
    }

    private void Update()
    {
        timeSpentOnScene += Time.deltaTime;
    }
}
