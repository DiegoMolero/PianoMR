using HoloToolkit.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

#if !UNITY_EDITOR
using Windows.Networking;
using Windows.Networking.Sockets;
using System.Threading.Tasks;
#endif


public class TCPCommunication : Singleton<TCPCommunication>
{
    [Tooltip("Port for connecting to the server")]
    public String Port="8000";
    [Tooltip("IP of the server. (Given by QR Code)")]
    public String Host= "";
    [Tooltip("Function to invoke at incoming packet")]
    public PianoMessageEvent pianoEvent = null;


    private Boolean _connection;
#if !UNITY_EDITOR
    StreamSocket _socket;
#endif
    // Use this for initialization
#if UNITY_EDITOR
    void Start () {
		_connection = false;
	}
#endif
#if !UNITY_EDITOR
    async void Start()
    {
        if (pianoEvent == null)
        {
            Debug.Log("Piano event needs a function to invoke for sending incoming data");
        }
        pianoEvent.AddListener(GameObject.FindGameObjectWithTag("PianoDriver").GetComponent<PianoDriver>().RecievePianoData); //Subscribe Piano event data
        StartPianoConnection();
        _connection = false;
    }
#endif
    // Update is called once per frame
    void Update () {
		
	}
#if !UNITY_EDITOR
    public async void StartPianoConnection()
    {
        TCPclient();
    }
#endif

#if !UNITY_EDITOR
    private async void TCPclient(){
         Debug.Log("Initializing TCP Client...");
        try
        {
            _socket = new StreamSocket();
            Debug.Log("Connecting to "+Host+":"+Port);
            HostName serverHost = new HostName("161.67.106.43");

            await _socket.ConnectAsync(serverHost, Port);
            //Connection Suscess
            Debug.Log("Connected");
            _connection = true;
            //Write data to the echo server.
            /*
            Stream streamOut = _socket.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            string request = "";
            await writer.WriteLineAsync(request);
            await writer.FlushAsync();
            */
            //Read data from the server.
            Stream streamIn = _socket.InputStream.AsStreamForRead();
            StreamReader reader = new StreamReader(streamIn);
            await reader.ReadLineAsync();
            while (_connection == true)
            {
                string response = await reader.ReadLineAsync();
                Debug.Log("Revieced: " + response);
                pianoEvent.Invoke(response);
            }

        }
        catch (Exception e)
        {
            //Handle exception here.  
            Debug.Log("Connection error! :"+e.ToString());
            _connection = false;
        }
    }
#endif
    public Boolean isConnected()
    {
        return _connection;
    }
}
