using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_OnTriggerEnter : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Collider _triggerCollider;  //Use if collider in scene
    [SerializeField] private string _triggerColliderTag; //Use if collider not in same scene
    [SerializeField] private float _delay;  //Delays trigger result if necessary
    [SerializeField] private bool _deactivateThis;  

    private ITriggerAction _trigger;

    public void AssignInStateLoader()
    {
        if (_id != 0) SceneStateLoader.Instance.objects.Add(_id, gameObject);
        else print("id is 0 in gameobject " + gameObject.name + "!");
    }

    private void OnEnable()
    {
        AssignInStateLoader();
    }

    private void OnDestroy()
    {
        if (_id != 0) SceneStateLoader.Instance.objects.Remove(_id);
    }

    private void Awake()
    {
        //Prevents blank tag error
        if(_triggerCollider != null)
            _triggerColliderTag = _triggerCollider.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == _triggerCollider || other.CompareTag(_triggerColliderTag))
        {
            _trigger = gameObject.GetComponent<ITriggerAction>();
            _trigger.TriggerAction(_delay);
        }

        if(_deactivateThis)
        {
            Destroy(this, _delay + 0.01f);
        }
    }
}
