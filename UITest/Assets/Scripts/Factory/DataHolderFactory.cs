using UnityEngine;

public class DataHolderFactory : Factory
{
    [SerializeField] private DataHolder _dataHolder;

    public override IProduct GetProduct(Transform parent)
    {
        IProduct product = Instantiate(_dataHolder, Vector2.zero, Quaternion.identity, parent);
        product.Initialize();
        return product;
    }
}
