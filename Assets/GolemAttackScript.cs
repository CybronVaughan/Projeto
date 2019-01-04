using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemAttackScript : MonoBehaviour {

    [HideInInspector] public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    public bool ready = false;
    public Animator animgolem;
    private string idle = "idle";
    private string walk = "walk";
    private string hit = "hit";
    private string punch = "punch";
    private string death = "death";
    public string anim = "idle";

    // Use this for initialization
    void Start() {
        target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
        animgolem = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }

        if (agent.velocity.magnitude >= 1f && !animgolem.GetBool(punch))
        {
            GolemAnima(walk);
        }
        else if (agent.velocity.magnitude < 1f && !animgolem.GetBool(punch))
        {
            GolemAnima(idle);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void GolemAnima(string animus)
    {
        animgolem.SetBool(anim, false);
        animgolem.SetBool(animus, true);
        anim = animus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
