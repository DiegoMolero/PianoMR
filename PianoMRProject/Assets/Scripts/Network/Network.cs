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
    public String _UDP_PORT = "8001";
#if !UNITY_EDITOR
    DatagramSocket _socket;
#endif
    // Use this for initialization
#if UNITY_EDITOR
    void Start () {
		
	}
#endif
#if !UNITY_EDITOR
    async void Start()
    {
        UDPServer();
        //TCPclient();
    }
#endif
	// Update is called once per frame
	void Update () {
		
	}
#if !UNITY_EDITOR
     private async void UDPServer(){
        Debug.Log("Initializing UDP Server in port: "+ _UDP_PORT + "...");
        _socket = new DatagramSocket();
        _socket.MessageReceived += Socket_MessageReceived;
        try
        {
            await _socket.BindEndpointAsync(null, _UDP_PORT);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log(SocketError.GetStatus(e.HResult).ToString());
            return;
        }

        Debug.Log("exit start");
    }
#endif

#if !UNITY_EDITOR
    private async void Socket_MessageReceived(Windows.Networking.Sockets.DatagramSocket sender,
        Windows.Networking.Sockets.DatagramSocketMessageReceivedEventArgs args)
    {
        //Read the message
        Debug.Log("Reading message...");
        Stream streamIn = args.GetDataStream().AsStreamForRead();
        StreamReader reader = new StreamReader(streamIn);
        string message = await reader.ReadLineAsync();

        Debug.Log("MESSAGE: " + message);
    }
#endif

#if !UNITY_EDITOR
    private async void TCPclient(){
         Debug.Log("Initializing TCP Client...");
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
            Debug.Log(e.ToString());
        }
    }
#endif
}
