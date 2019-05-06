using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pause_menu_ui;        // pause menu panel GO under pauseGameCanvas
    public GameObject pause_button;         // pause button in the in-game_canvas GO

    public static bool is_game_paused { get; private set; }
    public static bool isGamePaused { set { is_game_paused = value; } }

    public static PauseMenu instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        is_game_paused = false;
    }

    void Update()
    {

    }

    public void pauseGame()
    {
        pause_menu_ui.SetActive(true);      // enable pause menu
        pause_button.SetActive(false);      // disable pause button
        is_game_paused = true;
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        pause_menu_ui.SetActive(false);     // disable pause menu
        pause_button.SetActive(true);       // enable pause button
        is_game_paused = false;
        Time.timeScale = 1;
    }

    public void otherPauseButtons()
    {
        pause_menu_ui.SetActive(false);     // disable pause menu
        Time.timeScale = 1;
    }
}
