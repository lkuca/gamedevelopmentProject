using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenuManager : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI; // Панель меню
    public Button continueButton;
    public Button saveButton;
    public GameObject option_panel;
    public Button optionsButton;
    public Button exitButton;
    public GameObject player;
    public PlayerWeaponManager weaponManager;
    public SaveManager saveManager;

    //public Slider volumeSlider;
    //private bool isPaused = false;

    void Start()
    {
        // Изначально меню скрыто
        pauseMenuUI.SetActive(false);
        option_panel.SetActive(false);
        // Привязка кнопок
        continueButton.onClick.AddListener(ResumeGame);
        saveButton.onClick.AddListener(SaveGame);
        optionsButton.onClick.AddListener(OpenOptions);
        exitButton.onClick.AddListener(ExitGame);

        //volumeSlider.value = AudioListener.volume;

        //volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void Update()
    {
        // Ожидаем нажатия ESC для открытия/закрытия меню
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC pressed"); // Проверка!

            //if (IsGamePaused)
            //    ResumeGame();
            //else
            //    PauseGame();

            if (option_panel.activeSelf)
            {
                // Если сейчас открыта панель настроек — вернуться назад
                BackToPauseMenu();
            }
            else
            {
                if (IsGamePaused)
                    ResumeGame();
                else
                    PauseGame();
            }
        }
    }


    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Остановка времени
        IsGamePaused = true;
    }
    public void TogglePause()
    {
        IsGamePaused = !IsGamePaused;

        if (IsGamePaused)
        {
            Time.timeScale = 0f;
            pauseMenuUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenuUI.SetActive(false);
            option_panel.SetActive(false);
        }
    }


    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Возврат к норме
        IsGamePaused = false;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        Vector3 pos = player.transform.position;
        data.playerX = pos.x;
        data.playerY = pos.y;
        data.playerZ = pos.z;
        //data.weaponType = weaponManager.curWeaponType;

        saveManager.Save(data);
    }
    void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel");
            SceneManager.LoadScene(savedLevel); // Загружаем сохраненный уровень
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No save data found.");
        }
    }

    public void OpenOptions()
    {
        pauseMenuUI.SetActive(false);
        option_panel.SetActive(true);
        // Здесь можно добавить функциональность для изменения настроек (звука, яркости)
        Debug.Log("Open Options Menu");
    }

    void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit(); // Закрытие игры
    }
    void ChangeVolume(float value)
    {
        AudioListener.volume = value; // Меняем громкость
        Debug.Log("Volume set to: " + value);
    }

    public void BackToPauseMenu()
    {
        option_panel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}

[System.Serializable]
public class SaveData
{
    public string weaponType;
    public float playerX;
    public float playerY;
    public float playerZ;
}

//public class SaveManager : MonoBehaviour
//{
//    private string savePath;

//    void Awake()
//    {
//        savePath = Path.Combine(Application.persistentDataPath, "playerdata.json");
//    }

//    public void Save(SaveData data)
//    {
//        string json = JsonUtility.ToJson(data, true);
//        File.WriteAllText(savePath, json);
//        Debug.Log("Game saved to: " + savePath);
//    }

//    public SaveData Load()
//    {
//        if (File.Exists(savePath))
//        {
//            string json = File.ReadAllText(savePath);
//            return JsonUtility.FromJson<SaveData>(json);
//        }
//        else
//        {
//            Debug.LogWarning("No save file found!");
//            return null;
//        }
//    }
//}
