using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Behaviour_PillbottleAberration : MonoBehaviour, ITrigger
{
    [SerializeField] private float _effectDuration = 10f;
    [SerializeField] private GameObject[] _lights;
    [SerializeField] private PostProcessVolume _postProcessVolume;

    private bool _applyEffect;
    private float _currentEffectTimer;
    private ChromaticAberration _aberration;


    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        _lights[0].SetActive(false);
        _lights[1].SetActive(false);
        GetComponent<AudioSource>().Play();

        ChromaticAberration tmp;
        if (_postProcessVolume.profile.TryGetSettings<ChromaticAberration>(out tmp))
        {
            _aberration = tmp;
        }

        _currentEffectTimer = _effectDuration;
        _applyEffect = true;
    }

    void Update()
    {
        if(_applyEffect)
        {
            _currentEffectTimer -= Time.deltaTime;

            if(_currentEffectTimer > 0)
            {
                PlayerController.Instance.StressControl.AddStress();
                if (_aberration.intensity.value < 1)
                {
                    _aberration.intensity.value += Time.deltaTime * 3;
                }

                if(_currentEffectTimer <= _effectDuration - 0.2f)
                {
                    _lights[0].SetActive(true);
                    _lights[1].SetActive(true);
                }
            }
            else if (_currentEffectTimer <= 0f)
            {
                if (_aberration.intensity.value > 0) _aberration.intensity.value -= Time.deltaTime / 2;
                else
                    _applyEffect = false;
            }
        }   
    }
}
