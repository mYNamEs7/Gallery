using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class ImageLoader
{
    public static IEnumerator Load(string url, Image target)
    {
        var request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Ошибка загрузки: {url}");
            yield break;
        }

        Texture2D texture = DownloadHandlerTexture.GetContent(request);

        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            Vector2.one * 0.5f
        );

        target.sprite = sprite;
    }
}