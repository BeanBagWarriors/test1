using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIManager : MonoBehaviour
{
    private string apiUrl = "http://localhost:4000/unity-data";
    private string getDataUrl = "http://localhost:4000/get-data"; // Endpoint-ul nou
    private float interval = 3f;

    void Start()
    {
        StartCoroutine(SendData());
        StartCoroutine(FetchData());
    }

    [System.Serializable]
    public class UnityData{
        public string playerName;
        public int score;
        public string timestamp;
    }

    IEnumerator SendData()
    {
        while (true)
        {
            // Exemplu de date
            var payload = new UnityData{
                playerName = "Player1",
                score = 100,
                timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            string json = JsonUtility.ToJson(payload);

            UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
            byte[] body = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(body);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Eroare: {request.error}");
            }
            else
            {
                Debug.Log($"Răspuns: {request.downloadHandler.text}");
            }

            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator FetchData()
    {
        while (true)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(getDataUrl))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Eroare GET: {request.error}");
                }
                else
                {
                    // Deserializare răspuns JSON
                    string jsonResponse = request.downloadHandler.text;
                    Debug.Log($"Răspuns GET: {jsonResponse}");
                }
            }
            yield return new WaitForSeconds(interval);
        }
    }
}