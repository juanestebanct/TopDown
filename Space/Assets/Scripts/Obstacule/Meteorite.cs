using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : Obstacle
{
    public GenerationAsteroid GenerationAsteroid;
    private Rigidbody2D rb;
    private float force = 50f;

    [SerializeField] private int level;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 tempMeteorite, float torque)
    {
        tempMeteorite.Normalize();
        rb.AddForce(tempMeteorite * force, ForceMode2D.Impulse);
        rb.AddTorque(torque);

        transform.rotation = Quaternion.Euler(0, Random.value * 180.0f, 0);

    }

    private void DesactiveMeteorite()
    {
        if (level != 1)
        {
            GenerationAsteroid.SpinBigAsteroid(transform.position);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public override void ResiveDamage(float Damage)
    {
        print("colliciono con el arma ");
        Score.Instance.GetPoins(Point);
        AudioManager.instance.PlayClip(AudioManager.instance.Explocion);
        DesactiveMeteorite();
    }
}
