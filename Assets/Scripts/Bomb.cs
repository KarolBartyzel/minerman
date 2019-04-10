using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public LayerMask levelMask;

    void Start()
    {
        Invoke("Explode", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i < 3; i++)
        {
            //2
            RaycastHit hit;
            //3
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,
              i, levelMask);

            //4
            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction),
                  //5 
                  explosionPrefab.transform.rotation);
                //6
            }
            else
            { //7
                break;
            }

            //8
            yield return new WaitForSeconds(.05f);
        }
    }


        void Explode()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity); //1

            StartCoroutine(CreateExplosions(Vector3.forward));
            StartCoroutine(CreateExplosions(Vector3.right));
            StartCoroutine(CreateExplosions(Vector3.back));
            StartCoroutine(CreateExplosions(Vector3.left));

            GetComponent<MeshRenderer>().enabled = false; //2
            transform.Find("Collider").gameObject.SetActive(false); //3
            Destroy(gameObject, .3f); //4
        }

    }
