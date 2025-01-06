using System;
using Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets
{
    public class AssetLoader 
    {
        public async UniTask<T> LoadAsync<T>(AssetName key) 
        {
            var opHandle = Addressables.LoadAssetAsync<GameObject>(key.Name);

            await opHandle.ToUniTask();

            if (opHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Не удалось загрузить ассет {key.Name}");
            }
            var obj = opHandle.Result.GetComponent<T>();

            if (obj == null)
            {
                throw new Exception($"Компонент типа {typeof(T)} не найден на загруженном GameObject {key.Name}");
            }

            return obj;
        }

        public T LoadSync<T>(AssetName key)
        {
            var opHandle = Addressables.LoadAssetAsync<GameObject>(key.Name);
            opHandle.WaitForCompletion();

            if (opHandle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"Не удалось загрузить ассет {key.Name}");
            }

            var obj = opHandle.Result.GetComponent<T>();

            if (obj == null)
            {
                throw new Exception($"Компонент типа {typeof(T)} не найден на загруженном GameObject {key.Name}");
            }

            return obj;
        }

        public T InstantiateSync<T>(T handle, Transform parent) where T : Component
        {
            T obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, parent);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }

        public T InstantiateSync<T>(T handle, Vector3 position, Quaternion rotation) where T : Component
        {
            T obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, position, rotation);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }

        public T InstantiateSync<T>(T handle, Vector3 position, Quaternion rotation, Transform parent) where T : Component
        {
            T obj = handle;
            var result = UnityEngine.Object.Instantiate(obj, position, rotation, parent);

            if (result.GetComponent<T>())
            {
                return result.GetComponent<T>();
            }
            else
            {
                throw new Exception($"{nameof(T)} не найден");
            }
        }
    }
}
