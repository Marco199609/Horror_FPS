using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Book : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _requiredKey, _closedBook, _openBook, _opaqueKeyHole, _glowingKeyHole;
    [SerializeField] private Trigger_ActivateGameObject _activateTreeCrowTombstone, _activateZombie, _activateLevelLoaderTrigger, _activateHallLight;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _rotateX, _rotateY, _rotateZ, _locked = true;
    [SerializeField] private GameObject _diaryTextUIEnglish, _diaryTextUISpanish;
    private bool _inspectableOnly = false;
    public void AssignInStateLoader()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        if (playerController.Inventory.SelectedItem() == _requiredKey)
        {
            playerController.Inventory.Remove(_requiredKey);
            Destroy(_requiredKey);

            _closedBook.SetActive(false);
            _openBook.SetActive(true);
            _locked = false;
            _activateZombie.TriggerBehaviour(0);
            _activateLevelLoaderTrigger.TriggerBehaviour(0);
            _activateHallLight.TriggerBehaviour(0);
            _inspectableOnly = true;
        }
        else if (_locked)
        {
            TriggerActions();
            _audioSource.PlayOneShot(_audioClip);
            _opaqueKeyHole.SetActive(false);
            _glowingKeyHole.SetActive(true);
            playerController.StressControl.AddStress();
        }

        StartCoroutine(DeactivateCollider(GetComponent<Collider>(), 1));
    }
    private IEnumerator DeactivateCollider(Collider collider, float delay)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }

    private void Update()
    {
        if (!_locked && PlayerController.Instance.PlayerInspect.Inspecting() && PlayerController.Instance.PlayerInspect.CurrentInspectable() == gameObject)
        {
            if(DialogueSystem.Instance.DialogueData.English)
                _diaryTextUIEnglish.SetActive(true);
            else if(DialogueSystem.Instance.DialogueData.Spanish)
                _diaryTextUISpanish.SetActive(true);
        }
        else
        {
            _diaryTextUIEnglish.SetActive(false);
            _diaryTextUISpanish.SetActive(false);
        }
    }

    public bool[] InteractableNonInspectableOrInspectableOnly()
    {
        bool nonInspectable = false;

        bool[] interactableType = new bool[] { nonInspectable, _inspectableOnly };

        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };

        return rotateXYZ;
    }

    public void TriggerActions()
    {
        _activateTreeCrowTombstone.TriggerBehaviour(0);
    }
}
