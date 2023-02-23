using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombinable
{
    public GameObject objectRequired { get; set; }

    public void Combine();
}