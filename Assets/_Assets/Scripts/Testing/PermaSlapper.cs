using UnityEngine;

public class PermaSlapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, 0.2f);
        if (collidersInRange.Length == 0)
        {
            return;
        }

        foreach (Collider collider in collidersInRange)
        {
            IInteractable interactable = null;
            if (!collider.TryGetComponent<IInteractable>(out interactable))
            {
                return;
            }
            interactable.Slap();
        }
    }
}
