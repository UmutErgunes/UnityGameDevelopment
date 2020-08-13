using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    //Declaration Variables
    float horizontalMove;
    float verticalMove;
    Vector3 Difference;
    Vector3 DifferentX;
    public GameObject Camera;
    public GameObject BackGroundImage;
    public const int carSpeed = 10;
    public int RotationSpeed;
    Rigidbody2D rigidbdy;
    int Points;
    float Fuel = 100;
    public Text PointText, FuelText;



    void Start()
    {
        rigidbdy = GetComponent<Rigidbody2D>();

        //Get the difference between camera and car X-Axis
        Difference = Camera.transform.position - transform.position;
        
    }

    
    void Update()
    {
        MoveCar();
        PointText.text = "Points: " + Points.ToString();
        FuelText.text = "Fuel: " + Fuel.ToString();

        //Control of the Camera
        DifferentX = transform.position + Difference;
        Camera.transform.position = new Vector3(DifferentX.x, DifferentX.y, Camera.transform.position.z);
        BackGroundImage.transform.position = new Vector3(DifferentX.x, BackGroundImage.transform.position.y, BackGroundImage.transform.position.z);
        /*
               if (Input.GetKeyDown(KeyCode.Z))
               {
                   transform.rotation.z = 10;
               }

                if (Input.GetKeyDown(KeyCode.D))
                 {
                     Fuel -= 2 * Time.deltaTime;
                 }*/
    }

    void MoveCar()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        //Show Horiz. axis input 'q' & 'd' in Debug 
        //Debug.Log(horizontalMove);
        //Debug.Log(verticalMove);
        rigidbdy.AddForce(new Vector3(verticalMove * carSpeed, 0, 0));
        //rigidbdy.MoveRotation(Mathf.LerpAngle(rigidbdy.rotation, horizontalMove, RotationSpeed * Time.deltaTime));
       // rigidbdy.MoveRotation(horizontalMove * RotationSpeed);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            Points++;
            Destroy(collision.gameObject);
        }
    }

}
