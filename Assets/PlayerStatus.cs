using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public const int MAX_STRESS = 100;
    public const float LIFE_STRESS_TIME = 2f;

    public const int NEED_TOILET_MAX = 13;
    public const int NEED_FOOD_MAX = 17;

    public int stress;
    public int stressRate;
    public int needToilet;
    public int needFood;
    public bool needSleep;
    public bool sleepyTime;
    float lifeStressTimer = LIFE_STRESS_TIME;
	public bool chilling;
	public bool sleeping;

    public Household household;
    private SpriteRenderer face;

    // Start is called before the first frame update
    void Start()
    {
        household = FindObjectsOfType<MonoBehaviour>().OfType<Household>().First();
        face = transform.Find("FacialExpression").GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        sleepyTime = household.Hour <= 7 && household.Hour >= 3;

        stressRate = 1;
        
        if (needToilet >= NEED_TOILET_MAX) stressRate += 1;
        if (needFood >= NEED_TOILET_MAX) stressRate += 1;
        if (chilling) stressRate -= 2;

        if (sleepyTime){
            if (sleeping) {
                stressRate -= 1;
            } else {
                stressRate += 1;
            }
        }

        // Life is hard
        lifeStressTimer -= Time.fixedDeltaTime;
        if (lifeStressTimer <= 0) {
            lifeStressTimer += LIFE_STRESS_TIME;

            if (Random.value > 0.7f) needFood += 1;
            if (Random.value > 0.7f) needToilet += 1; 
        }


        // Clamp to maximum stress
        if (stress > MAX_STRESS) stress = MAX_STRESS;
        if (needToilet > NEED_TOILET_MAX) needToilet = NEED_TOILET_MAX;
        if (needFood > NEED_FOOD_MAX) needFood = NEED_FOOD_MAX;
    }
}
