using Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 指定したシーンに遷移する
/// </summary>
public class SceneLoaders : MonoBehaviour
{
    [Tooltip("遷移先のシーン")]
    [SerializeField] private SceneNames _nextScene = default;

    /// <summary> フェードアウト -> シーン遷移 </summary>
    /// <param name="sceneName"> 遷移先のシーン名 </param>
    private void PassToLoad(string sceneName)
    {
        Fade.StartFadeOut
            (() => SceneManager.LoadScene(sceneName));
    }

    /// <summary> フェードアウト -> シーン遷移
    ///          (シーン上のPanel,Button等に設定) </summary>
    public void LoadToScene()
    {
        PassToLoad(Consts.Scenes[_nextScene]);
    }
}