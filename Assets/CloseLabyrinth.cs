using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLabyrinth : MonoBehaviour {

    private int OpenInt = 0;

    public GameObject LabyrinthDoor;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && OpenInt == 0)
        {
            LabyrinthDoor.GetComponent<Animation>().Play();
            OpenInt++;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
