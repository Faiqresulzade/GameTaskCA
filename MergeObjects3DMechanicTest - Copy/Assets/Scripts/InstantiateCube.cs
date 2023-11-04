using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InstantiateCube : MonoBehaviour
{
    [SerializeField] private List<GameObject> Cubes;
    [SerializeField] private Transform InstantiateTransform;

    private GameObject prevCube;
    private Transform ChangeCubeTransform;
    private int count;

    public static InstantiateCube _instance;



    private void Start()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<InstantiateCube>();
        }
    }

    public void Instantiate(GameObject firstCube)
    {
        try
        {
            if (prevCube != null)
            {
                Destroy(prevCube.GetComponent<CubeController>());
            }
            else
            {
                Destroy(firstCube.GetComponent<CubeController>());
            }

            GameObject Instantiatecube = Instantiate(Cubes[Random.Range(0, Cubes.Count)], InstantiateTransform.position, InstantiateTransform.rotation);

            if (Instantiatecube != null)
            {
                Destroy(Instantiatecube.GetComponent<CubeController>());
                Destroy(Instantiatecube.GetComponent<BoxCollider>());
                prevCube = Instantiatecube;
                Instantiatecube.AddComponent<Rigidbody>();
                Instantiatecube.AddComponent<BoxCollider>();
                Instantiatecube.AddComponent<CubeController>();
                Instantiatecube.AddComponent<Detector>();
            }
        }
        catch
        {
            GameObject Instantiatecube = Instantiate(Cubes[Random.Range(0, Cubes.Count)], InstantiateTransform.position, InstantiateTransform.rotation);

            if (Instantiatecube != null)
            {
                prevCube = Instantiatecube;
                Instantiatecube.AddComponent<Rigidbody>();
                Instantiatecube.AddComponent<BoxCollider>();
                Instantiatecube.AddComponent<CubeController>();
                Instantiatecube.AddComponent<Detector>();
            }
        }
    }

    public void CubeDestroy(Collision collision, GameObject destroyedCube)
    {
        if (collision.gameObject.tag == destroyedCube.gameObject.tag)
        {
            bool result = Int32.TryParse(collision.gameObject.tag, out int tagInt);

            count++;
            if (count == 2)
            {
                ChangeCubeTransform = collision.transform;
                Destroy(collision.gameObject);
                Destroy(destroyedCube);
                if (tagInt != Cubes.Count)
                {
                    GameObject Instantiatecube = Instantiate(Cubes[tagInt], ChangeCubeTransform.position, ChangeCubeTransform.rotation);

                    Instantiatecube.AddComponent<Rigidbody>();
                    Instantiatecube.AddComponent<BoxCollider>();
                    Instantiatecube.AddComponent<Detector>();
                    
                }
                count = 0;
            }
        }

    }
}