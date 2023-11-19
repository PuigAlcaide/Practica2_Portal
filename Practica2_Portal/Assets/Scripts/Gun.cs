using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform puntoDeSoltar; // Asigna el transform deseado desde el Inspector
    public Transform orientation; // Asigna el objeto de orientación desde el Inspector

    private GameObject objetoSostenido;
    private bool objetoEnAire = false;
    private bool puedeRecoger = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && puedeRecoger)
        {
            RecogerObjeto();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SoltarObjeto();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            LanzarObjeto();
        }

        if (objetoEnAire)
        {
            ActualizarPosicionEnAire();
        }
    }

    void RecogerObjeto()
    {
        // Verifica si ya tienes un objeto sostenido y si se puede recoger
        if (puedeRecoger)
        {
            Debug.Log("t54tge");

            // Raycast para detectar objetos cercanos que pueden ser recogidos
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
            {
                Debug.Log("ews");

                if (hit.collider.CompareTag("GrabObject"))
                {
                    Debug.Log("fdfsz");
                    objetoSostenido = hit.collider.gameObject;
                    Rigidbody objetoRigidbody = objetoSostenido.GetComponent<Rigidbody>();

                    // Desactiva la física temporalmente para establecer la posición y la rotación
                    objetoRigidbody.isKinematic = true;

                    // Establece el objeto como hijo del punto de sujeción para que siga su movimiento
                    objetoSostenido.transform.SetParent(puntoDeSoltar);

                    // Establece la posición y rotación específicas del objeto al recogerlo
                    objetoSostenido.transform.localPosition = Vector3.zero;
                    objetoSostenido.transform.localRotation = Quaternion.identity;

                    // Desactiva temporalmente el Raycast del jugador para que no interfiera con el objeto recogido
                    puedeRecoger = false;
                }
                else
                {
                    Debug.Log("sssas");
                }
            }
        }
    }

    void SoltarObjeto()
    {
        if (!puedeRecoger)
        {
            Rigidbody objetoRigidbody = objetoSostenido.GetComponent<Rigidbody>();

            objetoRigidbody.isKinematic = false;
            objetoSostenido.transform.SetParent(null);

            // El objeto ha sido soltado y puede ser recogido nuevamente
            puedeRecoger = true;
        }
    }

    void LanzarObjeto()
    {
        if (!puedeRecoger)
        {
            objetoSostenido.transform.SetParent(null); // Asegúrate de que el objeto ya no sea hijo del punto de sujeción
            StartCoroutine(EsperarLanzamiento());
        }
    }

    void ActualizarPosicionEnAire()
    {
        if (objetoSostenido != null)
        {
            // Ajusta la posición del objeto para que siga la posición del arma
            objetoSostenido.transform.position = transform.position + transform.forward * 2f; // Ajusta la distancia según sea necesario
        }

        // Chequea si el objeto está en el aire y ha tocado el suelo o una torreta
        RaycastHit hit;
        if (Physics.Raycast(objetoSostenido.transform.position, Vector3.down, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Floor"))
            {
                // El objeto ha tocado el suelo y puede ser recogido
                objetoEnAire = false;
            }
            else
            {
                // El objeto está en el aire
                objetoEnAire = true;
            }
        }
    }


    IEnumerator EsperarLanzamiento()
    {
        yield return new WaitForSeconds(0f); // Espera un segundo antes de lanzar el objeto
        Rigidbody objetoRigidbody = objetoSostenido.GetComponent<Rigidbody>();

        objetoRigidbody.isKinematic = false;

        // Utiliza la rotación de la orientación para determinar la dirección de lanzamiento
        objetoRigidbody.transform.rotation = Quaternion.LookRotation(orientation.forward);

        // Ajusta la posición inicial del objeto antes de aplicar la fuerza
        objetoRigidbody.transform.position = puntoDeSoltar.position;

        // Usa AddForce para lanzar el objeto en la dirección de la orientación
        objetoRigidbody.AddForce(orientation.forward * 10f, ForceMode.Impulse);
    }
}