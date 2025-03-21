/*
    TapEffectAnimation.cs

    タップエフェクト・ロングタップエフェクトアニメーションクラス
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TapEffectAnimation : MonoBehaviour
{
    // スプライト配列の添え字指定用
    private int spritesNum;

    /// <summary>
    /// 任意の秒数待った後にスプライトを切り替える処理
    /// </summary>
    /// <param name="_sprites">使用するスプライトが格納された配列</param>
    /// <param name="_samples">待つ秒数</param>
    /// <returns></returns>
    public IEnumerator EffectAnimation(Sprite[] _sprites, float _samples)
    {
        // 指定した秒数待つ
        yield return new WaitForSeconds(_samples);

        // スプライトが格納されている配列の添え字を変更
        if (spritesNum < _sprites.Length - 1)
        {
            spritesNum++;
        }
        else
        {
            // 添え字、スプライトのリセット
            spritesNum = 0;
            this.gameObject.GetComponent<Image>().sprite = _sprites[spritesNum];

            // オブジェクトを非表示
            this.gameObject.SetActive(false);

            // 処理終わりたいときは yield break
            yield break;
        }

        // スプライトを変更しアニメーション
        this.gameObject.GetComponent<Image>().sprite = _sprites[spritesNum];

        // 待った後にもう一度呼ぶ　再帰する
        StartCoroutine(EffectAnimation(_sprites, _samples));
    }

}
