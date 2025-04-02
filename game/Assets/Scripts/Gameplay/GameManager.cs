using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int score;
        public int level;
    }

    private PlayerData _currentData; // Stocăm datele primite
    private bool _hasData; // Verificăm dacă avem date

    private void Start()
    {
        // Startăm invocarea repetată a funcției LogData la fiecare 5 secunde
        InvokeRepeating(nameof(LogData), 0, 5f);
    }

    public void ReceiveData(string jsonData)
    {
        _currentData = JsonUtility.FromJson<PlayerData>(jsonData);
        _hasData = true; // Marcăm că avem date disponibile
    }

    private void LogData()
    {
        if (_hasData && _currentData != null)
        {
            Debug.Log($"Date afișate la interval: Nume: {_currentData.name}, Scor: {_currentData.score}, Nivel: {_currentData.level}");
        }
        else
        {
            Debug.Log("Nu s-au primit date încă.");
        }
    }
}