using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    public int maxMagazineAmmo = 10; // Maximum ammo in a magazine
    public int maxTotalAmmo = 50;
    public int currentTotalAmmo; // Total ammo the player has
    public int currentMagazineAmmo; // Current ammo in the magazine
    public GameObject decalPrefab;


    public TMP_Text currentAmmoText;
    void Start()
    {
        currentMagazineAmmo = maxMagazineAmmo;
        currentTotalAmmo = maxTotalAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentMagazineAmmo > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentMagazineAmmo < maxMagazineAmmo && currentTotalAmmo > 0)
        {
            Reload();
        }
        
        DisplayHUD();
    }

    void Shoot()
    {
        // Define the maximum shooting distance
        float maxDistance = 100f;

        // Create a ray from the camera through the center of the screen
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Declare a RaycastHit variable to store information about the hit object
        RaycastHit hit;

        // Perform the raycast and check if it hits an object within the maxDistance
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            /*// Check if the hit object has a Drone component
            Drone drone = hit.collider.GetComponent<Drone>();
            if (drone != null)
            {
                // Apply damage to the drone
                drone.ReceiveDamage(damageAmount);
            }*/
        }
        
        currentMagazineAmmo--;
    }

    void Reload()
    {
        int bulletsToReload = maxMagazineAmmo - currentMagazineAmmo;
        int bulletsAvailable = Mathf.Min(bulletsToReload, currentTotalAmmo);
        
        currentTotalAmmo -= bulletsAvailable;
        currentMagazineAmmo += bulletsAvailable;
    }

    void DisplayHUD()
    {
        GameManager.Instance.UpdateAmmoText(currentMagazineAmmo, currentTotalAmmo);
    }

    public void Refill()
    {
        currentMagazineAmmo = maxMagazineAmmo;
        currentTotalAmmo = maxTotalAmmo;
    }

    public bool HasFullAmmo()
    {
        return currentMagazineAmmo == maxMagazineAmmo && currentTotalAmmo == maxTotalAmmo;
    }

    void CreateDecal(Vector3 position, Vector3 normal)
{

    GameObject[] decals = GameObject.FindGameObjectsWithTag("Decal");

    if(decals.Length <25){
    // Instantiate the decal prefab at the hit point
    GameObject decal = Instantiate(decalPrefab, position, Quaternion.identity);

    // Rotate the decal to match the hit normal
    
    decal.transform.forward = normal;

    decal.transform.Rotate(Vector3.up, 180f);
     // Destroy the decal after 10 seconds
    Destroy(decal, 5f);
    }

   
}
}
