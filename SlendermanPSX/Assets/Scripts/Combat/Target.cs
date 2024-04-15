
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 100f;
    public Slider slider;

    public void TakeDamage(float amount)
    {
        health -= amount;
        slider.value = health;
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Knockback(Vector3 direction, float force)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        // rb.AddForce(1000 * force * direction);
        for(int i = 0; i < 10000; i++)
            transform.position += direction * force / 10000;
    }
   
}
