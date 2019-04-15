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
        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit, i, levelMask);

            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation);
            }
            else
            {
                Debug.Log(hit.collider.gameObject.transform.parent.tag);
                if(hit.collider.gameObject.transform.parent.tag == "WeakBlock")
                {
                    Debug.Log("not yet");
                    Destructible crate = (Destructible) hit.collider.gameObject.GetComponent(typeof(Destructible));
                    crate.Collapse();
                }
                break;
            }

            yield return new WaitForSeconds(.05f);
        }
    }

    void destroyWall(GameObject hitObj)
    {
        Debug.Log("Destroying: " + hitObj.ToString());
        Destroy(hitObj);
    }


    void Start()
    {
        Invoke("Explode", 3f);
    }

    void Update()
    {

    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));

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
