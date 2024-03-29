﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Constants
{
    public static class Consts
    {
        //各プレイヤーの駒の配置制限個数
        public const int PIECE_LIMIT = 5;

        public static readonly Dictionary<SceneNames, string> Scenes = new()
        {
            [SceneNames.TITLE_SCENE] = "Title",
            [SceneNames.GAME_SCENE] = "MainGame",
            [SceneNames.RESULT_SCENE] = "Result",
        };

        /// <summary> 座標からオブジェクトを検索する </summary>
        /// <param name="pos"> 指定した座標 </param>
        /// <returns> 得られたオブジェクト </returns>
        public static GameObject FindWithVector(Vector3 pos)
        {
            GameObject find = null;

            foreach (GameObject obj
                     in Object.FindObjectsOfType(typeof(GameObject)).Cast<GameObject>())
            {
                if (pos == obj.transform.position)
                {
                    find = obj;
                    break;
                }
            }
            return find;
        }
    }

    public enum SceneNames
    {
        TITLE_SCENE,
        GAME_SCENE,
        RESULT_SCENE,
    }

    public enum Turns
    {
        RED,
        BLACK,
    }

    public enum MoveType
    {
        DEFAULT,
        SET,
        MOVE
    }

    public enum JudgeResult
    {
        DRAW,
        RED_WIN,
        BLACK_WIN,
    }
}