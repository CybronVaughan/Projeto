using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAttack : MonoBehaviour
{
    public GameObject Enemy;
    private string animtrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animtrigger = Enemy.GetComponent<GolemAttackScript>().anim;
            Debug.Log(animtrigger);
            Enemy.GetComponent<Animator>().SetBool(animtrigger, false);
            Enemy.GetComponent<Animator>().SetBool("punch", true);

            //Enemy.GetComponent<Animator>().SetBool("punch", true);
            //Enemy.GetComponent<GolemAttackScript>().GolemAnima("punch");
        }
    }
}
