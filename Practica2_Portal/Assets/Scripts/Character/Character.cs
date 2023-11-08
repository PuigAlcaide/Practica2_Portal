using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float mouseXSensitivity = 120f;
    public float mouseYSensitivity = 120f;
    public float speed = 5f;

    Camera camera;
    Rigidbody rb;

    private float rotation;
    private bool isGrounded;
    private bool isDead;

    private Gun gun;
    
    public int maxLife = 100;
    public int maxShield = 100;
    public int currentLife;
    public int currentShield;

    void Start(){
        camera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();


        currentLife = maxLife;
        currentShield = maxShield;
    }

    void Update()
    {
        if (!isDead)
        {
            Rotate();
            Move();   
        }
    }

    void Rotate(){
        float xr = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
        float yr = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;
        
        rotation -= yr;
        rotation = Mathf.Clamp(rotation, -90, 90);

        transform.Rotate(0, xr, 0);
        camera.transform.localRotation = Quaternion.Euler(rotation, 0, 0);
    }

    void Move(){
        float lateralMov = Input.GetAxis("Horizontal");
        float forwardMov = Input.GetAxis("Vertical");

        Vector3 inputMovement = new Vector3(lateralMov, 0, forwardMov);

        Vector3 worldMovement = transform.TransformDirection(inputMovement);

        Vector3 finalMovement = worldMovement * speed * Time.deltaTime;

        rb.MovePosition(rb.position + finalMovement);
    }

    public void RefillAmmo()
    {
        gun.Refill();
    }

    public bool HasFullAmmo()
    {
        return gun.HasFullAmmo();
    }

    public void ReceiveDamage(int damage)
    {

        if (currentShield > 0)
        {
            int shieldDamage = (int) (damage * 0.75f);
            int lifeDamage = (int) (damage * 0.25f);

            currentShield = Mathf.Max(currentShield - shieldDamage, 0);
            currentLife = Mathf.Max(currentLife - lifeDamage, 0);
        }
        else
        {
            currentLife = Mathf.Max(currentLife - damage, 0);
            Die();
        }
        UpdateLifeAndShield();
    }

    public bool HasFullLife()
    {
        return currentLife == maxLife;
    }

    public void RefillLife()
    {
        currentLife = maxLife;
        UpdateLifeAndShield();
    }
    
    public bool HasFullShield()
    {
        return currentShield == maxShield;
    }

    public void RefillShield()
    {
        currentShield = maxShield;
        UpdateLifeAndShield();
    }

    void UpdateLifeAndShield()
    {
        GameManager.Instance.UpdateLifeAndShieldText(currentLife, currentShield);
    }

    public void Die()
    { 
        isDead = true;
        GameManager.Instance.PlayerDied();
    }
}
