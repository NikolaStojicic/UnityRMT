using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchNikola : MonoBehaviour
{
    private int index = 1;
    /// <summary>
    /// Switches between cameras by disabling them and enabling in order.
    /// </summary>
    public void nextCam()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
        transform.GetChild(index).gameObject.SetActive(true);
        index++;
        if (index == 5) index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            nextCam();
    }
}
