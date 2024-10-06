using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    private int countPickUps;
    private int numPickUps = 4; //Put here the number of pickups you have;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI PlayerPosition;
    public TextMeshProUGUI DistancePlayerToPickups;
    public TextMeshProUGUI PlayerVelocity;
    public Vector3 PlayerLastPosition;
    public Vector3 PlayerCurrentPosition;
    public float PlayerSpeed;
    void Start()
    {
        countPickUps = 0;
        winText.text = "";
        SetCountText();
        PlayerLastPosition = transform.position;
    }

    void OnMove (InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void FixedUpdate() //because the movements involve physics
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUp"){
            other.gameObject.SetActive(false);
            countPickUps++;
            print(countPickUps);
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + countPickUps.ToString();
        if(countPickUps >= numPickUps)
        {
            winText.text = "You Win!!";
        }
    }

    void Update()
    {
        PlayerCurrentPosition = transform.position;
        PlayerPosition.text = "Player Position: " + PlayerCurrentPosition.ToString("0.00");

        PlayerSpeed = (PlayerCurrentPosition - PlayerLastPosition).magnitude / Time.deltaTime;
        PlayerVelocity.text = "Player Velocity: " + PlayerSpeed.ToString("0.00");
    }
}
