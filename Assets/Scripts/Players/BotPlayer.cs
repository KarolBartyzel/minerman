using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public abstract class BotPlayer: BasePlayer {
    protected static readonly int WALL_LAYER = 8;
    protected static readonly int PLAYER_LAYER = 9;
    public AudioSource deadAudio;

    protected int direction = 1;
    protected int step = 3;

    protected override void Init()
    {
        playerId = globalManager.AddPlayer();
    }

    protected override void UpdateCollectables(){}

    protected void makeStep() 
    {
        step = (step + direction + 4) % 4;
    }

    protected void move()
    {
        switch(step) {
            case 0:
                moveUp();
                break;
            case 1:
                moveLeft();
                break;
            case 2:
                moveDown();
                break;
            case 3:
                moveRight();
                break;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            //place bomb, change direction and turn around
            DropBomb();
            direction = (-1) * direction;
        }
    }

    protected abstract void NextStep();

    override protected void UpdateMovement()
    {
        if (rigidBody.velocity.magnitude < 2) {
            makeStep();
        }
        
        NextStep();

        animator.SetBool("Walking", false);
        switch(step) {
            case 0:
                moveUp();
                break;
            case 1:
                moveLeft();
                break;
            case 2:
                moveDown();
                break;
            case 3:
                moveRight();
                break;
        }
    }
    
    protected virtual void PlayBotDeadAudio(){
        //this.deadAudio.Play();
        Debug.Log("DEAD");
    }
}