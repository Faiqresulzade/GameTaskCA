using UnityEngine;

public class Detector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        InstantiateCube._instance.CubeDestroy(collision, gameObject);
    }

}
