using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_crawler : MonoBehaviour, IInteractable
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject _playerLookingLight;
    [SerializeField] private Material _wireCrawlerMaterial;
    [SerializeField] private AudioSource _wireCrawlerAudioSource;

    private PlayerController _playerController;

    private float _emissionIntensity;
    private bool _enableEmission = false;

    private void Start()
    {
        _wireCrawlerMaterial.SetVector("_EmissionColor", new Vector4(1, 1, 1, 1) * 1/10);
    }

    public void Interact(PlayerController playerController)
    {

        _enableEmission = true;
        _wireCrawlerAudioSource.Play();

        _playerController = playerController;
        _playerController.FreezePlayerMovement = true;
    }

    private void Update()
    {
        if (_enableEmission)
        {
            _emissionIntensity += Time.deltaTime;
            _wireCrawlerMaterial.SetVector("_EmissionColor", new Vector4(1, 1, 1, 1) * _emissionIntensity);

            if (!_wireCrawlerAudioSource.isPlaying)
            {
                _playerLookingLight.SetActive(false);
                _playerController.FreezePlayerMovement = false;
                _levelManager.LoadHouseLevel();
            }
        }
    }


    public string ActionDescription()
    {
        return "";
    }

    public string InteractableDescription()
    {
        return "";
    }

    public bool InspectableOnly()
    {
        return false;
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
