using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _maxVolume = 1.0f;
    [SerializeField] private float _volumeChangeSpeed = 0.5f;
    
    private AudioSource _audioSource;
    private Coroutine _changeVolumeRoutine;
    
    private float _targetVolume;
    private float _minVolume = 0f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = _minVolume;
        _audioSource.loop = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Thief thief))
        {
            _targetVolume = _maxVolume;
            
            if(_audioSource.isPlaying == false)
                _audioSource.Play();
            
            _changeVolumeRoutine = StartCoroutine(ChangeVolume());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Thief thief))
        {
            _targetVolume = _minVolume;
            
            _changeVolumeRoutine = StartCoroutine(ChangeVolume());
        }
    }
    
    private IEnumerator ChangeVolume()
    {
        while (!Mathf.Approximately(_audioSource.volume, _targetVolume))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);

            yield return null;
        }
        
        if (_audioSource.volume <= _minVolume && _audioSource.isPlaying == true)
        {
            _audioSource.Stop();
            
            StopCoroutine(_changeVolumeRoutine);
        }
        else if (_audioSource.volume >= _maxVolume)
        {
            StopCoroutine(_changeVolumeRoutine);
        }
    }
}