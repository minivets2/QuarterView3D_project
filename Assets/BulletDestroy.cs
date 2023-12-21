using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Area")) return;
        
        gameObject.SetActive(false);
    }
}
