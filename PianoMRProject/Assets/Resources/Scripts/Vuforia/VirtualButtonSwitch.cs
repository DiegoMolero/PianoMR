using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButtonSwitch : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject vbButton;
    public GameObject piano;

    // Use this for initialization
    void Start()
    {
        vbButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Debug.Log("BOTON PULSADO!");
        /*
        GameObject aux = Instantiate(piano);
        aux.name = aux.transform.name.Replace("(Clone)", "");
        aux.transform.position = this.transform.position;
        aux.transform.rotation = this.transform.rotation;
        GameObject.FindGameObjectWithTag("AppManager").GetComponent<StageManager>().NextState();*/
    }

    // This method is necessary to be here because of the IVirtualButtonEventHandler interface
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

    }
}
