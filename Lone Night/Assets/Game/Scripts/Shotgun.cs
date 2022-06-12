using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public float speed = 10;
    public float fireRate = 1f;
    public float nextFire = 0;
    public GameObject bullet;
    public Transform barrel;
    public Transform barrel2;
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
            GameObject spawnedBullet2 = Instantiate(bullet, barrel2.position, barrel2.rotation);
            spawnedBullet2.GetComponent<Rigidbody>().velocity = speed * barrel2.forward;
            audioSource.PlayOneShot(audioClip);
            Destroy(spawnedBullet, 2);
            Destroy(spawnedBullet2, 2);
        }
    }
}
