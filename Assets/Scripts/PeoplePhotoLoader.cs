using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PeoplePhotoLoader : MonoBehaviour
{
    public string getImageUrl = "http://localhost/kongwahServer/getPhoto.php";
    public RawImage image;
    private string currentImageUrl;

    IEnumerator Start()
    {
        while (true)
        {
            // Create a UnityWebRequest to get the image URL
            using (UnityWebRequest www = UnityWebRequest.Get(getImageUrl))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Failed to get image URL: " + www.error);
                }
                else
                {
                    string imageUrl = www.downloadHandler.text;

                    // Check if a new image URL is received
                    if (!string.IsNullOrEmpty(imageUrl) && imageUrl != currentImageUrl)
                    {
                        currentImageUrl = imageUrl;
                        yield return LoadImageFromURL(imageUrl);
                    }
                }
            }

            // Wait for a certain amount of time before checking again
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator LoadImageFromURL(string imageUrl)
    {
        // Load the image using the received URL
        using (UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return imageRequest.SendWebRequest();

            if (imageRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Failed to download image: " + imageRequest.error);
            }
            else
            {
                Debug.Log("downloaded image");
                // Get the downloaded texture
                Texture2D originalTexture = DownloadHandlerTexture.GetContent(imageRequest);

                //Texture2D tex = rotateTexture(originalTexture);

                // Apply the texture to the image renderer
                if (image != null)
                {
                    image.texture = originalTexture;
                }
            }
        }
    }


    public Texture2D rotateTexture(Texture2D image)
    {

        Texture2D target = new Texture2D(image.height, image.width, image.format, false);    //flip image width<>height, as we rotated the image, it might be a rect. not a square image

        Color32[] pixels = image.GetPixels32(0);
        pixels = rotateTextureGrid(pixels, image.width, image.height);
        target.SetPixels32(pixels);
        target.Apply();

        //flip image width<>height, as we rotated the image, it might be a rect. not a square image

        return target;
    }


    public Color32[] rotateTextureGrid(Color32[] tex, int wid, int hi)
    {
        Color32[] ret = new Color32[wid * hi];      //reminder we are flipping these in the target

        for (int y = 0; y < hi; y++)
        {
            for (int x = 0; x < wid; x++)
            {
                //ret[(hi - 1) - y + x * hi] = tex[x + y * wid];         //juggle the pixels around
                ret[y + (wid - 1 - x) * hi] = tex[x + y * wid];

            }
        }

        return ret;
    }
}
