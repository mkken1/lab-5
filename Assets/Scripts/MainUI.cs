using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{ 
    [SerializeField] private TMP_InputField _searchName;
    [SerializeField] private TMP_InputField _searchTag;
    private ObjectsActions _objectsActions;
    private bool _isSpinning = false;

    void Start()
    {
        _objectsActions = gameObject.AddComponent<ObjectsActions>();
    }

    void Update()
    {
        if (_isSpinning)
            _objectsActions.SpinObjects();

    }

    public void SpawnOnClick() => _objectsActions.SpawnObjects();

    public void SpinOnClick() => _isSpinning = !_isSpinning;
    
// MainUI.cs
    public void SearchOnClick()
    {
        string nameQuery = _searchName.text.Trim();
        string tagQuery = _searchTag.text.Trim();

        List<GameObject> allObjects = _objectsActions.GetGameObjects();

        foreach (var obj in allObjects)
        {
            Debug.Log("ВЫЗОВ");

            if (obj == null) 
            {
                Debug.LogWarning("[Search] Пропущен NULL-объект!");
                continue;
            }

            // Очищаем имя от "(Clone)" и пробелов
            string cleanName = obj.name.Replace("(Clone)", "").Trim();
            
            // Проверяем имя: регистронезависимый поиск подстроки
            bool matchesName = string.IsNullOrEmpty(nameQuery) || 
                            cleanName.IndexOf(nameQuery, System.StringComparison.OrdinalIgnoreCase) >= 0;
            
            // Проверяем тег (только если поле не пустое)
            bool matchesTag = string.IsNullOrEmpty(tagQuery) || 
                            (UnityTagExists(tagQuery) && obj.CompareTag(tagQuery));

            obj.SetActive(matchesName && matchesTag);
            Debug.Log($"Проверка объекта: '{obj.name}', чистое имя: '{cleanName}', тег: '{obj.tag}'");
            Debug.Log($"Запрос: имя='{nameQuery}', тег='{tagQuery}' → Результат: {matchesName && matchesTag}");
        }
    }

    // Вспомогательный метод для проверки существования тега
    private bool UnityTagExists(string tag)
    {
        // Для редактора:
        #if UNITY_EDITOR
        return UnityEditorInternal.InternalEditorUtility.tags.Contains(tag);
        #else
        // Для билда: проверяем через любой объект с таким тегом
        return GameObject.FindGameObjectsWithTag(tag).Length > 0;
        #endif
    }
}
