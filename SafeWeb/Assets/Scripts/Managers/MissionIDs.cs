using System.Collections.Generic;
using UnityEngine;

public class MissionIDs : MonoBehaviour
{

    public List<int> missionIDs = new List<int>();

    int missionsSolved = 0;
    int missionCounter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int GetMissionID()
    {
        return missionIDs[missionCounter];
    }

    public void MissionSolved()
    {
        missionsSolved += 1;
        missionCounter += 1;
    }

    public bool isSolved()
    {
        if (missionsSolved > 0)
        {
            return true;
        }
        return false;
    }

    public void ResetMissions()
    {
        missionsSolved = 0;
    }
}
