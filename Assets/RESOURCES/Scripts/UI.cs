using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    private TrafficController trafficController;
    private static bool addOnce;
    private List<string> listaAutaSample;
    private void Start()
    {
        trafficController = GameObject.FindObjectOfType<TrafficController>();
        listaAutaSample = new List<string>();
        listaAutaSample.Add("Left");
        listaAutaSample.Add("Right");
        listaAutaSample.Add("Straight");
        if (!addOnce)
        {
            trafficController.spawnCarFromLightRandomDirections("Way3",10);
            trafficController.spawnCarFromLightRandomDirections("Way1",10);
            trafficController.spawnCarFromLightRandomDirections("Way4",10);
            trafficController.spawnCarFromLightRandomDirections("Way2",10);
        }
        addOnce = true;
    }


    public void addUp()
    {
        trafficController.spawnCarFromLight("Way3", listaAutaSample);
    }
    public void addBot()
    {
        trafficController.spawnCarFromLight("Way1", listaAutaSample);
    }
    public void addRight()
    {
        trafficController.spawnCarFromLight("Way4", listaAutaSample);
    }
    public void addLeft()
    {
        trafficController.spawnCarFromLight("Way2", listaAutaSample);
    }
}
