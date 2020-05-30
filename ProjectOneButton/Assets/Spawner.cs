using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform thisTransform;
    public GameObject unit;
    private Unit unitScript;
    private float lastTime;
    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time - 1 > lastTime && isRunning) {
            GameObject currentUnit = Instantiate(unit);
            currentUnit.transform.position = thisTransform.position;
            unitScript = currentUnit.GetComponent<Unit>();

            switch (Random.Range(0,3)) {
                case 0:
                    unitScript.setUnit(new Color(0,255,0), UnitType.Clear);
                    break;
                case 1:
                    unitScript.setUnit(new Color(255, 255, 0), UnitType.Crush);
                    break;
                case 2:
                    unitScript.setUnit(new Color(255,0,0), UnitType.Block);
                    break;
                default:
                    Debug.Log("Out of Range");
                    unitScript.setUnit(new Color(0, 255, 0), UnitType.Clear);
                    break;
            }
            

            unitScript.startMoving();
            lastTime = Time.time;
        }
    }

    public void startGame() {
        isRunning = true;
    }

    public void endGame() {
        isRunning = false;
        lastTime = Time.time;
       
    }
}
