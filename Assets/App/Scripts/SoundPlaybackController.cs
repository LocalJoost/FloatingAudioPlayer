using System.Collections;
using HoloToolkit.Examples.InteractiveElements;
using UnityEngine;
using UnityEngine.Networking;

public class SoundPlaybackController : BaseMediaLoader
{
    public AudioSource Audio;

    public GameObject Slider;

    public GameObject Button;

    private SliderGestureControl _sliderControl;

    private IconToggler _iconToggler;

    // Use this for initialization
    void Start()
    {
        _sliderControl = Slider.GetComponent<SliderGestureControl>();
        _sliderControl.OnUpdateEvent.AddListener(ValueUpdated);
        Slider.SetActive(false);
        Button.SetActive(false);
        _iconToggler = Button.GetComponent<IconToggler>();
    }

    private IEnumerator LoadMediaFromUrl(string url)
    {
        var handler = new DownloadHandlerAudioClip(url, AudioType.OGGVORBIS);
        yield return ExecuteRequest(url, handler);
        if (handler.audioClip.length > 0)
        {
            Audio.clip = handler.audioClip;
            _sliderControl.SetSpan(0, Audio.clip.length);
            Slider.SetActive(true);
            Button.SetActive(true);
            _iconToggler.SetBaseState();
        }
    }

    private float _lastClick;
    public void TogglePlay()
    {
        if (Time.time - _lastClick > 0.1)
        {
            _lastClick = Time.time;
            if (Audio.isPlaying)
            {
                Audio.Pause();
            }
            else
            {
                Audio.Play();
            }
        }
    }

    private void ValueUpdated()
    {
        Audio.time = _sliderControl.SliderValue;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Audio.isPlaying)
        {
            _sliderControl.SetSliderValue(Audio.time);
        }
        if (Mathf.Abs(Audio.time - _sliderControl.MaxSliderValue) < 0.1f)
        {
            Audio.Stop();
            Audio.time = 0;
            _iconToggler.SetBaseState();
            _sliderControl.SetSliderValue(0);
        }
    }

    protected override IEnumerator StartLoadMedia()
    {
        Slider.SetActive(false);
        Button.SetActive(false);
        yield return LoadMediaFromUrl(MediaUrl);
    }
}
