
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------Audio Clip---------")]
    public AudioClip Audio_CompeleteMap;
    public AudioClip Audio_Player_Attack_1;
    public AudioClip Audio_Player_Attack_2;
    public AudioClip Audio_Player_Attack_In_Monters;
    public AudioClip Audio_Player_Death;
    public AudioClip Audio_Player_Hurt;
    public AudioClip Audio_TakeItem;
    public AudioClip Audio_Player_Run;
    private void Start()
    {
        musicSource.clip = Audio_CompeleteMap;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
