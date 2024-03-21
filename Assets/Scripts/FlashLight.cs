using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private Light flashLight;

    private void Start()
    {
        flashLight = GetComponent<Light>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            flashLight.enabled = !flashLight.enabled;
        }
    }
}
