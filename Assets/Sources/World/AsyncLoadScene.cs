using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Client.World
{
    [RequireComponent(typeof(ModelViewConnection))]
    public sealed class AsyncLoadScene : MonoBehaviour
    {
        private ModelViewConnection _network;

        private void Awake()
        {
            _network = GetComponent<ModelViewConnection>();
        }

        public void LoadScene(int sceneIndex, Action<ModelViewConnection> action)
        {
            StartCoroutine(LoadSceneAsync(sceneIndex, action));
        }

        private IEnumerator LoadSceneAsync(int sceneIndex, Action<ModelViewConnection> action)
        {
            yield return null;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

            Debug.Log("Pro :" + asyncOperation.progress);
            action?.Invoke(_network);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}