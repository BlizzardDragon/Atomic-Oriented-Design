using UnityEngine;
using DG.Tweening;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private float _scaleDuration;
    private Vector3 _lossyScale;


    private void Awake()
    {
        _lossyScale = transform.lossyScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(_lossyScale, _scaleDuration);
    }

    public void InvokeDestroyTrail()
    {
        transform.SetParent(null);
        transform.DOScale(Vector3.zero, _scaleDuration);
        Invoke(nameof(DestroyTrail), _scaleDuration);

    }

    private void DestroyTrail()
    {
        Destroy(gameObject);
    }
}
