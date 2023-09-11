using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Behaviour_PillbottleAberration : MonoBehaviour, ITrigger
{
    [SerializeField] private PostProcessVolume _postProcessVolume;
    [SerializeField] private float _effectSpeed = 0.5f;
    [SerializeField] private bool _applyEffect;
    private ChromaticAberration _aberration;


    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        GetComponent<AudioSource>().Play();
        ChromaticAberration tmp;
        if (_postProcessVolume.profile.TryGetSettings<ChromaticAberration>(out tmp))
        {
            _aberration = tmp;
        }

        _applyEffect = true;
    }

    void Update()
    {
     if(_applyEffect)
        {
            _effectSpeed -= Time.deltaTime;

            if (_effectSpeed <= -10f)
            {
                if (_aberration.intensity.value > 0) _aberration.intensity.value -= Time.deltaTime / 2;
                else
                    _applyEffect = false;
            }
            else
            {
                PlayerController.Instance.StressControl.AddStress();
                if (_aberration.intensity.value < 1) _aberration.intensity.value += Time.deltaTime * 3;
            }
        }   
    }
}
