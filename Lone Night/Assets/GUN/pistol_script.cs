using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol_script : MonoBehaviour
{
    // ____________ EL QUE HEU DE FER ES A LSCRIPT DE UNITY DE GRAB ASSIGNAR UN BOTÓ QUE QUAN L'APRETIS CRIDI LA FUNCIÓ SHOOTBULLET() 
    public GameObject bulletPrefab;
    public float realoadTime;
    public int magazineCapacity;
    public float timeBtwnBullets;
    [SerializeField] private int currentAmmountOfBullets;
    public Transform firePoint;
    [SerializeField] private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmountOfBullets = magazineCapacity;
        canShoot = true;
    }
    private void Update()
    {
        if (currentAmmountOfBullets <= 0)
        {
            StartCoroutine(Reload());
        }
    }
    public IEnumerator ShootBullet()
    {
        if(canShoot)
        {
            if (currentAmmountOfBullets >= 0)
            {              
                Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                currentAmmountOfBullets--;
                yield return new WaitForSeconds(timeBtwnBullets);
            }
        }        
    }

    public IEnumerator Reload()
    {
        canShoot = false;
        yield return new WaitForSeconds(realoadTime);
        currentAmmountOfBullets = magazineCapacity;
        canShoot = true;
    }
}
