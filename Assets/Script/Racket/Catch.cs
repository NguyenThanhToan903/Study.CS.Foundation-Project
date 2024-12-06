using UnityEngine;

public class Catch : MonoBehaviour
{
    [SerializeField]
    private ScoreSystem scoreSystem;

    [SerializeField]
    private AudioManager audioManager;

    //private void Awake()
    //{
    //    audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<RabbitMovement>())
        {
            scoreSystem.takeScore();
            audioManager.PlaySFX(audioManager.catchClip);
            Destroy(other.gameObject);
        }
    }
}
