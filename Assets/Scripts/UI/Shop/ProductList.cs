using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProductList : MonoBehaviour
{
    [SerializeField] private List<Product> _products;

    public List<Product> Products => _products;
}
