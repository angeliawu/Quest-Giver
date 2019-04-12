using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Quest
{
    public enum Status { INACTIVE, UNDERWAY, COMPLETE }
    public string name;
    public Status status;
}

[CreateAssetMenu()]

public class QuestManager : ScriptableObject
{
    static QuestManager instance;
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance<QuestManager>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    void Awake()
    {
        instance = this;
    }

    public Quest[] quests;
}
