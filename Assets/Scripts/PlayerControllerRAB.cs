using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;
using TMPro;

public class PlayerControllerRAB : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
	public GameObject winTextObject;
    public TextMeshProUGUI accText;
    public bool isMobileBuild;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        if (isMobileBuild)
        {
            // enable reading sensor values
            // InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
        }
    }

    void Update()
    {
        float dist = (this.transform.position - GameObject.Find("Ground").transform.position).magnitude;
        if (dist > 30) this.transform.position = new Vector3(0, 1f, 0);
    }

    // void OnMove(InputValue movementValue)
    // {
    //     Vector2 movementVector = movementValue.Get<Vector2>();

    //     movementX = movementVector.x;
    //     movementY = movementVector.y;
    // }

    private void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        // if (isMobileBuild)
        // {
        //     // use accelarometer
        //     // Android is right-handed coordinate, remember to change to Unity coordinate (left-handed) 
        //     Vector3 a = UnityEngine.InputSystem.Accelerometer.current.acceleration.ReadValue();
        //     accText.text = "Accelerometer: " + a.ToString("F6");
        //     movement = new Vector3(a.x, 0.0f, a.y);
        // }
        // else
        // {
        //     movement = new Vector3(movementX, 0.0f, movementY);
        // }

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("pickups")) 
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }

    // void OnCollisionEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("wall"))
    //     {
    //         Handheld.Vibrate();
    //     }
    // }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 30)
        {
            winTextObject.SetActive(true);
        }
    }
}
