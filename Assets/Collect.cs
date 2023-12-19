using UnityEngine;

public class Collect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        other.GetComponent<Item>().Collect();
        GameManager.instance.exp.ExperienceLevelUp(other.GetComponent<Item>().exp);
    }
}
