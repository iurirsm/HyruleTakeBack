using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioClip jumpClip;
    AudioSource src;
    PlayerController pc;

    void Awake() { src = GetComponent<AudioSource>(); pc = GetComponent<PlayerController>(); }
    void Update()
    {
        if (UnityEngine.Input.GetButtonDown("Jump") && pc != null) TryPlay();
    }
    void TryPlay()
    {
        if (jumpClip == null) return;
        src.PlayOneShot(jumpClip, 0.7f);
    }
}