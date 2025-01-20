using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private float _maxVolume = 1.0f;
    [SerializeField] private float _volumeChangeSpeed = 0.5f;
    
    private AudioSource _audioSource;
    
    private float _targetVolume;
    private float _minVolume = 0f;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.volume = _minVolume;
        _audioSource.loop = true;
    }

    private void Update()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);
        
        if(_audioSource.volume <= 0 && _audioSource.isPlaying == true)
            _audioSource.Stop();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Thief thief))
        {
            _targetVolume = _maxVolume;
            
            if(_audioSource.isPlaying == false)
                _audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Thief thief))
        {
            _targetVolume = _minVolume;
        }
    }
}