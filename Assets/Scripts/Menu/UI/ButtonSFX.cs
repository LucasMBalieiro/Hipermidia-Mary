using Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ButtonSFX : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    [SerializeField] private SoundData onHoverSound;
    [SerializeField] private SoundData onClickSound;

    public void OnPointerEnter(PointerEventData eventData) => SoundManager.Instance.CreateSound().Play(onHoverSound);

    public void OnPointerClick(PointerEventData eventData) => SoundManager.Instance.CreateSound().Play(onClickSound);
}
