using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public static class Consts
    {
        //各プレイヤーの持ち時間
        public const float GAME_TIME_ONE = 100f;
        public const float GAME_TIME_TWO = 100f;

        public static readonly Dictionary<SceneNames, string> Scenes = new()
        {
            [SceneNames.TITLE_SCENE] = "Title",
            [SceneNames.GAME_SCENE] = "MainGame",
            [SceneNames.RESULT_SCENE] = "Result",
        };
    }

    public enum SceneNames
    {
        TITLE_SCENE,
        GAME_SCENE,
        RESULT_SCENE,
    }

    public enum JudgeResult
    {
        DRAW,
        WHITE_WIN,
        BLACK_WIN,
    }
}