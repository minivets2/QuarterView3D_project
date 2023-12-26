using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        other.GetComponent<Item>().SetTarget(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        other.GetComponent<Item>().SetTarget(false);
    }
}
