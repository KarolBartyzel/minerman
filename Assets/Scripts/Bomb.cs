using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bomb: MonoBehaviour
{
    private bool exploded = false;

    public GameObject explosionPrefab;
    public LayerMask levelMask;
    public LayerMask weakWallsMask;
    public Destructible crateScript;

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i < 5; i++)
        {
            Physics.SphereCast(transform.position + new Vector3(0, 0.5f, 0), 0.1f, direction, out RaycastHit hit, i, levelMask);

            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            }
            else
            {
                if(hit.collider.gameObject.transform.tag == "WeakBlock")
                {
                    Destructible crate = (Destructible) hit.collider.gameObject.GetComponent(typeof(Destructible));
                    crate.Collapse();
                }
                break;
            }

            yield return new WaitForSeconds(.05f);
        }
    }

    void Start()
    {
        Invoke("Explode", 3f);
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward / 2.0f));
        StartCoroutine(CreateExplosions(Vector3.right / 2.0f));
        StartCoroutine(CreateExplosions(Vector3.back / 2.0f));
        StartCoroutine(CreateExplosions(Vector3.left / 2.0f));

        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false);
        Destroy(gameObject, .3f);
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (!exploded && other.CompareTag("Explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }  
    }
}
