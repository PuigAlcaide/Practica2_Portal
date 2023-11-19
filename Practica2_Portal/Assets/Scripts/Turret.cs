using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LineRenderer laserLine;
    public float maxLaserDistance = 100f;
    public LayerMask hitLayerMask;

    private bool enabled = true;
    
    void Start()
    {
        // Initialize the LineRenderer component
        InitializeLineRenderer();
    }
    
    void Update()
    {
        if (enabled)
        {
            ShootLaser();
        }
    }
    
    void InitializeLineRenderer()
    {
        // Check if LineRenderer component exists on the GameObject
        laserLine = GetComponent<LineRenderer>();

        // If LineRenderer doesn't exist, create and configure it
        if (laserLine == null)
        {
            laserLine = gameObject.AddComponent<LineRenderer>();
            laserLine.useWorldSpace = true; // Set to true if you want world space coordinates
            laserLine.material = new Material(Shader.Find("Sprites/Default"));
            laserLine.material.color= Color.red;
            laserLine.startWidth = 0.1f; // Adjust the width as needed
            laserLine.endWidth = 0.1f;
        }

        // Disable the LineRenderer initially
        laserLine.enabled = false;
    }
    
    void ShootLaser()
    {
        // Set the starting position of the laser to the turret's position
        laserLine.SetPosition(0, transform.position);

        // Cast a ray from the turret forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Check if the ray hits anything within the specified distance and layer mask
        if (Physics.Raycast(ray, out hit, maxLaserDistance, hitLayerMask))
        {
            if (hit.collider.gameObject.tag.Equals("Player"))
            {
                Player character = hit.collider.GetComponent<Player>();
                character.Die();
            }
            else
            {
                // Set the end position of the laser to the hit point
                laserLine.SetPosition(1, hit.point);

                // Get information about the collider that was hit
                Collider hitCollider = hit.collider;
                Debug.Log("Hit object: " + hitCollider.gameObject.name);
                Debug.Log("Hit point: " + hit.point);
                Debug.Log("Hit normal: " + hit.normal);
            }


        }
        else
        {
            // If the ray doesn't hit anything, set the end position of the laser to the maximum distance
            laserLine.SetPosition(1, transform.position + transform.forward * maxLaserDistance);
        }

        // Enable the LineRenderer to make the laser visible
        laserLine.enabled = true;

        // You may want to disable the LineRenderer after a short delay to make the laser disappear
        // Invoke("DisableLaser", 0.1f);
    }

    void DisableLaser()
    {
        enabled = false;
        // Disable the LineRenderer to make the laser invisible
        laserLine.enabled = false;
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag.Equals("Cube") || other.gameObject.tag.Equals("Turret"))
        {
            Debug.Log("Desactivando: "+other.gameObject);
            DisableLaser();
            
        }
    }
}