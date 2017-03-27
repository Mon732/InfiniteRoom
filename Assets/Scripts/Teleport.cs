using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform target;
    public float offset;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " teleported " + other.name);
        other.gameObject.transform.position = target.position + (target.up * offset);
    }
}
