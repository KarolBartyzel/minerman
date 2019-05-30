using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HumanPlayer: BasePlayer {
    public Text coinsText;
    public Image shieldImg;
	public Image healthBarImg;
    public AudioSource damageAudio;
    public AudioSource coinAudio;
    public AudioSource shieldAudio;
    public AudioSource healthPotionAudio;
	
	public float HealthLevel;

    protected override void Init()
    {
        this.shieldImg.enabled = false;
        playerId = globalManager.AddPlayer(string.IsNullOrEmpty(GlobalState.playerName) ? "Anonim" : GlobalState.playerName, true);
        hasLight = true;
		this.healthBarImg.fillAmount = base.healthRate;
        
    }

    protected override void UpdateCollectables()
    {
        coinsText.text = "Coins: " + base.coins;
        this.shieldImg.enabled = base.hasShield;
		this.healthBarImg.fillAmount = base.healthRate;
    }

    override protected void UpdateMovement()
    {
        animator.SetBool("Walking", false);

        if (Input.GetKey(KeyCode.W))
        {
            moveUp();
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveRight();
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDown();
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }

        if (canDropBombs && Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }
    }
	
	protected override void UpdateHealthRate()
    {
        base.UpdateHealthRate();
        if(this.healthBarImg.fillAmount > base.healthRate)
        {
            this.damageAudio.Play();
        }
		this.healthBarImg.fillAmount = base.healthRate;
    }
    
    protected override void PlayCollectCoinAudio(){
        this.coinAudio.Play();
    }
    
    protected override void PlayCollectShieldAudio(){
        this.shieldAudio.Play();
    }
    
    protected override void PlayCollectHealthPotionAudio(){
        this.healthPotionAudio.Play();
    }
}