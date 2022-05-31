using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;

public class QRCodeScaner : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawImageBackground;
    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializeField]
    private TextMeshProUGUI _textOut;
    [SerializeField]
    private RectTransform _scanZone;

    private bool _isCamAvaliable;
    private WebCamTexture _webCamTexture;

    private void Start()
    {
        SetUpCamera();
    }

    private void Update()
    {
        UpdateCameraRender();
    }

    private void SetUpCamera()
    {
        var devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            _isCamAvaliable = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _webCamTexture = new WebCamTexture(devices[i].name,
                    (int)_scanZone.rect.width, (int)_scanZone.rect.height);
            }
        }
        _webCamTexture.Play();
        _rawImageBackground.texture = _webCamTexture;
        _isCamAvaliable = true;
    }

    private void UpdateCameraRender()
    {
        if (_isCamAvaliable == false)
            return;
        var ratio = _webCamTexture.width / (float)_webCamTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;
        int orientation = -_webCamTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void OnClickScan()
    {
        try
        {
            IBarcodeReader reader = new BarcodeReader();
            Result result = reader.Decode(_webCamTexture.GetPixels32(),
                _webCamTexture.width, _webCamTexture.height);
            _textOut.text = result == null ? "Failed to read" : result.Text;
        }
        catch
        {
            _textOut.text = "Failed";
        }
    }
}
