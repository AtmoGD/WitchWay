using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Animator loadingAnimator = null;

    private bool isLoading = false;

    public void ShowStartScreen()
    {
        if (isLoading)
            return;

        animator.SetBool("StartScreenOpen", true);
        animator.SetBool("LevelSelectionOpen", false);
        animator.SetBool("CreditsOpen", false);
    }

    public void ShowLevelSelect()
    {
        if (isLoading)
            return;

        animator.SetBool("StartScreenOpen", false);
        animator.SetBool("LevelSelectionOpen", true);
        animator.SetBool("CreditsOpen", false);
    }

    public void ShowCredits()
    {
        if (isLoading)
            return;

        animator.SetBool("StartScreenOpen", false);
        animator.SetBool("LevelSelectionOpen", false);
        animator.SetBool("CreditsOpen", true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
                Application.Quit();
#endif
    }

    public void LoadLevel(int level)
    {
        if (isLoading)
            return;

        StartCoroutine(LoadLevelCoroutine(level));
    }

    private IEnumerator LoadLevelCoroutine(int _level)
    {
        isLoading = true;

        loadingAnimator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(_level);

        isLoading = false;
    }

}
