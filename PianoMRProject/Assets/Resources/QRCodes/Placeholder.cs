using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{
    private StageManager AppManager;
  private void Start()
  {
    AppManager = GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>();
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
                AppManager.IpAdrress = result;
                AppManager.NextState();
            }
          }, 
          false);
        },
        TimeSpan.FromSeconds(30));
#endif
    }
}
