using UnityEngine;

public class VictoryParticles : MonoBehaviour
{
    [Header("Particle Settings")]
    [SerializeField] int particleCount = 50;
    [SerializeField] float explosionForce = 5f;
    [SerializeField] float lifetime = 2f;
    [SerializeField] Color[] colors = { Color.yellow, Color.cyan, Color.magenta, Color.white };
    [SerializeField] float particleSize = 0.2f;

    void Start()
    {
        CreateParticles();
    }

    void CreateParticles()
    {
        for (int i = 0; i < particleCount; i++)
        {
            GameObject particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            particle.transform.position = transform.position;
            particle.transform.localScale = Vector3.one * particleSize;

            Rigidbody2D rb = particle.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0.5f;

            Vector2 randomDir = Random.insideUnitCircle.normalized;
            float randomForce = Random.Range(explosionForce * 0.5f, explosionForce);
            rb.AddForce(randomDir * randomForce, ForceMode2D.Impulse);

            SpriteRenderer sr = particle.GetComponent<Renderer>() as SpriteRenderer;
            if (sr == null)
            {
                sr = particle.AddComponent<SpriteRenderer>();
            }

            if (colors.Length > 0)
            {
                sr.color = colors[Random.Range(0, colors.Length)];
            }

            Destroy(particle, lifetime);
        }
    }
}
