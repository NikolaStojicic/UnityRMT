using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI avg;

    [SerializeField]
    public TMP_InputField input;

    private int numOfCars;
    private float timeSpentOfAllCars;


    private AllLightsController allLightsController;
    private TrafficController trafficController;
    private List<string> listaAutaSample;

    private void Start()
    {
        input.text = "18.188.14.65:18138";
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
        string ip = input.text.Split(':')[0];
        int port = int.Parse(input.text.Split(':')[1]);

        Debug.Log(ip + port);
        //GameObject.FindObjectOfType<TCPListener>().makeConnection(ip, port);
        GameObject.FindObjectOfType<TCPListener>().makeConnection(ip, port);
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
