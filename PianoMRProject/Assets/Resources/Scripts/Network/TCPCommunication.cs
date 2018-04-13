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
    private PianoDriver _pianodriver;


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
        Host = GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().IpAdrress;
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
    public void setPianoDriver(PianoDriver aux)
    {
        _pianodriver = aux;
    }
    public void setHost(String ip)
    {
        Host = ip;
    }

#if !UNITY_EDITOR
    private async void TCPclient(){
         Debug.Log("Initializing TCP Client...");
        try
        {
            _socket = new StreamSocket();
            Debug.Log("Connecting to "+Host+":"+Port);
            HostName serverHost = new HostName(Host);

            await _socket.ConnectAsync(serverHost, Port);
            //Connection Suscess
            Debug.Log("Connected");
            GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();
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
                try
                {
                    string response = await reader.ReadLineAsync();
                    Debug.Log("Revieced: " + response);
                    _pianodriver.RecievePianoData(response);
                }
                catch (Exception e)
                {
                    Debug.Log("Connection error! :" + e.ToString());
                }

            }

        }
        catch (Exception e)
        {
            //Handle exception here.  
            Debug.Log("Connection error! :"+e.ToString());
            //_connection = false;
        }
    }
#endif
    public Boolean isConnected()
    {
        return _connection;
    }
}
