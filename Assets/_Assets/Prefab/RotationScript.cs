using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed = 50f; // Vitesse de rotation en degr�s par seconde

    void Update()
    {
        // Faire tourner l'objet autour de son axe Y local
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}