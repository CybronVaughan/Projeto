using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour
{
    public GameObject Enemy;

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
        if (other.gameObject == Enemy && Enemy.GetComponent<GolemAttackScript>().GolemHealth > 0)
        {
            Enemy.GetComponent<GolemAttackScript>().GolemDamage();
        }
    }
}
