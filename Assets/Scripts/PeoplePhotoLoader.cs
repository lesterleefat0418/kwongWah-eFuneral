using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SFB;
using System;

public class PeoplePhotoLoader : MonoBehaviour
{
    public static PeoplePhotoLoader Instance = null;
    public string getImageUrl = "";
    public Texture originalImage, originalHallPeopleImage, originalResultPeopleImage;
    public RawImage image, hallPeopleImage, resultPeopleImage;
    private string currentImageUrl;
    public CanvasGroup resetBtn;
    public float latestUploadPhotoWithinTime = 5;
    public bool showOncePopup = false;

    public string GetImageApi
    {
        set
        {
            this.getImageUrl = value;
        }
        get
        {
            return this.getImageUrl;
        }
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;


        if (this.image != null && this.image.texture != null)
            this.originalImage = this.image.texture;

        if (this.hallPeopleImage != null && this.hallPeopleImage.texture != null)
            this.originalHallPeopleImage = this.hallPeopleImage.texture;

        if (this.resultPeopleImage != null && this.resultPeopleImage.texture != null)
            this.originalResultPeopleImage = this.resultPeopleImage.texture;

        if(this.resetBtn != null)
        {
            SetUI.Set(this.resetBtn, false, 0.75f, 0f);
        }

    }

    private void OnEnable()
    {
        StartCoroutine(CheckUploadPhoto());
        this.showOncePopup = false;
    }

    private void OnDisable()
    {
        this.StopCheckUploadPhoto();
    }

    public void StopCheckUploadPhoto()
    {
        StopCoroutine(CheckUploadPhoto());
        this.showOncePopup = false;
    }

    IEnumerator CheckUploadPhoto()
    {       
        if(LoaderConfig.Instance != null)
        {
            this.GetImageApi = "http://localhost/" + LoaderConfig.Instance.configData.getImageFolderName + "/getPhoto.php";
            this.latestUploadPhotoWithinTime = LoaderConfig.Instance.configData.latestUploadPhotoWithinTime;
        }

        while (this.enabled && !string.IsNullOrEmpty(this.GetImageApi))
        {
            // Create a UnityWebRequest to get the image URL
            using (UnityWebRequest www = UnityWebRequest.Get(this.GetImageApi))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log("Failed to get image URL: " + www.error);
                    if(LoaderConfig.Instance!= null && !this.showOncePopup) { 
                        LoaderConfig.Instance.setServerPopup(true, www.error);
                        this.showOncePopup = true;

                        yield return new WaitForSeconds(5f);
                    }
                }
                else
                {
                    string response = www.downloadHandler.text;

                    if (response.StartsWith("NEW_IMAGE:"))
                    {
                        Debug.Log(response);
                        string[] parts = response.Split(',');
                        string latestImageFile = parts[0].Substring("NEW_IMAGE:".Length);
                        long creationTime = long.Parse(parts[1]);
                        //Debug.Log("creationTime:" + creationTime);
                        // Convert the creation time to a DateTime object
                        DateTime createdAt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(creationTime);
                        createdAt = createdAt.ToUniversalTime().AddHours(8);
                        //Debug.Log("createdAt:" + createdAt);
                        //Debug.Log("Now:" + DateTime.Now);
                        // Check if the image was created within the last 10 seconds
                        if ((DateTime.Now - createdAt).TotalSeconds <= this.latestUploadPhotoWithinTime)
                        {
                            //Debug.Log(response);
                            Debug.Log("New image created within the last 10 seconds: " + latestImageFile);
                            currentImageUrl = latestImageFile;
                            yield return LoadImage(latestImageFile);
                        }
                        else
                        {
                            Debug.Log("no new image: ");
                        }
                    }
                    else if (response == "NO_NEW_IMAGE")
                    {
                        Debug.Log("No new image message: " + response);
                    }
                    else
                    {
                        Debug.Log("Unexpected response from server: " + response);
                    }
                }
            }

            // Wait for a certain amount of time before checking again
            yield return new WaitForSeconds(1f);
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


    public void LoadImageFromDesktop()
    {
        var extensions = new[] {
                new ExtensionFilter("Image Files", "png", "jpg", "jpeg" )
            };

        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Image", "", extensions, false);

        if (paths.Length > 0)
        {
            string path = paths[0];

            // Load and display the image
            StartCoroutine(LoadImage(path));
        }
    }

    public void ResetPhoto()
    {
        if (this.image != null && this.hallPeopleImage != null && this.resultPeopleImage != null)
        {
            this.image.texture = this.originalImage;
            AspectRatioFitter ratioFitter = this.image.GetComponent<AspectRatioFitter>();
            ratioFitter.aspectRatio = 1f;
            AspectRatioFitter hallPeopleFitter = this.hallPeopleImage.GetComponent<AspectRatioFitter>();
            this.hallPeopleImage.texture = this.originalHallPeopleImage;
            hallPeopleFitter.aspectRatio = 1f;
            AspectRatioFitter resultPeopleFitter = this.resultPeopleImage.GetComponent<AspectRatioFitter>();
            this.resultPeopleImage.texture = this.originalResultPeopleImage;
            resultPeopleFitter.aspectRatio = 1f;

            if (this.resetBtn != null)
            {
                SetUI.Set(this.resetBtn, false, 0.75f, 0f);
            }
        }
    }

    private IEnumerator LoadImage(string path)
    {
        using (UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(path))
        {
          //  imageRequest.certificateHandler = new WebReqSkipCert();

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

                // Apply the texture to the image renderer
                if (this.image != null && originalTexture != null && this.hallPeopleImage != null && this.resultPeopleImage != null)
                {
                    Debug.Log(originalTexture.width + ";" + originalTexture.height);
                    float ratio = (float)originalTexture.width / (float)originalTexture.height;
                    Debug.Log("ratio: " + ratio);
                    AspectRatioFitter ratioFitter = this.image.GetComponent<AspectRatioFitter>();
                    ratioFitter.aspectRatio = ratio;
                    this.image.texture = originalTexture;

                    AspectRatioFitter hallPeopleFitter = this.hallPeopleImage.GetComponent<AspectRatioFitter>();
                    hallPeopleFitter.aspectRatio = ratio;
                    this.hallPeopleImage.texture = originalTexture;

                    AspectRatioFitter resultPeopleFitter = this.resultPeopleImage.GetComponent<AspectRatioFitter>();
                    resultPeopleFitter.aspectRatio = ratio;
                    this.resultPeopleImage.texture = originalTexture;

                    if (this.resetBtn != null)
                    {
                        SetUI.Set(this.resetBtn, true, 1f, 0f);
                    }
                }
            }
        }
    }
}


public class WebReqSkipCert: CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}