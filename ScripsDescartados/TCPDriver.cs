using System;
using UnityEngine;
#if !UNITY_EDITOR
using System.IO;
using Windows.Networking;
using Windows.Networking.Sockets;
#endif

public class TCPDriver : MonoBehaviour
{
    public String host = "127.0.0.1";
    public String port = "8000";
#if !UNITY_EDITOR

    async void Start()
    {
        Debug.Log("NOT Unity Editor");
        Debug.Log("Connecting with: "+host+" : "+port);
        var socket = new StreamSocket();
        var hostName = new HostName(this.host);
        await socket.ConnectAsync(hostName, port);
        using (var reader =
        new StreamReader(socket.InputStream.AsStreamForRead()))
        {
            var data = reader.ReadToEnd();
            Debug.Log(data.ToString());
        }
    }
#endif
#if UNITY_EDITOR
    void Start()
    {
        Debug.Log("Unity Editor");
    }
#endif
}