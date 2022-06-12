using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 10;
    public float fireRate = 0.25f;
    public float nextFire = 0;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public ParticleSystem shooteffect;

    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shooteffect.Play();
            GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
            spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
            audioSource.PlayOneShot(audioClip);
            Destroy(spawnedBullet, 2);
        }
    }
}
