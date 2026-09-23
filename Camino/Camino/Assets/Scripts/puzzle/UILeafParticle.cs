using UnityEngine;

public class UILeafParticle : MonoBehaviour
{
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // This triggers every time the UI object is enabled
    void OnEnable()
    {
        if (ps != null)
        {
            ps.Clear(); // Clears old leaves
            ps.Play();  // Starts the new ones
        }
    }
}
