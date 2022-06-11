using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m4a2 : MonoBehaviour
{
    public float speed = 20;
    public float fireRate = 0.25f;
    public float nextFire = 0;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public ParticleSystem shoteffect;

    private bool fire = false;

    public void Update()
    {
        if (fire == true && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shoteffect.Play();
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
            audioSource.PlayOneShot(audioClip);
            Destroy(spawnedBullet, 2);
        }
    }

    public void Fire()
    {
        fire = true;
    }

    public void NotFire()
    {
        fire = false;
    }
}
