﻿using UnityEngine;
using System.Collections;

public class PirateShipInteraction : MonoBehaviour {

    private GameObject shipObj;
    Transform ship;
    public float dis;
    public AIShip boarded;
    public enum ShipSeat { passenger, pilot, none };
    public ShipSeat seat = ShipSeat.none;


    // Use this for initialization
    void Start()
    {

        shipObj = GameObject.Find("PirateShip");
        ship = shipObj.transform;
        boarded = shipObj.GetComponent<AIShip>();
    }

    // Update is called once per frame
    void Update()
    {



        // steerShipPilot();

        switch (seat)
        {
            case ShipSeat.none:
                dis = Vector3.Magnitude(transform.position - ship.position);
                if (dis < 1.3f)
                {

                    FindSeat();

                }
                break;
            case ShipSeat.passenger:
                RideShipPassenger();
                break;
            case ShipSeat.pilot:
                SteerShipPilot();
                break;



        }



    }

    private void SteerShipPilot()
    {
        ship.position = transform.position;
        ship.rotation = transform.rotation;
        gameObject.transform.localScale = new Vector3(0, 0, 0);


    }//SteerShipPiloy

    private void RideShipPassenger()
    {

        transform.position = ship.position;
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    private void FindSeat()
    {
        if (!boarded.boreded)
        {
            boarded.boreded = true;
        }
        if (boarded.passengers > 0 && boarded.passengers < boarded.maxSpace)
        {
            seat = ShipSeat.passenger;
            boarded.passengers++;

        }

        if (boarded.passengers == 0)
        {
            seat = ShipSeat.pilot;
            boarded.passengers++;

        }


    }//FIndSeat


}