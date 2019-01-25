using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    const int MAX_STRESS = 100;
    const float LIFE_STRESS_TIME = 2;

    int stress;
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
        }

        // Clamp to maximum stress
        if (stress > MAX_STRESS) stress = MAX_STRESS;
    }
}
