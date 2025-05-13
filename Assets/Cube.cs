using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Material _material;
    private List<Platform> _encounteredObjects = new List<Platform>();

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Platform currentObject = collision.gameObject.GetComponent<Platform>();

        if (_encounteredObjects.Contains(currentObject) == false)
        {
            _encounteredObjects.Add(currentObject);
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        _material.color = new Color (Random.value, Random.value, Random.value, 255);
    }

    public void ResetObject()
    {
        _encounteredObjects.Clear();
    }
}
