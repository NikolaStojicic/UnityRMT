using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI status;
    [SerializeField]
    public TextMeshProUGUI avg;

    [SerializeField]
    public TextMeshProUGUI input;

    private int numOfCars;
    private float timeSpentOfAllCars;


    private AllLightsController allLightsController;
    private TrafficController trafficController;
    private List<string> listaAutaSample;

    private void Start()
    {
        numOfCars = 0;
        timeSpentOfAllCars = 0;
    }

    public void addTime(float time)
    {
        numOfCars++;
        timeSpentOfAllCars += time;
        avg.text = "Time spent on scene (avg): " + System.Math.Round((timeSpentOfAllCars / numOfCars), 2) + " sec.";
    }

    public void connectClicked()
    {
        status.text = "Status:\nConnceted to server: " + input.text + " successfuly!";
    }

    public void chkBox()
    {
        if (allLightsController == null)
            allLightsController = GameObject.FindObjectOfType<AllLightsController>();
        Toggle tg = GetComponent<Toggle>();
        var light = allLightsController.GetType().GetField("light" + tg.name);
        light.SetValue(allLightsController, tg.isOn);
    }

}
