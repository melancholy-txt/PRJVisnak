using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    //vlastnosti zbrane
    public float damage, timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    int bulletsLeft, bulletsShot, currentMagazineSize = 80;

    //bools 
    bool shooting, readyToShoot, reloading;

    //reference
    public Camera fpsCam;
    public ParticleSystem muzzleflash;
    public GameObject impactEffectSurface, impactEffectEnemy, player;
    public AudioSource shootSound, reloadSound, cockingSound;

    public Text ammoDisplay;


     private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        ammoDisplay.text = bulletsLeft / bulletsPerTap+ "/" + currentMagazineSize / bulletsPerTap ;
    }
    void Update()
    {
        // Shoot();

        // Debug.Log("GunScript is working");
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if(Input.GetButtonDown("Fire1") && readyToShoot && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }

    }

    private void Shoot()
    {
        muzzleflash.Play();
        shootSound.Play();
        readyToShoot = false;

        //Spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        if (Physics.Raycast(fpsCam.transform.position, direction, out RaycastHit hit, range))
        {
            // Debug.Log(hit.transform.name);
            // Debug.DrawLine(fpsCam.transform.position, hit.point, Color.green, range);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Instantiate(impactEffectEnemy, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(impactEffectSurface, hit.point, Quaternion.LookRotation(hit.normal));
            }  

        }

        bulletsLeft--;
        bulletsShot--;

        Invoke(nameof(ResetShot), timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0){
            cockingSound.Play();
            Invoke(nameof(Shoot), timeBetweenShots);
        }
        //push player back a small amount
        player.transform.position -= player.transform.forward * 0.1f;
        // player.transform.position += player.transform.up * 0.5f;


        ammoDisplay.text = bulletsLeft/bulletsPerTap + "/" + currentMagazineSize/bulletsPerTap;
        
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        if(currentMagazineSize >= magazineSize){
            reloading = true;
            reloadSound.Play();
            Invoke(nameof(ReloadFinished), reloadTime);
        }
        else
        {
            StartCoroutine(AmmoDisplayFlash());         
        }
    }

    private IEnumerator AmmoDisplayFlash()
    {
        ammoDisplay.text = "Out of Ammo";
        ammoDisplay.color = Color.red;
        yield return new WaitForSeconds(1);
        ammoDisplay.text = "0/0";
        ammoDisplay.color = Color.white;
        
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        currentMagazineSize -= magazineSize;
        ammoDisplay.text = bulletsLeft/bulletsPerTap + "/" + currentMagazineSize/bulletsPerTap;
        
    }
}
