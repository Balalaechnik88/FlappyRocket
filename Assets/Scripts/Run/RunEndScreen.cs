using UnityEngine;

public class RunEndScreen : MonoBehaviour
{
    private const float TitlePositionYRatio = 0.4f;
    private const float HintOffsetY = 60f;
    private const float TitleHeight = 50f;
    private const float HintHeight = 40f;

    [SerializeField] private RunSession _runSession;
    [SerializeField] private string _title = "GAME OVER";
    [SerializeField] private string _restartHint = "Press R to restart";

    private GUIStyle _titleStyle;
    private GUIStyle _hintStyle;

    private void OnGUI()
    {
        if (_runSession == null || _runSession.IsFinished == false)
            return;

        EnsureStyles();

        float titlePositionY = Screen.height * TitlePositionYRatio;

        GUI.Label(
            new Rect(0f, titlePositionY, Screen.width, TitleHeight),
            _title,
            _titleStyle);

        GUI.Label(
            new Rect(0f, titlePositionY + HintOffsetY, Screen.width, HintHeight),
            _restartHint,
            _hintStyle);
    }

    private void EnsureStyles()
    {
        if (_titleStyle != null && _hintStyle != null)
            return;

        _titleStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 34,
            fontStyle = FontStyle.Bold
        };

        _hintStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 22
        };
    }
}