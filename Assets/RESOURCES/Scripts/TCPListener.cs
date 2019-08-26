using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class TCPListener : MonoBehaviour
{
    Thread t;
    [SerializeField]
    AllLightsController lc;

    [SerializeField]
    UI ui;

    private int port;
    private string ip;

    /// <summary>
    /// Starts new thread for controling traffic lights.
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
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
            string textToSend = DateTime.Now.ToString();
            TcpClient client = new TcpClient(this.ip, this.port);
            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);
            ui.statusUIThreadSafe = "Status:\nConnceted to server: " + ip + ":" + port + " successfuly!";
            ui.isConnected = true;
            while (true)
            {
                byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                string stringData = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
                int data = int.Parse(stringData);
                if (data == 0)
                {
                    lc.light2 = false;
                    lc.light4 = false;
                    Thread.Sleep(2000);
                    lc.light1 = true;
                    lc.light3 = true;
                }
                else if (data == 1)
                {
                    lc.light1 = false;
                    lc.light3 = false;
                    Thread.Sleep(2000);
                    lc.light2 = true;
                    lc.light4 = true;
                }
                Debug.Log("Received : " + data);
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
