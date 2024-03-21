using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class CaptureText : MonoBehaviour
{
    public Image image;
    public Transform parent;
    public DrawViewController drawView;

    private void Start()
    {
        this.image = this.GetComponent<Image>();
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            CaptureView();
    }

    public void CaptureView()
    {
        RectTransform imageRect = image.rectTransform;
        int width = Mathf.RoundToInt(imageRect.rect.width/3);
        int height = Mathf.RoundToInt(imageRect.rect.height/3);

        // Create a new Texture2D with transparency
        Texture2D capturedTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        RenderTexture renderTexture = new RenderTexture(width, height, 32);
        RenderTexture.active = renderTexture;

        // Render the image to the render texture
        Graphics.Blit(image.sprite.texture, renderTexture);

        // Read the pixels from the render texture and apply them to the captured texture
        capturedTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        capturedTexture.Apply();

        // Create a new Sprite with the captured texture
        Sprite capturedSprite = Sprite.Create(capturedTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

        // Create a new GameObject to hold the captured image
        GameObject capturedImageGO = new GameObject("CapturedImage");

        // Add a RectTransform component to the GameObject
        RectTransform imageRectTrans = capturedImageGO.AddComponent<RectTransform>();

        // Set the size of the RectTransform to match the captured image's size
        imageRectTrans.sizeDelta = new Vector2(width, height);

        // Add an Image component to the GameObject and set its sprite to the captured sprite
        Image capturedImage = capturedImageGO.AddComponent<Image>();
        capturedImage.sprite = capturedSprite;

        // Set the captured image's parent to the parent object
        capturedImageGO.transform.SetParent(parent, false);

        // Clean up
        RenderTexture.active = null;
        Destroy(renderTexture);

        if(this.drawView != null) this.drawView.ClearBtn();
        Debug.Log("Image captured and instantiated as a child of the parent object!");
    }
}
