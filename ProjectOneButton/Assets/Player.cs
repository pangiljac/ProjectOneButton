using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour{

    public Transform topWall, bottomWall;
    public float openSpeed;
    public float closeSpeed;
    public TextMeshProUGUI textScore;
    public GameObject resetButton;
    public GameObject darkScreen;
    public Spawner spawner;

    private bool isClosed = true;
    private bool isMaxOpen = false;
    private float startLocation;
    private int score = 0;

    // Start is called before the first frame update
    void Start() {
        startLocation = topWall.position.y;
    }

    // Update is called once per frame
    void Update() {
        if (!isMaxOpen & Input.GetKey("space")) {
            //Debug.Log("Opening Gate");
            isClosed = false;
            topWall.transform.Translate(Vector3.up * openSpeed * Time.deltaTime);
            bottomWall.transform.Translate(Vector3.down * openSpeed * Time.deltaTime);
            
            if (topWall.position.y >= 5) {
                isMaxOpen = true;
                topWall.position = new Vector3(topWall.position.x, (float)5, topWall.position.z);
                bottomWall.position = new Vector3(bottomWall.position.x, (float)-5, bottomWall.position.z);
            }
        }
        else if(!isClosed & !Input.GetKey("space")){
            //Debug.Log("Closing Gate");
            isMaxOpen = false;
            topWall.transform.Translate(Vector3.down * closeSpeed * Time.deltaTime);
            bottomWall.transform.Translate(Vector3.up* closeSpeed * Time.deltaTime);

            if (topWall.position.y <= startLocation) {
                topWall.position = new Vector3(topWall.position.x, startLocation, topWall.position.z);
                bottomWall.position = new Vector3(bottomWall.position.x, -startLocation, bottomWall.position.z);
                isClosed = true;
            }
            


        }

    }

    public void updateScore() {
        score += 1;
        textScore.text = "Score: " + score;
    }

    public void resetScore() {
        score = 0;
        textScore.text = "Score: " + score;
    }

    public void endGame() {
        resetButton.SetActive(true);
        darkScreen.SetActive(true);
        spawner.endGame();
    }

    public void startGame() {
        resetScore();
        resetButton.SetActive(false);
        darkScreen.SetActive(false);
        spawner.startGame();
    }
}
