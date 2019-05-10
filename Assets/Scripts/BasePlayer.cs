using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BasePlayer: MonoBehaviour {
    private static readonly int MAX_BOMBS = 2;
    protected Rigidbody rigidBody;
    protected Transform myTransform;
    protected Animator animator;
    public GlobalStateManager globalManager;

    [Range (1, 4)]
    public int playerNumber = 1;

    public float moveSpeed = 5f;
    public bool canDropBombs = true;
    public bool dead = false;

    public GameObject bombPrefab;
    private IList<GameObject> bombs = new List<GameObject>();

    protected virtual void UpdateMovement() {}

    protected void moveUp() {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
        myTransform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("Walking", true);
    }

    protected void moveRight() {
        rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
        myTransform.rotation = Quaternion.Euler(0, 90, 0);
        animator.SetBool("Walking", true);
    }

    protected void moveDown() {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
        myTransform.rotation = Quaternion.Euler(0, 180, 0);
        animator.SetBool("Walking", true);
    }

    protected void moveLeft() {
        rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
        myTransform.rotation = Quaternion.Euler(0, 270, 0);
        animator.SetBool("Walking", true);
    }

    protected void DropBomb()
    {
        if (bombPrefab)
        {
            bombs = bombs.Where(bomb => bomb != null).ToList();
            if (bombs.Count() < MAX_BOMBS) {
                GameObject bomb = Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x), bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z)), bombPrefab.transform.rotation);
                bombs.Add(bomb);
            }
        }
    }

    protected void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = myTransform.Find("PlayerModel").GetComponent<Animator>();
    }

    void Update()
    {
        UpdateMovement();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            dead = true;
            globalManager.PlayerDied(playerNumber);
            Destroy(gameObject);
        }
    }
}
