using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare_KitchenDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpscareClip;

    private bool _alreadyJumpscared = false;
    public string ActionDescription()
    {
        return "";
    }

    public bool InspectableOnly()
    {
        return false;
    }

    public void Interact(PlayerController playerController)
    {
        print("kakaka");

        if (!_alreadyJumpscared)
        {
            _audioSource.PlayOneShot(_jumpscareClip);
            _alreadyJumpscared = true;
            print("jaa");
        }

    }

    private void Update()
    {
        
    }

    public string InteractableDescription()
    {
        return "";
    }

    public bool NonInspectable()
    {
        return true;
    }

    public bool PassRotateX()
    {
        return false;
    }

    public bool PassRotateY()
    {
        return false;
    }

    public bool PassRotateZ()
    {
        return false;
    }
}
