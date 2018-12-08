using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour {

    public GameObject MasterHalberd;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MasterHalberd.GetComponent<Animation>().Play();
            MasterHalberd.GetComponent<Animation>().playAutomatically = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}
