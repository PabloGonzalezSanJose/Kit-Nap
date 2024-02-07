using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    // VARIABLES
    public Hand[] hands;

    // Fire bullets
    public float Step = 0.1f;       // retraso entre disparos
    // FireInAllDirections()
    public int RadialBullets;
    public int RadialRepeats;
    // FireConeTowardsDirection()
    public int ConeBullets;
    public float ConeSpread;
    public int ConeRepeats;
    // FireStraightAtDirection()
    public int StraightRepeats;
    // FireGiantBulletAtDirection()


    public void FireInAllDirections()
    {
        StartCoroutine(CFireInAllDirections());
    }
    private IEnumerator CFireInAllDirections()
    {
        foreach (Hand h in hands)
        {
            Vector2 direction = Vector2.down;
            float startAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;           // degrees
            float angle;
            Vector2 shootingVector;
            float spread = 180f / RadialBullets;

            for (int r = 0; r < RadialRepeats; r++)
            {
                for (int i = 0; i < RadialBullets; i++)
                {
                    angle = (startAngle + (i - RadialBullets / 2) * spread) * Mathf.Deg2Rad;     // radians
                    shootingVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    h.Shoot(shootingVector);
                }
                yield return new WaitForSeconds(Step);
            }
        }

    }

    public void FireConeTowardsDirection(Vector2 target)
    {
        StartCoroutine(CFireConeTowardsDirection(target));
    }
    private IEnumerator CFireConeTowardsDirection(Vector2 target)
    {
        foreach (Hand h in hands)
        {
            Vector2 direction = (target - (Vector2)h.transform.position).normalized;
            float startAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;           // degrees
            float angle;
            Vector2 shootingVector;

            for (int r = 0; r < ConeRepeats; r++)
            {
                for (int i = 0; i < ConeBullets; i++)
                {
                    angle = (startAngle + (i - ConeBullets / 2) * ConeSpread) * Mathf.Deg2Rad;     // radians
                    shootingVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    h.Shoot(shootingVector);
                }
                yield return new WaitForSeconds(Step);
            }
        }
    }

    public void FireStraightAtDirection(Vector2 target)
    {
        StartCoroutine(CFireStraightAtDirection(target));
    }
    private IEnumerator CFireStraightAtDirection(Vector2 target)
    {
        foreach (Hand h in hands)
        {
            Vector2 direction = (target - (Vector2)h.transform.position).normalized;

            for (int i = 0; i < StraightRepeats; i++)
            {
                h.Shoot(direction);
                yield return new WaitForSeconds(Step);
            }
        }
    }

    public void FireGiantBulletAtDirection(Vector2 target)
    {
        // choose a hand to shoot
        int handId = Random.Range(0, hands.Length);
        Hand h = hands[handId];

        Vector2 direction = (target - (Vector2)h.transform.position).normalized;
        h.ShootGiant(direction);
    }
}
