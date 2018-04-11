using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{
  public Transform textMeshObject;

  private void Start()
  {
    this.textMesh = this.textMeshObject.GetComponent<TextMesh>();
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
              GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().IpAdrress = result;
            GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().ChangeState(StageManager.State.PianoConnection);
          }, 
          false);
        },
        TimeSpan.FromSeconds(30));
#endif
    }
    public void OnRun()
  {
    this.textMesh.text = "running forever";

#if !UNITY_EDITOR
    MediaFrameQrProcessing.Wrappers.ZXingQrCodeScanner.ScanFirstCameraForQrCode(
        result =>
        {
          UnityEngine.WSA.Application.InvokeOnAppThread(() =>
          {
            this.textMesh.text = $"Got result {result} at {DateTime.Now}";
          }, 
          false);
        },
        null);
#endif
  }
  public void OnReset()
  {
    this.textMesh.text = "say scan or run to start";
  }
  TextMesh textMesh;
}
