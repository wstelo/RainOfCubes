using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minPositionX;
    [SerializeField] private int _maxPositionX;
    [SerializeField] private int _minPositionZ;
    [SerializeField] private int _maxPositionZ;
    [SerializeField] private int _positionY;
    [SerializeField] private int _poolCapacity = 5;

    private ObjectPool<Cube> _pool;
    private Vector3 _position = new Vector3 (1,20,3);
    private int _poolMaxSize = 10;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => CreateObject(),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCubes), 0.0f, 2f);
    }

    private void GetCubes()
    {
        _pool.Get();
    }

    private Cube CreateObject()
    {
        Cube cube = Instantiate(_cubePrefab, _position, Quaternion.identity);
        cube.EndedTimeToDestroy += ActionOnRelease;

        return cube; 
    }

    private void ActionOnGet(Cube cube)
    {
        Vector3 position = new Vector3(Random.Range(_minPositionX, _maxPositionX), _positionY, Random.Range(_minPositionZ, _maxPositionZ));
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;
        cube.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.RefreshParameters();
        _pool.Release(cube);
    }
}
