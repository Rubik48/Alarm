using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _maxVolume = 1.0f;
    [SerializeField] private float _volumeChangeSpeed = 0.5f;
   
    private AudioSource _audioSource;
    private Coroutine _changeVolumeRoutine;
   
    private float _minVolume = 0f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = _minVolume;
        _audioSource.loop = true;
    }

    public void TurnOn()
    {
        if(_audioSource.isPlaying == false)
            _audioSource.Play();
           
        if(_changeVolumeRoutine != null)
            StopCoroutine(_changeVolumeRoutine);
           
        _changeVolumeRoutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void TurnOff()
    {
            if(_changeVolumeRoutine != null)
                StopCoroutine(_changeVolumeRoutine);
           
            _changeVolumeRoutine = StartCoroutine(ChangeVolume(_minVolume));
    }
   
    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (Mathf.Approximately(_audioSource.volume, targetVolume) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeChangeSpeed * Time.deltaTime);

            yield return null;
        }
       
        if (_audioSource.volume <= _minVolume && _audioSource.isPlaying == true)
        {
            _audioSource.Stop();
        }
    }
}