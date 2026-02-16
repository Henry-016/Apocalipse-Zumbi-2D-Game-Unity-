using UnityEngine;
using UnityEngine.EventSystems;
public class SonsInterface : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Configurações de Áudio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip somDeHover;
    [SerializeField] private AudioClip somDeClick;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (somDeHover != null)
        {
            audioSource.PlayOneShot(somDeHover);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (somDeClick != null)
        {
            audioSource.PlayOneShot(somDeClick);
        }
    }
}