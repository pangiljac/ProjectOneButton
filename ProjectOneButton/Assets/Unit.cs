using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType {
    Clear = 0,
    Crush,
    Block
}

public class Unit : MonoBehaviour {
    public Vector3[] wayPoints;
    public float speed;
    public Transform thisTransform;
    public SpriteRenderer spriteRenderer;

    private bool isMoving = false;
    private bool topCollide = false;
    private bool botCollide = false;
    private Transform wallTransform;
    private Camera cam;
    private Player player;

    
    private UnitType unitType;

    // Start is called before the first frame update
    void Start() {
        wallTransform = GameObject.Find("Wall").transform;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").GetComponent<Player>();
        

    }

    public void setUnit(Color color, UnitType unitType) {
        spriteRenderer.color = color;
        this.unitType = unitType;
    }

    // Update is called once per frame
    void Update() {
        if (isMoving) {
            thisTransform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (topCollide && botCollide) {

            Collider2D[] hits = null;
            hits = Physics2D.OverlapPointAll(new Vector2(transform.position.x, transform.position.y));
            foreach (Collider2D hit in hits){
                if (hit.name == "Inside") {
                    Debug.Log("Crush");
                    if (unitType == UnitType.Crush) {
                        player.updateScore();
                    }
                    else {
                        player.endGame();
                    }
                    Destroy(this.gameObject);
                    return;
                }
            }

            if (unitType == UnitType.Block) {
                player.updateScore();
            }
            else {
                player.endGame();
            }
            Debug.Log("Block");
            Destroy(this.gameObject);
        }

    }

    public void startMoving() {
        isMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Collsion");

        if (col.gameObject.name == "Checker") {
            if (unitType == UnitType.Clear) {
                player.updateScore();
            }
            else {
                player.endGame();
            }
            Debug.Log("Pass");
            Destroy(this.gameObject);
        }
        else if (col.gameObject.name == "TopWall") {
            topCollide = true;
        }
        else if (col.gameObject.name == "BottomWall") {
            botCollide = true;
        }
    }
}


