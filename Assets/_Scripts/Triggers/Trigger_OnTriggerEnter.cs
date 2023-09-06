using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_OnTriggerEnter : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private Collider _triggerCollider;  //Use if collider in scene
    [SerializeField] private string _triggerColliderTag; //Use if collider not in same scene
    [SerializeField] private GameObject[] _triggerObjects;
    [SerializeField] private float[] _triggerDelays;  //Delays trigger result if necessary
    [SerializeField] private bool _deactivateThis;



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
            

            for (int i = 0; i < _triggerObjects.Length; i++)
            {
                ITrigger trigger = _triggerObjects[i].GetComponent<ITrigger>();
                trigger.Trigger(_triggerDelays[i]);
            }
        }

        if(_deactivateThis)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
