using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Network : MonoBehaviour {
    public String Port="8000";
    public String Host= "172.19.248.171";

    // Use this for initialization
#if UNITY_EDITOR
    void Start () {
		
	}
#endif
#if !UNITY_EDITOR
    async void Start()
    {
        try
        {
            Windows.Networking.Sockets.StreamSocket socket = new Windows.Networking.Sockets.StreamSocket();

            Windows.Networking.HostName serverHost = new Windows.Networking.HostName("172.19.248.171");

            string serverPort = Port;
            await socket.ConnectAsync(serverHost, serverPort);

            //Write data to the echo server.
            Stream streamOut = socket.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            string request = "test";
            await writer.WriteLineAsync(request);
            await writer.FlushAsync();

            //Read data from the echo server.
            Stream streamIn = socket.InputStream.AsStreamForRead();
            StreamReader reader = new StreamReader(streamIn);
            string response = await reader.ReadLineAsync();
        }
        catch (Exception e)
        {
            //Handle exception here.            
        }
    }
#endif
	// Update is called once per frame
	void Update () {
		
	}
}
