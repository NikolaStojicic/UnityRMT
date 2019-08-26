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

    [SerializeField]
    public TextMeshProUGUI status;

    [SerializeField]
    public Button btn;

    private int numOfCars;
    private float timeSpentOfAllCars;


    private AllLightsController allLightsController;
    private TrafficController trafficController;
    private List<string> listaAutaSample;

    public bool isConnected;

    public string ip = null;
    public int port = 0;

    public string statusUIThreadSafe = @"Status:
Waiting for the connection...";

    private void Start()
    {
        input.text = "127.0.0.1:8000";
        numOfCars = 0;
        timeSpentOfAllCars = 0;
        isConnected = false;
    }

    public void addTime(float time)
    {
        numOfCars++;
        timeSpentOfAllCars += time;
        avg.text = "Time spent on scene (avg): " + System.Math.Round((timeSpentOfAllCars / numOfCars), 2) + " sec.";
    }

    public void connectClicked()
    {
        try
        {
            ip = input.text.Split(':')[0];
            port = int.Parse(input.text.Split(':')[1]);
            statusUIThreadSafe = "Status:\nConnecting to: " + ip + ":" + port + "...";
            isConnected = true;
            Debug.Log(ip + port);
            GameObject.FindObjectOfType<TCPListener>().makeConnection(ip, port + 1);
            GameObject.FindObjectOfType<ClientConnection>().makeConnection(ip, port);
        }
        catch (System.Exception)
        {
            statusUIThreadSafe = "Error:\nInvalid ip address!";
            isConnected = false;
        }
    }

    public void chkBox()
    {
        if (allLightsController == null)
            allLightsController = GameObject.FindObjectOfType<AllLightsController>();
        Toggle tg = GetComponent<Toggle>();
        var light = allLightsController.GetType().GetField("light" + tg.name);
        light.SetValue(allLightsController, tg.isOn);
    }

    private void LateUpdate()
    {
        status.text = statusUIThreadSafe;
        btn.interactable = !isConnected;
    }
}
