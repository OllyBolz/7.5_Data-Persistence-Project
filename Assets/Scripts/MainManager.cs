using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class HiScore
{ 
    public string playerName;
    public int hiScore;
}

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text hiScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;

    private string hiScorePlayerName = "player137";
    private int hiScore = 10;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        LoadScore();
		hiScoreText.text = $"Best Score - {hiScorePlayerName}: {hiScore}";
	}

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        if (m_Points > hiScore)
        { 
            SaveScore();
        }
    }

	public void SaveScore()
	{
		HiScore data = new HiScore();
		data.playerName = NameManager.playerName;
        data.hiScore = m_Points;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/hiScore.json", json);
	}

	public void LoadScore()
	{
		string path = Application.persistentDataPath + "/hiScore.json";
		Debug.Log(path);
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			HiScore data = JsonUtility.FromJson<HiScore>(json);

			hiScorePlayerName = data.playerName;
			hiScore = data.hiScore;
		}
	}
}
