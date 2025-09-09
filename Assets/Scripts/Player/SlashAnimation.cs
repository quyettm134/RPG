using UnityEngine;

public class SlashAnimation : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (ps && !ps.IsAlive())
        {
            SelfDestruct();
        }   
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
