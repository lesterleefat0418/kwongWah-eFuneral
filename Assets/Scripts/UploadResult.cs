using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class UploadResult : CaptureManager
{
    private void Start()
    {
        init();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            captureImage();
        }
    }

}


public class CaptureManager : MonoBehaviour
{
    public string uploadUrl = "http://localhost/kongwahServer/uploadResult.php";
    public string uploadQRUrl = "http://localhost/kongwahServer/uploadResultQR.php";
    public string resultPath = "http://localhost/kongwahServer/uploads/results";
    public Camera captureCamera;
    public CanvasScaler targetScaler;
    public OutputFormat outputFormat = OutputFormat.jpg;
    public RenderTexture renderTexture;
    private long photoFormat;
    private Texture2D screenShot;
    public RawImage qrImage;
    public bool disableUploadPhoto = false;

    public void init()
    {
        this.screenShot = new Texture2D((int)targetScaler.referenceResolution.x, (int)targetScaler.referenceResolution.y, TextureFormat.RGB24, false, false);
        this.resetTargetTexture();
    }

    public void resetTargetTexture()
    {
        if (captureCamera != null && renderTexture != null)
            captureCamera.targetTexture = renderTexture;
    }

    public enum OutputFormat
    {
        jpg,
        png
    }


    private string DateFolderFormat
    {
        get
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
    }

    private Rect ImageFormat(Texture2D tex)
    {
        return new Rect(0.0f, 0.0f, tex.width, tex.height);
    }

    private Sprite Texture2DToSprite(Texture2D image)
    {
        if (image != null)
            return Sprite.Create(image, ImageFormat(image), new Vector2(0.5f, 0.5f));
        else
            return null;
    }

    public Sprite capturedSprite
    {
        get
        {
            return Texture2DToSprite(this.screenShot);
        }
    }


    public void captureImage()
    {
        if (captureCamera != null && targetScaler != null)
        {
            Debug.Log("Capture!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            photoFormat = (long)(DateTime.UtcNow - epochStart).TotalSeconds;

            Debug.Log("Screen.height: " + Screen.height);
            Vector2 targetResolution = targetScaler.referenceResolution;
            Rect rect = new Rect(Vector2.zero, new Vector2(targetResolution.x, targetResolution.y));
            if (captureCamera.targetTexture == null)
            {
                RenderTexture rt = new RenderTexture((int)targetResolution.x, (int)targetResolution.y, 24, RenderTextureFormat.ARGB32);
                captureCamera.targetTexture = rt;
            }
            captureCamera.Render();
            RenderTexture.active = captureCamera.targetTexture;
            screenShot.filterMode = FilterMode.Point;
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            captureCamera.targetTexture = null;
            RenderTexture.active = null;
            SendPhotoToWeb(screenShot);
        }
    }

    private byte[] CaptureOutPutBytes(Texture2D texture)
    {
        byte[] bytes = null;
        switch (outputFormat)
        {
            case OutputFormat.jpg:
                bytes = texture.EncodeToJPG();
                break;
            case OutputFormat.png:
                bytes = texture.EncodeToPNG();
                break;
        }
        return bytes;
    }

    private string CaptureOutPutFormat
    {
        get
        {
            string format = "";
            switch (outputFormat)
            {
                case OutputFormat.jpg:
                    format = ".jpg";
                    break;
                case OutputFormat.png:
                    format = ".png";
                    break;
            }
            return format;
        }
    }


    public Texture2D GenerateQRCode(string url)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = 256,
                Width = 256
            }
        };

        Color32[] pixels = writer.Write(url);
        Texture2D qrCodeTexture = new Texture2D(writer.Options.Width, writer.Options.Height, TextureFormat.RGBA32, false);
        qrCodeTexture.SetPixels32(pixels);
        qrCodeTexture.Apply();

        return qrCodeTexture;
    }

    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    /*private IEnumerator ControlSendPhotoToWeb(int type, Texture2D screenShot)
    {
        if (WebStatusChecker.isNetworkConnection)
        {
            SendPhotoToWeb(type, screenShot);
            yield return null;
        }
        else
        {
            Debug.Log("Failure and retry send again");
            yield return new WaitForSeconds(1f);
            StartCoroutine(ControlSendPhotoToWeb(type, screenShot));
        }
    }*/

    public void SendPhotoToWeb(Texture2D screenshot)
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        long currentTime = (long)(DateTime.UtcNow - epochStart).TotalSeconds;
        StartCoroutine(UploadCaptureCoroutine(screenshot, currentTime));
    }


    IEnumerator UploadCaptureCoroutine(Texture2D screenShot, long currentTime)
    {
        string fileName = currentTime + CaptureOutPutFormat;
        byte[] screenFileData = CaptureOutPutBytes(screenShot);

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", screenFileData, fileName, "image/jpeg");
        form.AddField("name", fileName);

        UnityWebRequest www = UnityWebRequest.Post(uploadUrl, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Failed to upload image: " + www.error);
            yield return new WaitForSeconds(1f);
            StartCoroutine(UploadCaptureCoroutine(screenShot, currentTime));
        }
        else
        {
            string response = www.downloadHandler.text;
            if (!string.IsNullOrEmpty(response))
            {
                Debug.Log("Image uploaded successfully. Filename: " + response);

                // Modify the response string to extract the URL part
                int startIndex = response.IndexOf("http://");
                int endIndex = response.LastIndexOf(".jpg") + 4;
                string url = response.Substring(startIndex, endIndex - startIndex);

                // Log the modified URL
                Debug.Log("Modified URL: " + url);
                Texture2D qrCodeTexture = GenerateQRCode(url);

                if(this.qrImage != null)
                {
                    this.qrImage.texture = qrCodeTexture;
                }
            }
            else
            {
                Debug.Log("Image upload failed. Server response is empty.");
            }
        }
    }


    public void ControlDeleteFolder(string Path)
    {
        DirectoryInfo dir = new DirectoryInfo(Path);
        DirectoryInfo[] folders = dir.GetDirectories();
        foreach (var folder in folders)
        {
            if (!DateFolderFormat.Equals(folder.Name))
            {
                Directory.Delete(dir + folder.Name, true);
            }
        }
    }
}
