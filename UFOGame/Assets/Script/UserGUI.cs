using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    bool isFirst = true;
    GUIStyle style;
    // Use this for initialization  
    void Start()
    {
        action = Director.getInstance().currentSceneControl as IUserAction;
        style = new GUIStyle();
        style.fontSize = 25;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width/2, 100, 400, 400), "Round:" +  (FirstSceneControl.getCurrentRount()+1).ToString(),style);
        if (Input.GetButtonDown("Fire1"))
        {

            Vector3 pos = Input.mousePosition;
            action.hit(pos);

        }

        GUI.Label(new Rect(1000, 0, 400, 400), "Score:" + action.GetScore().ToString(), style);

        if (isFirst && GUI.Button(new Rect(700, 100, 90, 90), "Start"))
        {
            isFirst = false;
            action.setGameState(GameState.ROUND_START);
        }

        if (!isFirst && action.getGameState() == GameState.ROUND_FINISH && GUI.Button(new Rect(700, 100, 90, 90), "Next Round"))
        {
            action.setGameState(GameState.ROUND_START);
        }
    }
}