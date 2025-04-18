using System;
using UnityEngine;

public class CursorControllerComplex : MonoBehaviour
{
    public static CursorControllerComplex Instance { get; private set; }
    [SerializeField] private Texture2D cursorTextureDefault;
    [SerializeField] private Texture2D cursorTextureQuestion;
    [SerializeField] private Texture2D cursorTextureSpeech;
    [SerializeField] private Texture2D cursorTextureAttack;
    
    [SerializeField] private Vector2 clickPosition = Vector2.zero;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
    }

    public void SetToMode(ModeOfCursor modeOfCursor)
    {
        switch (modeOfCursor)
        {
            case ModeOfCursor.Default:
                Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
                break;
            case ModeOfCursor.Question:
                Cursor.SetCursor(cursorTextureQuestion, clickPosition, CursorMode.Auto);
                break;
            case ModeOfCursor.Speech:
                Cursor.SetCursor(cursorTextureSpeech, clickPosition, CursorMode.Auto);
                break;
            case ModeOfCursor.Attack:
                Cursor.SetCursor(cursorTextureAttack, clickPosition, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
                break;
        }
    }
    public enum ModeOfCursor
    {
        Default,
        Question,
        Speech,
        Attack
    }

}
