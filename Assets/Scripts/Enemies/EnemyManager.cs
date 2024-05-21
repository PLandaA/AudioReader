using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] AudioClip hitAudio, nearToPlayerAudio;

    float damage ,speed, arrivalRange;
    float currentHealth;

    Rigidbody rb;
    GameObject player;
    Animator anim;
    UIControllerTargetIndicator ui;
    // [SerializeField] GameObject hitEffect;


    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        speed = 5.0f;
        arrivalRange = 3.0f;
        damage = 10.0f;

		ui = FindObjectOfType<UIControllerTargetIndicator>();
		if (ui == null) {
			ui = GameObject.Find("GameManager").GetComponent<UIControllerTargetIndicator>();
		}

		if (ui == null) Debug.LogError("No UIController component found");

		ui.AddTargetIndicator(this.gameObject);

	}

  
    void FixedUpdate () {
        seek(player.transform.position);
        arrival(player.transform.position);
    }

	private void Update () {
        checkForDeath();
        destroyAll();
    }



	public void damagePlayer () {
        player.GetComponent<PlayerManager>().Hit(damage);
        SoundManager.instance.setSound(nearToPlayerAudio);
    }

    public void TakeDamage (float dmg, Vector3 hitPos, Vector3 hitNormal) {
       // Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));
        currentHealth -= dmg;
        SoundManager.instance.setSoundDifferentVolume(hitAudio, 0.15f);
        anim.SetBool("Hurted", true);
    }

    public void TakeShockwaveDamage (float damage) {
        currentHealth -= damage;
    }

 

    void seek (Vector3 p_target) {
        transform.LookAt(p_target);
        rb.velocity = transform.forward * speed;
        anim.SetBool("Attacking", false);
        anim.SetBool("Hurted", false);

        if (rb.mass > 1) {
            rb.velocity /= rb.mass;
        }


    }


    void arrival (Vector3 p_target) {
        Vector3 targetCopy = p_target;
        float arrivalDistance = Vector3.Distance(transform.position, targetCopy);
        if (arrivalDistance < arrivalRange) {
            rb.velocity /= (arrivalRange / (arrivalRange - (arrivalRange - arrivalDistance)));
            anim.SetBool("Attacking", true);
            anim.SetBool("Hurted", false);
        }
        if (arrivalDistance <= 2.0f) {
            rb.velocity = Vector3.zero;
            anim.SetBool("Attacking", true);
            anim.SetBool("Hurted", false);

        }



    }
    void checkForDeath () {
        if (currentHealth <= 0) {
            speed = 0;
            anim.SetBool("Dying", true);
            anim.SetBool("Flying", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Hurted", false);
            StartCoroutine(Die());
        }
        

        
    }

    IEnumerator Die () {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);

    }

    void destroyAll () {
        if (GameManager.instance.isLevel2State()) {
            Destroy(gameObject);
		} 
	}

}

