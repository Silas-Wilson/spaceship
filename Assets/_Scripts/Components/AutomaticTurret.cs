using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class AutomaticTurret : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float _rotationTime;
    [Header("External Components")]
    [SerializeField] ShipComponent component;
    [SerializeField] PlayerProjectile projectilePrefab;
    [SerializeField] GameObject gunShaft;
    [SerializeField] Detection detection;
    bool canFire;
    const string ENEMY_TAG = "enemy";
    void Start()
    {
        canFire = true;
    }
    void Update()
    {
        #region Find Target

        GameObject currentTarget = null;

        foreach (GameObject entity in detection.DetectedEntities)
        {
            if (entity != null && entity.CompareTag(ENEMY_TAG))
            {
                currentTarget = entity;
                break;
            }
        }

        #endregion

        #region Attack

        if (currentTarget == null)
        {
            return;
        }

        Vector2 enemyDirection = (currentTarget.transform.position - transform.position).normalized;
        Vector2 enemyVelocity = currentTarget.GetComponentInParent<Rigidbody2D>().linearVelocity;

        Vector2 enemyParallelVelocity = Vector2.Dot(enemyVelocity, enemyDirection) * enemyDirection;
        Vector2 enemyPerpendicularVelocity = enemyVelocity - enemyParallelVelocity;
        float enemyPerpendicularSpeed = enemyPerpendicularVelocity.magnitude;

        float cross = enemyDirection.x * enemyPerpendicularVelocity.y - enemyDirection.y * enemyPerpendicularVelocity.x;

        float ratio = enemyPerpendicularSpeed / component.Stats.ProjectileSpeed;
        ratio = Mathf.Clamp(ratio, -1f, 1f);

        float fireAngle = Mathf.Asin(ratio);
        float enemyAngle = Mathf.Atan2(enemyDirection.y, enemyDirection.x);

        Quaternion idealAimPosition = quaternion.Euler(0, 0, enemyAngle + Mathf.Sign(cross) * fireAngle);

        gunShaft.transform.rotation = Quaternion.Lerp(gunShaft.transform.rotation, idealAimPosition, _rotationTime);

        if (canFire)
        {
            PlayerProjectile currentBullet = Instantiate(projectilePrefab, transform.position, gunShaft.transform.rotation);
            currentBullet.Initialize(component);
            StartCoroutine(BulletCooldown());
        }

        #endregion
    }

    IEnumerator BulletCooldown()
    {
        canFire = false;
        float timeLeft = component.Stats.FireRate;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        canFire = true;
    }

    Vector2 Rotate(Vector2 v, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }
}
