using UnityEngine;

public class DineroGameObject : MonoBehaviour
{
    public static DineroGameObject Instance { get; private set; }
    private Dinero data;

    // Notifica cambios a la UI u otros sistemas
    public event System.Action<int> OnMoneyChanged;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        data = Resources.Load<Dinero>("Data/MoneyData");
        if (data == null)
        {
            Debug.LogError("Falta Resources/Data/MoneyData.asset. Crea el asset ahí.");
            return;
        }

        // Valor inicial
        OnMoneyChanged?.Invoke(data.dinero);
    }

    public int Get() => data != null ? data.dinero : 0;

    public void Set(int valor)
    {
        if (data == null) return;
        data.Set(valor);
        OnMoneyChanged?.Invoke(data.dinero);
    }

    public void Add(int cantidad)
    {
        if (data == null) return;
        data.Add(cantidad);
        OnMoneyChanged?.Invoke(data.dinero);
    }

    public void Subtract(int cantidad)
    {
        if (data == null) return;
        data.Subtract(cantidad);
        OnMoneyChanged?.Invoke(data.dinero);
    }

    // Compra segura (evita saldo negativo)
    public bool TryPurchase(int costo)
    {
        if (data == null) return false;
        if (data.dinero >= costo)
        {
            data.Subtract(costo);
            OnMoneyChanged?.Invoke(data.dinero);
            return true;
        }
        return false;
    }
}

