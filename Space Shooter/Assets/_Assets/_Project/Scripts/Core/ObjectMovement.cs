using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Ships;

public class ObjectMovement : MonoBehaviour
{
    public float velocity;
    Rigidbody rb;
    Animator anim;
    public float life;

    public bool enabled;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    public void Init()
    {
        enabled = true;
        gameObject.SetActive(true);
    }

    public void Finish()
    {
        enabled = false;
        gameObject.SetActive(false);
    }

    public void Update()
    {        
        transform.Rotate(0, 0, velocity * Time.deltaTime);
        rb.velocity = new Vector3(0, 1, 0) * velocity * Time.deltaTime;

        Vector3 bulletLimit = Camera.main.WorldToViewportPoint(transform.position);
        if (bulletLimit.y >= 2 || bulletLimit.y <= 0)
        {
            Finish();
        }
 
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerUp" && tag == "Enemy")
        {
            return;
        }

        PlayerShip player = other.GetComponent<PlayerShip>();

        if (player == null)
        {
            return;
        }

        player.currentHealth += life;
        anim.SetBool("destroyed", true);

        if(player.currentHealth >= player.maxHealth)
        {
            player.currentHealth = player.maxHealth;
        }

        StartCoroutine(DestroyedObject());

    }

    IEnumerator DestroyedObject()
    {
        yield return new WaitForSeconds(0.5f);
        Finish();
    }

}
