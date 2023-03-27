using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Character1Outside : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _nonInspectable = true;
    [SerializeField] private string _description;

    private GameObject _player;
    private AudioSource _dialogueAudioSource;

    private void Awake()
    {
        _dialogueAudioSource = GetComponent<AudioSource>();
    }
    public string ActionDescription()
    {
        return "Talk";
    }

    public void Interact(PlayerController playerController)
    {
        if (!_dialogueAudioSource.isPlaying)
            _dialogueAudioSource.Play();
    }

    public string InteractableDescription()
    {
        return _description;
    }

    void Update()
    {
        if(_player == null) _player = GameObject.FindWithTag("Player");

        var newRotation = Quaternion.LookRotation(transform.position - _player.transform.position, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.z = 0.0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 80);
    }

    public bool NonInspectable()
    {
        return _nonInspectable;
    }

    public bool InspectableOnly()
    {
        return false;
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
