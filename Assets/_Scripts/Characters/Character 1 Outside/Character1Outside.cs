using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Character1Outside : MonoBehaviour, IInteractable
{
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

    public void Behaviour()
    {
        if(!_dialogueAudioSource.isPlaying)
            _dialogueAudioSource.Play();
    }

    public void Interact()
    {
        Behaviour();
    }

    public string InteractableDescription()
    {
        return _description;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var newRotation = Quaternion.LookRotation(transform.position - _player.transform.position, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.z = 0.0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 80);
    }
}