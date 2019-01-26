using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public const int MAX_STRESS = 100;
    public const float LIFE_STRESS_TIME = 0.02f;

    public const int NEED_TOILET_MAX = 13;
    public const int NEED_FOOD_MAX = 17;

    public int stress;
    public int needToilet;
    public int needFood;
    public bool needSleep;
    float lifeStressTimer = LIFE_STRESS_TIME;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate() {
        // Life is hard
        lifeStressTimer -= Time.fixedDeltaTime;
        if (lifeStressTimer <= 0) {
            lifeStressTimer += LIFE_STRESS_TIME;
            stress += 1;

            if (Random.value > 0.7f) needFood += 1;
            if (Random.value > 0.7f) needToilet += 1;
            
            if (needToilet >= NEED_TOILET_MAX) stress += 1;
            if (needFood >= NEED_TOILET_MAX) stress += 1;
        }


        // Clamp to maximum stress
        if (stress > MAX_STRESS) stress = MAX_STRESS;
        if (needToilet > NEED_TOILET_MAX) needToilet = NEED_TOILET_MAX;
        if (needFood > NEED_FOOD_MAX) needFood = NEED_FOOD_MAX;
    }
}
