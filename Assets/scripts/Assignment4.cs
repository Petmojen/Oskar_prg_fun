using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignment4 : ProcessingLite.GP21 {

    Vector2 velPos;
    Vector2 acsPos;
    Vector2 wrapPos;
    Vector2 acceleration = new Vector2(0, 0);

    float acsDecSpeed = 0.025f;
    float acsIncSpeed = 0.1f;

    float diameter = 3;
    float speed = 10;
    float maxSpeed;
    float radie;

    bool gravitySwitch = false;
    float gravity = 9.82f;

    void Start() {
        velPos = new Vector2(Width / 2, Height / 2);
        acsPos = new Vector2(Width / 2, Height / 3);
        radie = diameter / 2;
    }

    void Update() {
        Background(0, 0, 0);

        //Velocity Circle
        BallNR1();
        //Acceleration Circle
        BallNR2();
        BorderWrap();
        Gravity();
    }

    void Gravity() {
        if(Input.GetKeyUp(KeyCode.G)) {
            if(gravitySwitch == false) {
                gravitySwitch = true;
            } else {
                gravitySwitch = false;
            }
        }
        if(gravitySwitch == true) {
            if((velPos.y - radie) >= 0.1f) {
                velPos.y -= gravity * Time.deltaTime;
            } else {
                velPos.y = 0.1f + radie;
            }
        }
    }

    void BorderWrap() {
        //Left border Temp Circle
        if((velPos.x - radie) <= 0) {
            wrapPos = new Vector2(Width + velPos.x, velPos.y);
            Circle(wrapPos.x, wrapPos.y, diameter);
            //Right border Temp Circle
        }
        if((velPos.x + radie) >= Width) {
            wrapPos = new Vector2(velPos.x - Width, velPos.y);
            Circle(wrapPos.x, wrapPos.y, diameter);
            //Bottom border temp circle
        } 
        if((velPos.y - radie) <= 0) {
            wrapPos = new Vector2(velPos.x, Height + velPos.y);
            Circle(wrapPos.x, wrapPos.y, diameter);
            //Top border temp circle
        } 
        if((velPos.y + radie) >= Height) {
            wrapPos = new Vector2(velPos.x, velPos.y - Height);
            Circle(wrapPos.x, wrapPos.y, diameter);
        }

        //Left border switch (telport main circle to temp circle)
        if((velPos.x + radie) <= 0 || (velPos.x - radie) >= Width || (velPos.y + radie) <= 0 || (velPos.y - radie) >= Height)  {
            velPos.x = wrapPos.x;
            velPos.y = wrapPos.y;
        }
    }

    void BallNR1() {
        velPos.x = velPos.x + Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        velPos.y = velPos.y + Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Stroke(255, 255, 255);
        Circle(velPos.x, velPos.y, diameter);
    }

    void BallNR2() {
        if(Input.GetKey(KeyCode.D)) {
            acceleration.x += acsIncSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.A)) {
            acceleration.x -= acsIncSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.S)) {
            acceleration.y -= acsIncSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.W)) {
            acceleration.y += acsIncSpeed * Time.deltaTime;
        } else {
            //Decelatrion
            if(acceleration.x > 0) {
                acceleration.x -= acsDecSpeed * Time.deltaTime;
            }
            if(acceleration.x < 0) {
                acceleration.x += acsDecSpeed * Time.deltaTime;
            }
            if(acceleration.y > 0) {
                acceleration.y -= acsDecSpeed * Time.deltaTime;
            }
            if(acceleration.y < 0) {
                acceleration.y += acsDecSpeed * Time.deltaTime;
            }
        }
        if(acceleration.x >= 0.2f) {
            acceleration.x = 0.2f;
        }
        if(acceleration.x <= -0.2f) {
            acceleration.x = -0.2f;
        }
        if(acceleration.y >= 0.2f) {
            acceleration.y = 0.2f;
        }
        if(acceleration.y <= -0.2f) {
            acceleration.y = -0.2f;
        }
        acsPos.x += acceleration.x;
        acsPos.y += acceleration.y;
        Stroke(255, 0, 0);
        Circle(acsPos.x, acsPos.y, diameter);
        Stroke(255, 255, 255);
    }
}
