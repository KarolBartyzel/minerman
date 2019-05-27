using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class BasePlayer: MonoBehaviour {
    private static readonly int MAX_BOMBS = 2;
    protected Rigidbody rigidBody;
    protected Transform myTransform;
    protected Animator animator;
    public GlobalStateManager globalManager;

    public int playerId;

    public float moveSpeed = 5f;
    public bool canDropBombs = true;
    public bool dead = false;
    public bool hasLight = false;

    public GameObject bombPrefab;
    public GameObject light;
    private IList<GameObject> bombs = new List<GameObject>();

    protected abstract void UpdateMovement();
    protected abstract void UpdateCollectables();
    protected abstract void Init();

    // Collectables
    protected int coins = 0;

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
        Init();
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = myTransform.Find("PlayerModel").GetComponent<Animator>();
        light.SetActive(hasLight);
    }

    protected void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            UpdateMovement();
            UpdateCollectables();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("layer is: " + other.gameObject.layer);
        Debug.Log("tag is: " + other.gameObject.tag);
        if (other.CompareTag("Explosion"))
        {
            dead = true;
            globalManager.PlayerDied(playerId);
            Destroy(gameObject);
        }
        if (isCollectable(other.gameObject))
        { 
            other.gameObject.SetActive(false);
            HandleSpecificCollectable(other.gameObject);
        }
    }
    
    // TODO use some Unity mechanism to distunguish this
    // layers?
    // And create seperate classes for those
    private bool isCollectable(GameObject obj)
    {
        return obj.CompareTag("Coin");
    }

    private void HandleSpecificCollectable(GameObject collectable)
    {
        if (collectable.CompareTag("Coin"))
        {
            this.coins += 1;
            Debug.Log("Currently has coins: " + this.coins);
        }
    }
}
