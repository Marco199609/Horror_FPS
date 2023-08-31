using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Book : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _requiredKey, _closedBook, _openBook;
    [SerializeField] private Trigger_ActivateGameObject _activateTree, _activateCrow, _activateTombstone, _activateZombie, _activateLevelLoaderTrigger, _activateHallLight;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _rotateX, _rotateY, _rotateZ, _locked = true;
    private bool _inspectableOnly = false;
    public void AssignInStateLoader()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(PlayerController playerController)
    {
        if (playerController.Inventory.SelectedItem() == _requiredKey)
        {
            playerController.Inventory.Remove(_requiredKey);
            Destroy(_requiredKey);

            _closedBook.SetActive(false);
            _openBook.SetActive(true);
            _locked = false;
            _activateZombie.TriggerAction(0);
            _activateLevelLoaderTrigger.TriggerAction(0);
            _activateHallLight.TriggerAction(0);
            _inspectableOnly = true;
        }
        else if (_locked)
        {
            TriggerActions();
            _audioSource.PlayOneShot(_audioClip);
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

    public bool[] InteractableType()
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
        _activateCrow.TriggerAction(0);
        _activateTombstone.TriggerAction(0);
        _activateTree.TriggerAction(0);
    }
}
