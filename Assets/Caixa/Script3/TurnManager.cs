using UnityEngine;
using System.Collections.Generic;

public class TurnManagerr : MonoBehaviour
{
    public List<UnitMovementWithMP> units; // Arrastra aquí tus unidades en el inspector
    private int currentUnitIndex = 0;

    void Start()
    {
        StartTurn();
    }

    void Update()
    {
        // Ejemplo: pulsar espacio para terminar turno
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    void StartTurn()
    {
        UnitMovementWithMP activeUnit = units[currentUnitIndex];
        activeUnit.NewTurn();
        Debug.Log("Turno iniciado para unidad: " + activeUnit.name);
    }

    void EndTurn()
    {
        Debug.Log("Turno terminado para unidad: " + units[currentUnitIndex].name);

        // Pasar a la siguiente unidad
        currentUnitIndex++;
        if (currentUnitIndex >= units.Count)
        {
            currentUnitIndex = 0; // Reiniciar ciclo
        }

        StartTurn();
    }
}

