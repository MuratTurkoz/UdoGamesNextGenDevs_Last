using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameScene
{
    Market,
    Ocean
}

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set;}

    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Image _loadingBarImage;

    public Action OnPlayerEnteredOcean;
    public Action OnPlayerEnteredMarket;

    [SerializeField] private float _loadingTime = 3f;

    [SerializeField] private GameObject[] _scenes;

    public GameScene CurrentScene { get; private set; } = GameScene.Market;

    private void Awake() {
        Instance = this;
    }

    private bool isSceneLoading;

    public void LoadMarketScene()
    {
        ChangeScene(GameScene.Market);
    }

    public void LoadOceanScene()
    {
        ChangeScene(GameScene.Ocean);
    }

    public void ChangeScene(GameScene gameScene)
    {
        if (isSceneLoading) return;

        CurrentScene = gameScene;
        StartCoroutine(LoadScene((int)CurrentScene));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        isSceneLoading = true;
        _loadingBarImage.fillAmount = 0;
        _loadingPanel.SetActive(true);
        float timer = 0;

        for (int i = 0; i < _scenes.Length; i++)
        {
            _scenes[i].SetActive(i == sceneIndex);
        }

        while (timer < _loadingTime)
        {
            timer += Time.deltaTime;
            _loadingBarImage.fillAmount = timer / _loadingTime;
            yield return null;
        }

        switch (sceneIndex)
        {
            case 0:
                OnPlayerEnteredMarket?.Invoke();
                break;
            case 1:
                OnPlayerEnteredOcean?.Invoke();
                break;
        }

        _loadingPanel.SetActive(false);
        isSceneLoading = false;
    }


}
