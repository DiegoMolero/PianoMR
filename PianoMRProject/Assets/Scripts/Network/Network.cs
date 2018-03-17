using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if !UNITY_EDITOR
using Windows.Networking;
using Windows.Networking.Sockets;
using System.Threading.Tasks;
#endif
public class Network : MonoBehaviour {
    public String Port="8000";
    public String Host= "";
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
        _connection = false;
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
            Debug.Log("Connecting to "+Host+":"+Port);
            HostName serverHost = new HostName(Host);

            await _socket.ConnectAsync(serverHost, Port);
            //Connection Suscess
            Debug.Log("Connected");
            _connection = true;
            //Write data to the echo server.
            Stream streamOut = _socket.OutputStream.AsStreamForWrite();
            StreamWriter writer = new StreamWriter(streamOut);
            string request = "test";
            await writer.WriteLineAsync(request);
            await writer.FlushAsync();

            //Read data from the server.
            Stream streamIn = _socket.InputStream.AsStreamForRead();
            StreamReader reader = new StreamReader(streamIn);
            await readTCPDataAsync(reader);

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

#if !UNITY_EDITOR
    private async Task readTCPDataAsync(StreamReader reader)
    {

        while(_connection == true)
        {
            try
            {
                string response = await reader.ReadLineAsync();
                Debug.Log("Recieved: " + response);
            }
            catch (Exception e)
            {
                //Handle exception here.  
                Debug.Log("Connection error! :" + e.ToString());
                throw e;
            }
        }
    }
#endif
}
