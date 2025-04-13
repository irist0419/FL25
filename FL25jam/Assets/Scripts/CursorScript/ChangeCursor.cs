using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CursorControllerComplex.ModeOfCursor modeOfCursor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorControllerComplex.Instance.SetToMode(modeOfCursor);
    }

    // Update is called once per frame
    public void OnPointerExit(PointerEventData eventData)
    {
        CursorControllerComplex.Instance.SetToMode(CursorControllerComplex.ModeOfCursor.Default);
    }
    
}
