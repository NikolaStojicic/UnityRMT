using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTrackTriggerControll : MonoBehaviour
{
    /// <summary>
    /// If vehicle reached end of its destionation destroy it.
    /// </summary>
    /// <param name="other">Vehicle</param>
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(other.gameObject);
    }
}
