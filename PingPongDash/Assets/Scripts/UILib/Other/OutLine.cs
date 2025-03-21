/*
    OutLine.cs

    アウトライン生成クラス
*/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Text))]
public class OutLine : BaseMeshEffect
{
    #region - アウトラインの色
    [Header("アウトラインの色")]
    #endregion
    [SerializeField] Color color = Color.white;

    #region - アウトラインの太さ
    [Header("アウトラインの太さ")]
    [Tooltip("0,0～10.0の値。0が細い、10が太い。")]
    [Range(uiCommon.MIN_THICKNESS, uiCommon.MAX_THICKNESS)]
    #endregion
    [SerializeField] float thickness = 3.0f; 

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive() || vh.currentVertCount == 0)
            return;

        // オリジナルの頂点データを取得
        List<UIVertex> originalVertices = new List<UIVertex>();
        vh.GetUIVertexStream(originalVertices);

        int originalVertexCount = originalVertices.Count;
        List<UIVertex> modifiedVertices = new List<UIVertex>();

        // 8方向にアウトラインを生成
        Vector2[] directions = {
            new Vector2(-1, -1), new Vector2(-1,  1),
            new Vector2( 1, -1), new Vector2( 1,  1),
            new Vector2(-1,  0), new Vector2( 1,  0),
            new Vector2( 0, -1), new Vector2( 0,  1)
        };

        foreach (var dir in directions)
        {
            for (int i = 0; i < originalVertexCount; i++)
            {
                UIVertex vertex = originalVertices[i];
                vertex.position += new Vector3(dir.x * thickness, dir.y * thickness);
                vertex.color = color;
                modifiedVertices.Add(vertex);
            }
        }

        // 元の頂点データを追加
        modifiedVertices.AddRange(originalVertices);

        // 更新された頂点データを反映
        vh.Clear();
        vh.AddUIVertexTriangleStream(modifiedVertices);
    }
}