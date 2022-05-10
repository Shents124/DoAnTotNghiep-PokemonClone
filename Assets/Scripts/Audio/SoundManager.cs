using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;

    [SerializeField] private AudioClip mapMusic;
    [SerializeField] private AudioClip battleMusic;
    [SerializeField] private AudioClip hitSfx;
    [SerializeField] private AudioClip selectBtnSfx;
    [SerializeField] private AudioClip throwBallSfx;
    [SerializeField] private AudioClip breakBallSfx;
    [SerializeField] private AudioClip catchSuccessSfx;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayMapMusic()
    {
        music.clip = mapMusic;
        music.Play();
    }

    public void PlayBattleMusic()
    {
        music.clip = battleMusic;
        music.Play();
    }
    public void PlayHitSfx()
    {
        sfx.PlayOneShot(hitSfx);
    }

    public void PlaySelectButton()
    {
        sfx.PlayOneShot(selectBtnSfx);
    }
    public void PlayThrowBall()
    {
        sfx.PlayOneShot(throwBallSfx);
    }
    public void PlayBreakBall()
    {
        sfx.PlayOneShot(breakBallSfx);
    }
    public void PlayCatchSuccess()
    {
        sfx.PlayOneShot(catchSuccessSfx);
    }
}
