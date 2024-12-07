using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource musicAudioSource;

    [SerializeField]
    public AudioSource vfxAudioSource;

    [SerializeField]
    public AudioClip musicClip;

    [SerializeField]
    public AudioClip winClip;

    [SerializeField]
    public AudioClip catchClip;

    [SerializeField]
    public AudioClip buttonClip;


    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        if (musicAudioSource != null && musicClip != null)
        {
            Debug.Log("Đang phát âm thanh: " + musicClip.name);
            musicAudioSource.clip = musicClip;
            musicAudioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource hoặc AudioClip không được thiết lập!");
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.PlayOneShot(sfxClip);
    }



}
