using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPortals : MonoBehaviour
{
        
       
        public Camera otherCam;
       
        

    // Start is called before the first frame update
    void Start()
    {
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Quaternion direction = Quaternion.Inverse (transform.rotation) * Camera.main.transform.rotation;
        otherCam.transform.localEulerAngles = new Vector3 (direction.eulerAngles.x, direction.eulerAngles.y+180, direction.eulerAngles.z);


        Vector3 distancia= transform.InverseTransformPoint(Camera.main.transform.position);
        otherCam.transform.localPosition= - new Vector3 (distancia.x, -distancia.y, distancia.z);



    }
}
