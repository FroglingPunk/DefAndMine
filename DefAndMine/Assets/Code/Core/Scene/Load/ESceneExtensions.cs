using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class ESceneExtensions
{
    private static Dictionary<EScene, string> _sceneNames = new()
    {
        { EScene.LoadingScreen, "LoadingScreen" },
        { EScene.Init, "Init" },
        { EScene.Game, "Game" }
    };


    public static string SceneName(this EScene escene)
    {
        return _sceneNames.ContainsKey(escene) ? _sceneNames[escene] : default;
    }

    public static EScene GetActiveEScene()
    {
        var activeSceneName = SceneManager.GetActiveScene().name;
        return _sceneNames.FirstOrDefault(x => x.Value == activeSceneName).Key;
    }
}