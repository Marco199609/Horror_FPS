using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_crawler : MonoBehaviour, IInteractable, ITrigger
{
    [SerializeField] private int _id;
    [SerializeField] private Material[] _wireCrawlerMaterial;
    [SerializeField] private AudioSource _wireCrawlerAudioSource, _muffledTalkAudioSource;
    [SerializeField] private AudioClip _tensionBuildClip, _chokingClip;
    [SerializeField] private Animator _crawlerAnimator;
    [SerializeField] private Light _wireCrawlerLight;

    private PlayerController _playerController;

    private float _emissionIntensity;
    private bool _enableEmission = false;

    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    private void Start()
    {
        for(int i =  0; i < _wireCrawlerMaterial.Length; i++)
        {
            _wireCrawlerMaterial[i].SetVector("_EmissionColor", new Vector4(1, 1, 1, 1) * 1 / 10);
        }
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        _enableEmission = true;
        _muffledTalkAudioSource.Stop();
        _wireCrawlerAudioSource.PlayOneShot(_chokingClip);
        _wireCrawlerAudioSource.clip = _tensionBuildClip;
        _wireCrawlerAudioSource.Play();

        _playerController = playerController;
        _playerController.FreezePlayerMovement = true;
        _muffledTalkAudioSource.Stop();
        PlayerController.Instance.StressControl.AddStress();
        _crawlerAnimator.SetBool("HavingSeizure", true);

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
            _wireCrawlerLight.intensity += _emissionIntensity / 100;

            for (int i = 0; i < _wireCrawlerMaterial.Length; i++)
            {
                _wireCrawlerMaterial[i].SetVector("_EmissionColor", new Vector4(1, 1, 1, 1) * _emissionIntensity);
            }

            if (!_wireCrawlerAudioSource.isPlaying)
            {
                gameObject.GetComponent<Trigger_LevelLoader>().TriggerBehaviour(0, false, false);
            }
        }
    }

    public bool[] InteractableNonInspectableOrInspectableOnly()
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
    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
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
