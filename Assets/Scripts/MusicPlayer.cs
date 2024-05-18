
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public float MusicEndsIn = 99999.0f;
    public AudioClip[] peaceful;
    public AudioClip[] enemy;
    public AudioSource source;

    private bool attackMusic = false;
    private bool transitioning = false;

    private void Awake()
    {
        // Removes duplicates
        if (GameObject.FindGameObjectsWithTag("Music Player").Length != 1)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 && !attackMusic)
        {
            FadeInTransition(enemy);
            ;
            attackMusic = true;
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && attackMusic)
        {
            FadeInTransition(peaceful);
            attackMusic = false;
        }
        if (!source.isPlaying)
        {
            Play(peaceful);
        }
        else
        {
            if (source.clip != null)
            {
                MusicEndsIn = source.clip.length - source.time;
            }
        }

        if (MusicEndsIn <= 5.0f && !transitioning)
        {
            FadeInTransition(peaceful);
        }
    }

    public void FadeInTransition(AudioClip[] music)
    {

        StartCoroutine(IFadeInTransition(music));
    }

    private IEnumerator IFadeInTransition(AudioClip[] music)
    {
        float timePassed = 0.0f;
        transitioning = true;
        float duration = 2.5f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            source.volume = 1.0f - (timePassed / duration);
            yield return null;
        }

        source.volume = 1.0f;
        Play(music);
        transitioning = false;
    }

    private void Play(AudioClip[] music)
    {
        int random = Random.Range(0, music.Length);
        source.clip = music[random];
        source.Play();
    }
}
