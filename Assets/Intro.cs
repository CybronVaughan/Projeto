using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour {

    public float LogoTime = 0;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        LogoTime +=Time.time;
        Debug.Log ("O tempo é: " + LogoTime.ToString("00"));
		
	}
}
