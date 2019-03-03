using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class throwObj : MonoBehaviour
{
    private int numObjectsRemaining = 9;
    public Text gameOver;
    public Text numObjRemainingTxt;

    public float maxObjectSpeed = 40;
    public float flickSpeed = 0.4f;

    public string respawnName = "";
    public float howClose = 9.5F; //how close ball to follow along when dragging

    float startTime, endTime, swipeDistance, swipeTime;
    Vector2 startPos;
    Vector2 endPos;
    float tempTime;

    float Flicklength;
    float ObjectVelocity = 0;
    float objectSpeed = 0;
    Vector3 angle;

    bool thrown, holding;
    Vector3 newPosition, velocity;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.text = "";
        this.GetComponent<Rigidbody>().useGravity = false;
    }

    void onTouch()
    {
        if (numObjectsRemaining > 0)
        {
            Vector3 mousePos = Input.GetTouch(0).position;
            mousePos.z = Camera.main.nearClipPlane * howClose;
            newPosition = Camera.main.ScreenToViewportPoint(mousePos); //only drag in this plane
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition * 4, newPosition * 4, 80f * Time.deltaTime); //this obj
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (holding)
        {
            onTouch();
        }
        else if (thrown)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position); //ray is where you touch
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 200f)) //how far ray shoots
                {
                    if (hit.transform == this.transform)
                    {
                        startTime = Time.time;
                        startPos = _touch.position;
                        holding = true;
                        transform.SetParent(null); //not necessary
                    }
                }
            }
            else if (_touch.phase == TouchPhase.Ended && holding)
            {
                endTime = Time.time;
                endPos = _touch.position;
                swipeDistance = (endPos - startPos).magnitude;
                swipeTime = endTime - startTime;

                if (swipeTime < flickSpeed && swipeDistance > 80f)
                {
                    CalSpeed();
                    MoveAngle();
                    this.GetComponent<Rigidbody>().AddForce(new Vector3((angle.x * objectSpeed),
                        (angle.y * objectSpeed), (angle.z * objectSpeed + 100)));
                    this.GetComponent<Rigidbody>().useGravity = true;
                    holding = false;
                    thrown = true;
                    Invoke("_Reset", 3);
                }
                else
                {
                    _Reset(); // to prev position
                }
            }
            if (startTime > 0)
            {
                tempTime = Time.time - startTime;
            }
            if (tempTime > flickSpeed)
            {
                startTime = Time.time;
                startPos = _touch.position;
            }
        }

    }

    void _Reset()
    {
        numObjectsRemaining--;
        SetNumRemaining();
        Transform RespawnPoint = GameObject.Find(respawnName).transform;
        this.gameObject.transform.position = RespawnPoint.position;
        this.gameObject.transform.rotation = RespawnPoint.rotation;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.GetComponent<Rigidbody>().useGravity = false;
        thrown = holding = false;

    }

    void CalSpeed()
    {
        Flicklength = swipeDistance;
        if (swipeTime > 0)
        {
            ObjectVelocity = Flicklength / (Flicklength - swipeTime);
        }
        objectSpeed = ObjectVelocity * 50;
        objectSpeed = objectSpeed - (objectSpeed * 1.7f);
        if (objectSpeed <= -maxObjectSpeed)
        {
            objectSpeed = -maxObjectSpeed;
        }
        swipeTime = 0;
    }

    void MoveAngle()
    {
        angle = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(endPos.y + 200f, (Camera.main.GetComponent<Camera>().nearClipPlane - howClose)));
    }

    void SetNumRemaining()
    {
        if (numObjectsRemaining < 0)
        {
            gameOver.text = "GAME OVER";
            Destroy(this);
        }
        else
        {
            numObjRemainingTxt.text = "Remaining throws: " + (numObjectsRemaining +1).ToString();
        }
    }

}
