using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Vector3 startPosition;
    public Vector3 endtPosition;

    public GameObject[] PickUps;
    public GameObject Player;
    public TextMeshProUGUI DistancePlayerToPickUps;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        //neRenderer.SetPosition(0, startPosition);//0 for the start point, position vector 'startPosition'
        //neRenderer.SetPosition(1, endtPosition);//1 for the ens point, position vector 'endPosition'
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        //set LineRenderer material or color as needed\
       //ineRenderer.material = new Material(Shader.Find("Sprites/Default"));
       //ineRenderer.startColor = Color.cyan;
        //neRenderer.endColor = Color.cyan;

    }

    // Update is called once per frame
    void Update()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closesPickup = null;
        startPosition = Player.transform.position;

        //Loop through all PickUps
        foreach (GameObject PickUp in PickUps)
        {
            //check if the PickUp is atill active
            if (PickUp.activeSelf)
            {
                //calculate the distance from the player to the pickup
                float distanceToPickUp = Vector3.Distance(Player.transform.position, PickUp.transform.position);
                
                //find the closest active PickUp
                if(distanceToPickUp < closestDistance)
                {
                    closestDistance = distanceToPickUp;
                    closesPickup = PickUp;
                    DistancePlayerToPickUps.text = "Distance to PickUps: " + distanceToPickUp.ToString("0.00");
                }
            }
        }

        //if we have a closest pickup, set it to blue and others to white
        foreach(GameObject PickUp in PickUps)
        {
            if (PickUp.activeSelf)
            {
                if(PickUp == closesPickup)
                {
                    //set the closest pickup to bluse
                    PickUp.GetComponent<Renderer>().material.color = Color.blue;

                    //set up the lineRenderer to draw a line to the closest PickUp
                    endtPosition = closesPickup.transform.position;//closest PickUp
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, endtPosition);//0 for the start point, position vector 'startPosition'
                    lineRenderer.SetPosition(1, endtPosition);//1 for the ens point, position vector 'endPosition'

                }
                else
                {
                    //set other active PickUps to White
                    PickUp.GetComponent<Renderer>().material.color = Color.white;


                }
            }
        }

        //Disable the lineRenderer if no active PickUps are left
        if(closesPickup == null)
        {
            lineRenderer.enabled = false;
        }

    }
}
