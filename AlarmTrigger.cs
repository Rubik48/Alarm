using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Thief thief))
        {
            _alarmSystem.TurnOn();
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Thief thief))
        {
            _alarmSystem.TurnOff();
        }
    }
}
