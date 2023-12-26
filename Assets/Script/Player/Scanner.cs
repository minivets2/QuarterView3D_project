using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        targets = Physics.SphereCastAll(transform.position, scanRange, UnityEngine.Vector3.forward, 
            0, targetLayer);

        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit target in targets)
        {
            UnityEngine.Vector3 myPos = transform.position;
            UnityEngine.Vector3 targetPos = target.transform.position;
            float curDiff = UnityEngine.Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        
        return result;
    }
}
