using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_DialogueTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private int _id;
    [SerializeField] private bool _nonInspectable, _inspectableOnly;

    [Header("Use only if inspectable")]
    [SerializeField] private bool _rotateX = true;
    [SerializeField] private bool _rotateY = true;
    [SerializeField] private bool _rotateZ = true;
    [SerializeField] private Vector3 _inspectableInitialRotation;

    [SerializeField] private Trigger_DialogueSystem[] _dialogueTriggers;

    private Collider _collider;
    private float _reactivateColliderTimer = 1f; //Prevents dialogue trigger overlap
    public void AssignInStateLoader()
    {
        SceneStateLoader.Instance.objects.Add(_id, gameObject);
    }

    public void Interact(PlayerController playerController, bool isInteracting, bool isInspecting)
    {
        StartCoroutine(DisableColliderTemporarily(_reactivateColliderTimer));

        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
        if(!_nonInspectable) playerController.PlayerInspect.Inspect(transform, rotateXYZ);
    }

    IEnumerator DisableColliderTemporarily(float timer)
    {
        if (_collider == null) _collider = GetComponent<Collider>();
        _collider.enabled = false;

        yield return new WaitForSeconds(timer);

        _collider.enabled = true;
    }

    public bool[] InteractableIsNonInspectableOrInspectableOnly()
    {
        bool[] interactableType = new bool[] { _nonInspectable, _inspectableOnly };
        return interactableType;
    }

    public bool[] RotateXYZ()
    {
        bool[] rotateXYZ = new bool[] { _rotateX, _rotateY, _rotateZ };
        return rotateXYZ;
    }

    public void TriggerActions()
    {
        int dialogueIndex = Random.Range(0, _dialogueTriggers.Length);
        _dialogueTriggers[dialogueIndex].TriggerBehaviour(0, false, false);
    }

    public Vector3 InspectableInitialRotation()
    {
        return _inspectableInitialRotation;
    }
}
