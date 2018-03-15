using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if !UNITY_EDITOR
using Windows.Networking.Sockets;
#endif
public class Network : MonoBehaviour {
    public String Port="8000";
    public String Host= "";
#if !UNITY_EDITOR
    StreamSocket _socket;
#endif
    // Use this for initialization
#if UNITY_EDITOR
    void Start () {
		
	}
#endif
#if !UNITY_EDITOR
    async void Start()
    {
        TCPclient();
    }
#endif
	// Update is called once per frame
	void Update () {
		
	}

#if !UNITY_EDITOR
    private async void TCPclient(){
         Debug.Log("Initializing TCP Client...");
        try
        {
            _socket = new StreamSocket();
            Debug.Log("Connecting to "+Host);
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(Host);

            await _socket.ConnectAsync(serverHost, Port);

            //Write data to the echo server.
            Stream streamOut = _socket.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            string request = "test";
            await writer.WriteLineAsync(request);
            await writer.FlushAsync();

            //Read data from the echo server.
            Stream streamIn = _socket.InputStream.AsStreamForRead();
            StreamReader reader = new StreamReader(streamIn);
            string response = await reader.ReadLineAsync();
        }
        catch (Exception e)
        {
            //Handle exception here.  
            Debug.Log(e.ToString());
        }
    }
#endif
}
