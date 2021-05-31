using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStatusUpdate : MonoBehaviour
{
    Light child;
    MeshRenderer lampRender;
    private void Awake()
    {
        child = GetComponent<Light>();
        lampRender = GetComponent<MeshRenderer>();
        GameManager.ElectricityChanged += UpdateState;
    }

    private void UpdateState(bool status)
    {
        if (child != null)
            child.enabled = status;
        if (lampRender != null)
            lampRender.material = status ?
            GameManager.Instance.LampOn : GameManager.Instance.LampOff;
    }

    private void OnDestroy()
    {
        GameManager.ElectricityChanged -= UpdateState;
    }
}