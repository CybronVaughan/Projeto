using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeScript : MonoBehaviour {

    public GameObject Camera;
    public AudioClip PeacefulMoments;
    private int Trigger = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Trigger == 0)
        {
            Camera.GetComponent<AudioSource>().mute = true;
            Trigger++;
            //other.GetComponent<PlayerController>().PlaySound(PeacefulMoments, other.gameObject);
        }
    }

    // Use this for initialization
    void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
