using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderImportLvl : MonoBehaviour
{

    private void Start()
    {
        OnScan();
    }
    public void OnScan()
    {
#if !UNITY_EDITOR
    MediaFrameQrProcessing.Wrappers.ZXingQrCodeScanner.ScanFirstCameraForQrCode(
        result =>
        {
          UnityEngine.WSA.Application.InvokeOnAppThread(() =>
          {
            Debug.Log("scaneado: " + result);
            if(result == null) OnScan();
            else{
                this.transform.parent.GetComponent<XMLReaderMusic>().ReadURL(result);
                this.transform.parent.GetComponent<StateImportManager>().NextState();
            }
          }, 
          false);
        },
        TimeSpan.FromSeconds(30));
#endif
    }
}

