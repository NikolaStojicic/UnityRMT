using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
/// <summary>
/// This probably should be tweaked, because now just works to prevent cars backend eachother and stop on trafficlight.
/// </summary>
public class CarNikolaController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TriggerBack")
            GetComponent<CarController>().m_Topspeed = 0f;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<CarController>().m_Topspeed = 50f;
    }
}
