using Unity.VisualScripting;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    public static ControlaAudio instancia;

    [SerializeField] private AudioSource audioSourceMusica;
    [SerializeField] private AudioSource audioSourceSFX;

    private void Awake()
    {
        instancia = this;
    }

    public void PlayOneShot(AudioClip clip, float volumeScale = 0.8f)
    {
        audioSourceSFX.PlayOneShot(clip, volumeScale);
    }

    public void TocarSomGameOver(AudioClip clipDeDerrota)
    {
        audioSourceMusica.Stop();

        audioSourceSFX.PlayOneShot(clipDeDerrota, 1f);
    }

    public void PausarTrilhaSonora(bool isTocando)
    {
        if (isTocando == false)
        {
            audioSourceMusica.Pause();
        }

        else
        {
            audioSourceMusica.UnPause();
        }
    }
}
