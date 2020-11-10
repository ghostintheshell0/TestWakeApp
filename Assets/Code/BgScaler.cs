using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScaler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background = default;
    [SerializeField] private Camera currentCamera = default;
    private void Start()
    {
        Scale();
    }

    public void Scale()
    {
        if(background == null) return;

        transform.localScale = new Vector3(1,1,1);

        float width = background.sprite.bounds.size.x;
        float height = background.sprite.bounds.size.y;

        float worldScreenHeight = currentCamera.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        var scale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1f);
        transform.localScale = scale;
    }
}
