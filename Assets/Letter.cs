using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : Interaction
{
    public enum LetterType 
    {
        Paycheck,
        Bill,
    }
    public string letterName;
    public LetterType type;

    public override void FinishWork(PlayerStatus status)
    {
        switch(type) 
        {
            case LetterType.Paycheck:
                status.household.notifications.SpawnNotification($"Paycheck! ${-Cost}", Color.green);
                break;
            case LetterType.Bill:
                status.household.notifications.SpawnNotification($"Paid {letterName}! ${-Cost}", Color.red);
                break;
        }

        Destroy(gameObject);
    }
}
