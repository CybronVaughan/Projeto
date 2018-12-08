using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLabyrinth : MonoBehaviour {

    public GameObject LabyrinthDoor;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LabyrinthDoor.GetComponent<Animation>().Play();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
