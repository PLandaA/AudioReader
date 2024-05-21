using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyAI : MonoBehaviour {

    Animator anim;
    NavMeshAgent navMeshAgent;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 [] randomPos;
    [SerializeField] AudioClip damageSound;

      

    float maxHealth = 100.0f;


    [SerializeField] float currentHealth;


    void Start () {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        InvokeRepeating("damagePlayerSound", 1, 5);


    }


	private void FixedUpdate () {
        seekPlayer();
        checkForDeath();
        attackPlayer();
    }

	void seekPlayer () {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
        anim.SetBool("Running", true);
        anim.SetBool("Attacking", false);


    }

    void attackPlayer () {
        float distanceBetweenEnemyAndPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceBetweenEnemyAndPlayer < navMeshAgent.stoppingDistance) {
            stopToAttack();
            
		} else {
            seekPlayer();

        }
    }

    void stopToAttack () {
        navMeshAgent.isStopped =  true;
        anim.SetBool("Running", false);
        anim.SetBool("Attacking", true);
    }

    void checkForDeath () {
        if (currentHealth <= 0) {
            navMeshAgent.speed = 0;
            anim.SetBool("Dying", true);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            StartCoroutine(Die());

        }




    }

    IEnumerator Die () {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        resetInfo();
    }


    public void damagePlayerSound () {
        SoundManager.instance.setSound(damageSound);


    }

    public void TakeDamage (float dmg, Vector3 hitPos, Vector3 hitNormal) {
        // Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));
        currentHealth -= dmg;
    }

    void resetInfo () {
		if (!gameObject.activeSelf) {
			foreach (var positions in randomPos) {
                transform.position = randomPos [Random.Range(0, randomPos.Length)];
            }
            currentHealth = maxHealth;
        }
	}

}
