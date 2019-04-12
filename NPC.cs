using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    //Public array of game objects
    public GameObject[] waypoints;

    //Variable for NavMesh agent
    private NavMeshAgent myAgent;

    //Variable to keep track of current waypoint
    private int currentWaypoint;

    //Variable to stop character from returning to patrol
    private bool talking;

    //Game object for quest text
    public GameObject QuestText;

    //Boolean to keep track of whether quest item is found
    private bool questItemFound;

    //Public quest item to link to the quest item object in the scene
    public QuestItem questItem;

    //Activate quest after player talks to NPC
    public bool questActive;

    //Animator to reference popup text in scene
    public Animator popupText;

    //Start is called before the first frame update
    void Start()
    {
        //Reference NavMesh agent component
        myAgent = GetComponent<NavMeshAgent>();

        //Specify desired waypoint for destination
        myAgent.destination = waypoints[currentWaypoint].transform.position;
    }

    //Update is called once per frame
    void Update()
    {
        //Check to see if quest item was collected
        if (!questItemFound && questItem.itemCollected) 
        {
            //Do not destroy the grandma
            if (questItem.tag == "NPC 1")
            {
                questItemFound = true;
            }

            //Destroy the item if collected
            else
            {
                questItemFound = true;
                Destroy(questItem.gameObject);
            }
        }

        //Check for not talking to move to next waypoint
        if (Vector3.Distance(myAgent.destination, transform.position) <= 1 && !talking)
        {
            //Move to next waypoint
            currentWaypoint++;

            //Return to first waypoint after last so the patrol loops
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }

            //Specify desired waypoint for destination
            myAgent.destination = waypoints[currentWaypoint].transform.position;
        }
    }

    //On trigger stay function
    void OnTriggerStay(Collider other)
    {
        //Check for player character
        if (other.tag == "Player")
        {
            //Set the target destination to the NPC’s current position
            myAgent.destination = transform.position;

            //Check if NPC is not talking
            if (!talking)
            {
                //Change text and trigger popup animation if item is found
                if (questItemFound)
                {
                    //Check which NPC it is
                    if (myAgent.tag == "NPC 1")
                    {
                        QuestText.GetComponent<Text>().text = "Granny: Oh, it's my pineapple! Thank you! Here's some food.";
                        popupText.SetTrigger("Popup");
                    }

                    else if (myAgent.tag == "NPC 2")
                    {
                        QuestText.GetComponent<Text>().text = "Grandpa: Wow! I searched for 3 days but couldn't find it. Thanks!";
                        popupText.SetTrigger("Popup");
                    }

                    else if (myAgent.tag == "NPC 3")
                    {
                        QuestText.GetComponent<Text>().text = "Thank you! I was so hungry!";
                        popupText.SetTrigger("Popup");
                    }
                }

                //Trigger the talk animation and activate the quest
                questActive = true;
                GetComponentInChildren<Animator>().SetTrigger("Talk");
                talking = true;
                QuestText.SetActive(true);
            }

            //Make the NPC look at the player
            transform.LookAt(other.transform);
        }
    }

    //On trigger exit function sets NPC back to patrol
    void OnTriggerExit(Collider other)
    {
        //Check for player character
        if (other.tag == "Player")
        {
            //Set the target destination to the NPC’s current position
            myAgent.destination = transform.position;

            //Check if NPC is talking
            if (talking)
            {
                //Make the NPC stop talking and hide the quest text
                GetComponentInChildren<Animator>().SetTrigger("StopTalking");
                talking = false;
                QuestText.SetActive(false);
            }
        }
    }
}
