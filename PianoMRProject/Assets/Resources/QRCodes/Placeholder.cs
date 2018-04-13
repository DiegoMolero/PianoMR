using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
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
                GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().IpAdrress = result;
                GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();
            }
          }, 
          false);
        },
        TimeSpan.FromSeconds(30));
#endif
    }
}
