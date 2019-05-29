using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI;

public abstract class BasePlayer: MonoBehaviour {
    private static readonly int MAX_BOMBS = 2;
    protected Rigidbody rigidBody;
    protected Transform myTransform;
    protected Animator animator;
    public GlobalStateManager globalManager;

    public int playerId;

    public float moveSpeed = 5f;
	public float healthRate = 1f;
    public bool canDropBombs = true;
    public bool dead = false;
    public bool hasLight = false;

    public GameObject bombPrefab;
    public GameObject light;
    private IList<GameObject> bombs = new List<GameObject>();

    private float speedup = 0f;
    private float speedUpTime = 5f;

    protected abstract void UpdateMovement();
    protected abstract void UpdateCollectables();
    protected abstract void Init();

    // Collectables
    protected int coins = 0;
    protected bool hasShield = false;

    protected void moveUp() {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed + speedup);
        myTransform.rotation = Quaternion.Euler(0, 0, 0);
        animator.SetBool("Walking", true);
    }

    protected void moveRight() {
        rigidBody.velocity = new Vector3(moveSpeed + speedup, rigidBody.velocity.y, rigidBody.velocity.z);
        myTransform.rotation = Quaternion.Euler(0, 90, 0);
        animator.SetBool("Walking", true);
    }

    protected void moveDown() {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed - speedup);
        myTransform.rotation = Quaternion.Euler(0, 180, 0);
        animator.SetBool("Walking", true);
    }

    protected void moveLeft() {
        rigidBody.velocity = new Vector3(-moveSpeed - speedup, rigidBody.velocity.y, rigidBody.velocity.z);
        myTransform.rotation = Quaternion.Euler(0, 270, 0);
        animator.SetBool("Walking", true);
    }

    protected void DropBomb()
    {
        if (bombPrefab)
        {
            bombs = bombs.Where(bomb => bomb != null).ToList();
            if (bombs.Count() < MAX_BOMBS) {
                GameObject bomb = Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x - 0.5f) + 0.5f, bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z - 0.5f) + 0.5f), bombPrefab.transform.rotation);
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
        Debug.Log(other.name);
        if (other.CompareTag("Explosion"))
        {
            if(!hasShield)
            {
				UpdateHealthRate();
				if(healthRate <= 0)
				{
					dead = true;
					globalManager.PlayerDied(playerId);
					Destroy(gameObject);
				}
            }
            else
            {
                Invoke("disableShield", 0.5f);
            }
        }
        if (isCollectable(other.gameObject))
        { 
            HandleSpecificCollectable(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
	
	protected virtual void UpdateHealthRate()
    {
		healthRate = healthRate <= 0.3f ? 0f : healthRate - 0.34f;
    }
   
    // TODO use some Unity mechanism to distunguish this
    // layers?
    // And create seperate classes for those
    private bool isCollectable(GameObject obj)
    {
        return obj.CompareTag("Coin") || obj.CompareTag("Shield") || obj.CompareTag("Health Potion") || obj.CompareTag("Speed Potion");
    }

    private void HandleSpecificCollectable(GameObject collectable)
    {
        if (collectable.CompareTag("Coin"))
        {
            this.coins += 1;
        }
        if (collectable.CompareTag("Shield") && !hasShield)
        {
            enableShield();
        }
        if(collectable.CompareTag("Health Potion"))
        {
            healthRate = 1f;
        }
        if(collectable.CompareTag("Speed Potion"))
        {
            speedUp();
            Invoke("speedDown", speedUpTime);
        }
    }


    private void speedUp()
    {
        speedup = 3f;
    }
    
    private void speedDown()
    {
        speedup = 0f;
    }

    private void enableShield()
    {
        this.hasShield = true;
    }

    private void disableShield()
    {
        this.hasShield = false;
    }
}
