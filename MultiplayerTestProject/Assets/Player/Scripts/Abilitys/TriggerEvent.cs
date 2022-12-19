using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider> _enter;
    [SerializeField] private UnityEvent<Collider> _stay;
    [SerializeField] private UnityEvent<Collider> _exit;

    private Collider _collider;

    public UnityEvent<Collider> Enter => _enter;
    public UnityEvent<Collider> Stay => _stay;
    public UnityEvent<Collider> Exit => _exit;
    public Collider Collider => _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _enter.Invoke(other);
    }
    private void  OnTriggerStay(Collider other)
    {
        _stay.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        _exit.Invoke(other);
    }
}