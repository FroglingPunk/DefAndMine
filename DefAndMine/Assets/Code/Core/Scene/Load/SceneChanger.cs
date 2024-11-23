using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger
{
    public static async UniTask LoadScene(EScene newEScene)
    {
        var activeEScene = ESceneExtensions.GetActiveEScene();

        // load  loading screen scene
        await SceneManager.LoadSceneAsync(EScene.LoadingScreen.SceneName(), LoadSceneMode.Additive);

        // deinit prev scene
        await LocalSceneContainer.Instance.DeinitAsync();

        // unload prev scene
        await SceneManager.UnloadSceneAsync(activeEScene.SceneName());

        // load new scene
        await SceneManager.LoadSceneAsync(newEScene.SceneName(), LoadSceneMode.Additive);
        var newLoadedScene = SceneManager.GetSceneByName(newEScene.SceneName());
        SceneManager.SetActiveScene(newLoadedScene);

        // init new scene
        var newSceneInitializer = Object.FindObjectOfType<SceneInitializerBase>();
        var newLocalSceneContainer = new LocalSceneContainer(newSceneInitializer);
        await newLocalSceneContainer.InitAsync();

        // unload loading screen scene
        await SceneManager.UnloadSceneAsync(EScene.LoadingScreen.SceneName());
    }
}