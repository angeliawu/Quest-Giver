using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    //Variable to keep track of whether player has collected the quest item
    public bool itemCollected;

    //Public reference to NPC script
    public NPC questGiver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //On trigger enter function
    void OnTriggerEnter(Collider other)
    {
        //Check if quest is active before collecting item
        if (other.tag == "Player" && questGiver.questActive)
        {
            itemCollected = true;
        }
    }
}
