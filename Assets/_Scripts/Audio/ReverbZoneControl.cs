using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZoneControl : MonoBehaviour
{
    [SerializeField] private AudioReverbZone _reverbZone;
    [SerializeField] private string _colliderTag;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_colliderTag))
        {
            _reverbZone.enabled = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_colliderTag))
        {
            _reverbZone.enabled = false;
        }
    }
}
