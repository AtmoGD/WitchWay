using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Animator loadingAnimator = null;

    private bool isLoading = false;

    private void Start()
    {
        print("Start UI Menu Controller");
    }

    public void ShowStartScreen()
    {
        if (isLoading)
            return;

        print("Show Start Screen");

        animator.SetBool("StartScreenOpen", true);
        animator.SetBool("LevelSelectionOpen", false);
        animator.SetBool("CreditsOpen", false);
    }

    public void ShowLevelSelect()
    {
        if (isLoading)
            return;

        print("Show Level Select");

        animator.SetBool("StartScreenOpen", false);
        animator.SetBool("LevelSelectionOpen", true);
        animator.SetBool("CreditsOpen", false);
    }

    public void ShowCredits()
    {
        if (isLoading)
            return;

        print("Show Credits");

        animator.SetBool("StartScreenOpen", false);
        animator.SetBool("LevelSelectionOpen", false);
        animator.SetBool("CreditsOpen", true);
    }

    public void ExitGame()
    {
        // #if UNITY_EDITOR
        //         UnityEditor.EditorApplication.isPlaying = false;
        // #elif UNITY_STANDALONE
        //         Application.Quit();
        // #endif
    }

    public void LoadLevel(int level)
    {
        if (isLoading)
            return;

        print("Load Level " + level);

        StartCoroutine(LoadLevelCoroutine(level));
    }

    private IEnumerator LoadLevelCoroutine(int _level)
    {
        isLoading = true;

        print("Load Level Coroutine");

        loadingAnimator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        print("Load Level Coroutine 2");

        SceneManager.LoadScene(_level);

        isLoading = false;
    }

}
