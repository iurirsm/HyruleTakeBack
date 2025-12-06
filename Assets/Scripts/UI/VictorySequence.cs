using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class VictorySequence : MonoBehaviour
{
    [Header("Time Effects")]
    [SerializeField] float slowMotionScale = 0.3f;
    [SerializeField] float slowMotionDuration = 1.5f;

    [Header("Camera Shake")]
    [SerializeField] float shakeIntensity = 3f;
    [SerializeField] float shakeDuration = 0.5f;

    [Header("Particles")]
    [SerializeField] GameObject victoryParticlePrefab;
    [SerializeField] Transform particleSpawnPoint;

    [Header("Audio")]
    [SerializeField] AudioClip victorySound;

    [Header("UI")]
    [SerializeField] WinUI winUI;

    AudioSource audioSource;
    CinemachineVirtualCamera vCam;
    CinemachineBasicMultiChannelPerlin noise;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        vCam = FindFirstObjectByType<CinemachineVirtualCamera>();
        if (vCam != null)
            noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (winUI == null)
            winUI = FindFirstObjectByType<WinUI>(FindObjectsInactive.Include);
    }

    public void PlayVictorySequence(Vector3 position)
    {
        StartCoroutine(VictoryRoutine(position));
    }

    IEnumerator VictoryRoutine(Vector3 position)
    {
        Time.timeScale = slowMotionScale;

        if (victorySound)
            audioSource.PlayOneShot(victorySound);

        if (noise != null)
            StartCoroutine(CameraShakeRoutine());

        if (victoryParticlePrefab)
        {
            Vector3 spawnPos = particleSpawnPoint ? particleSpawnPoint.position : position;
            GameObject particles = Instantiate(victoryParticlePrefab, spawnPos, Quaternion.identity);
            Destroy(particles, 5f);
        }

        yield return new WaitForSecondsRealtime(slowMotionDuration);

        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(0.5f);

        if (winUI != null)
            winUI.Show();
    }

    IEnumerator CameraShakeRoutine()
    {
        if (noise == null) yield break;

        noise.AmplitudeGain = shakeIntensity;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float normalizedTime = elapsed / shakeDuration;
            noise.AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, normalizedTime);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        noise.AmplitudeGain = 0f;
    }
}
