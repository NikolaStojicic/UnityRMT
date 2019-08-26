using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ClientConnection : MonoBehaviour
{
    [SerializeField]
    UI ui;

    Thread t;

    private int port;
    private string ip;

    public byte[] dataToSend = null;

    public void makeConnection(string ip, int port)
    {
        this.port = port;
        this.ip = ip;
        t = new Thread(tcpListener);
        t.Start();
    }

    private void OnDestroy()
    {
        t.Abort();
    }


    public void tcpListener()
    {
        try
        {
            while (true)
            {
                string textToSend = DateTime.Now.ToString();
                TcpClient client = new TcpClient(this.ip, this.port);
                NetworkStream nwStream = client.GetStream();
                byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
                ui.statusUIThreadSafe = "Status:\nImage sent successfuly!";
                ui.isConnected = true;
                Debug.Log(dataToSend);
                if (dataToSend != null)
                {
                    nwStream.Write(dataToSend, 0, dataToSend.Length);
                    Debug.Log("Sent : " + dataToSend.Length);
                    nwStream.Close();
                    Thread.Sleep(10000);
                }
            }
        }

        catch (Exception e)
        {
            Debug.Log(e.Message);
            ui.isConnected = false;
            ui.statusUIThreadSafe = "Error:\nConnection lost, " + e.Message;
            t.Abort();
        }

    }
}
