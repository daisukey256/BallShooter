using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public float rotationSpeed;
    public float maxRotate = 10.0f;
    public float minRotate = -10.0f;

    Vector3 playerCurrentAngle;

    float gameTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentAngle = transform.rotation.eulerAngles;
        if (playerCurrentAngle.y > 180f)
        {
            playerCurrentAngle = new Vector3(playerCurrentAngle.x,playerCurrentAngle.y - 360f, playerCurrentAngle.z);
        }

        gameTime += Time.deltaTime;
        float horizontal = Mathf.Sin(gameTime);

        transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0); // 水平方向の回転

        if (playerCurrentAngle.y > maxRotate)
        {
            playerCurrentAngle = new Vector3(playerCurrentAngle.x, maxRotate, playerCurrentAngle.z);
            transform.rotation = Quaternion.Euler(playerCurrentAngle);
        }
        else if (playerCurrentAngle.y < minRotate)
        {
            playerCurrentAngle = new Vector3(playerCurrentAngle.x, minRotate, playerCurrentAngle.z);
            transform.rotation = Quaternion.Euler(playerCurrentAngle);
        }
    }
}
