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

    public void AddForce(Vector2 tempMeteorite)
    {
        tempMeteorite.Normalize();
        rb.AddForce(tempMeteorite * force, ForceMode2D.Impulse);
    }

    private void Desactive()
    {
        if (level != 0)
        {
            GenerationAsteroid.SpinBigAsteroid();
        }
    }
    public override void ResiveDamage(float Damage)
    {
        Score.Instance.GetPoins(Point);
        AudioManager.instance.PlayClip(AudioManager.instance.Explocion);
        Desactive();
    }
}
