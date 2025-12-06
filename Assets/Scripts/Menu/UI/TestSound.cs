using Audio;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestSound : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SoundData testSound;


    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.CreateSound().Play(testSound);
    }
}
