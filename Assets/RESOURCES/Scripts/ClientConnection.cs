using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using TMPro;

public class ClientConnection : MonoBehaviour
{
    private string ip;
    private int port;
    UI UIController;
    private TcpClient client = null;

    string message;
    int byteCount;
    NetworkStream stream;
    byte[] sendData;

    public void makeConnection(string ip, int port)
    {
        this.ip = ip;
        this.port = port;
        UIController = GameObject.FindObjectOfType<UI>();

        try
        {
            client = new TcpClient(ip, port);
            //UIController.status.text = "Status:\nConnceted to server: " + ip + ":" + port + " successfuly!";


            message = "Hello server!";

            byteCount = Encoding.ASCII.GetByteCount(message);
            sendData = new byte[byteCount];
            sendData = Encoding.ASCII.GetBytes(message);
        }
        catch (System.Net.Sockets.SocketException e)
        {
            Debug.Log(e.Message);
            Debug.Log("FEJLOVO SAM BURAZ");
          //  UIController.status.text = "Status:\nConnection cannot be established to server: " + ip + ":" + port;
        }
    }

    public void sendPicture(byte[] bytes)
    {
        try
        {

            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
        }
        catch (System.Exception)
        {
            client = null;

        }
    }

    private void Update()
    {
        //if (client != null)
        //{
        //    try
        //    {
        //        stream.Write(sendData, 0, sendData.Length);
        //    }
        //    catch (System.Exception)
        //    {
        //        client = null;
        //        UIController.status.text = "Status:\nConnection lost!";
        //    }
        //    Debug.Log("Uspesno poslata poruka");
        //}

    }
}
