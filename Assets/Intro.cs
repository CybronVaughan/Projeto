using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour
{
    float timeLeft = 15.0f;
    public AudioClip Bloops;
    public GameObject Camera;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Player.GetComponent<PlayerController>().PlaySound(Bloops, Camera);
            SceneManager.LoadScene("DS");
        }
    }
}