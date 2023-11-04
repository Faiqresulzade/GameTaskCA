using System.Collections;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startPosition;
    private int _hitCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.constraints = RigidbodyConstraints.None;
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rb.constraints = RigidbodyConstraints.None;
            Vector3 releasePosition = Input.mousePosition;
            Vector3 direction = (startPosition - releasePosition).normalized;
            rb.velocity = -direction * 10f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        _hitCount++;
        if (_hitCount == 1)
        {
            InstantiateCube._instance.Instantiate(gameObject);
        }

    }

    IEnumerator CallInstantiate()
    {
        yield return new WaitForSeconds(0.0002f);
        // _hitCount++;

    }
}
