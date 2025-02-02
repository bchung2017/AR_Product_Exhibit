using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlaneController : MonoBehaviour
{
    private PortalController portalController;
    // Start is called before the first frame update
    void Start()
    {
        portalController = transform.parent.GetComponent<PortalController>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider aCol)
    {
        portalController.OnChildTriggerEnter(aCol.gameObject); // pass the own collider and the one we've hit
    }

    private void OnTriggerExit(Collider other)
    {
        portalController.OnChildTriggerExit(other.gameObject);
    }
}
