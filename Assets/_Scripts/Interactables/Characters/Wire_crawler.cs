using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_crawler : MonoBehaviour, IInteractable, ITriggerAction
{
    [SerializeField] private int _id;
    [SerializeField] private Material _wireCrawlerMaterial;
    [SerializeField] private Renderer _wireCrawlerRenderer;
    [SerializeField] private AudioSource _wireCrawlerAudioSource, _muffledTalkAudioSource;
    [SerializeField] private AudioClip _lastMuffledTalkClip, _tensionBuildClip;

    private PlayerController _playerController;

    private float _emissionIntensity;
    private bool _enableEmission = false;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    private void Start()
    {
        _wireCrawlerMaterial.SetVector("_EmissionColor", new Vector4(1, 1, 1, 1) * 1/10);
    }

    public void Interact(PlayerController playerController)
    {
        _enableEmission = true;
        _muffledTalkAudioSource.Stop();
        _wireCrawlerAudioSource.PlayOneShot(_lastMuffledTalkClip);
        _wireCrawlerAudioSource.clip = _tensionBuildClip;
        _wireCrawlerAudioSource.Play();

        _playerController = playerController;
        _playerController.FreezePlayerMovement = true;
        _muffledTalkAudioSource.Stop();
        PlayerController.Instance.StressControl.AddStress();

        Collider[] colliders = gameObject.GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }

    private void Update()
    {
        if (_enableEmission)
        {
            _emissionIntensity += Time.deltaTime;
            _wireCrawlerMaterial.SetVector("_EmissionColor", new Vector4(1, 1, 1, 1) * _emissionIntensity);

            if (!_wireCrawlerAudioSource.isPlaying)
            {
                gameObject.GetComponent<Trigger_LevelLoader>().TriggerAction(0);
            }
        }
    }

    public bool[] InteractableType()
    {
        bool nonInspectable = true;
        bool inspectableOnly = false;

        bool[] interactableType = new bool[] { nonInspectable, inspectableOnly };

        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { false, false, false };

        return rotateXYZ;
    }

    public void TriggerActions()
    {
        throw new System.NotImplementedException();
    }


    #region Triggers
    public void TriggerAction(float triggerDelay)
    {
        StartCoroutine(Trigger(triggerDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);
       _muffledTalkAudioSource.Play();
        PlayerController.Instance.StressControl.AddStress();
    }
    #endregion
}
