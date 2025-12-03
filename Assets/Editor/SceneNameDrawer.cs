using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 1. 安全检查：确保这个标签只用在 string 类型上
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.LabelField(position, label.text, "ERROR: [SceneName] only works on strings!");
            return;
        }

        // 2. 获取 Build Settings 中的场景列表
        var scenes = EditorBuildSettings.scenes;
        List<string> sceneOptions = new List<string>();

        // 添加一个“无 / 未设置”的选项，防止强制选择
        sceneOptions.Add("- Not Set -");

        foreach (var scene in scenes)
        {
            // 只显示已勾选（Enabled）的场景
            if (scene.enabled)
            {
                string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                sceneOptions.Add(sceneName);
            }
        }

        // 3. 如果 Build Settings 里没有场景，提示用户
        if (sceneOptions.Count == 1)
        {
            EditorGUI.LabelField(position, label.text, "No scenes in Build Settings!");
            return;
        }

        // 4. 匹配当前存储的值
        int selectedIndex = 0;
        string currentStringValue = property.stringValue;

        // 遍历列表寻找当前值对应的索引
        for (int i = 1; i < sceneOptions.Count; i++)
        {
            if (sceneOptions[i] == currentStringValue)
            {
                selectedIndex = i;
                break;
            }
        }

        // 5. 绘制下拉菜单
        // 检查当前值是否存在于列表中（防止场景改名后，Inspector 显示错误）
        if (!string.IsNullOrEmpty(currentStringValue) && selectedIndex == 0)
        {
            // 如果存了一个值，但在列表里找不到（比如场景被删了），我们临时显示它
            sceneOptions.Add($"Missing: {currentStringValue}");
            selectedIndex = sceneOptions.Count - 1;
        }

        int newIndex = EditorGUI.Popup(position, label.text, selectedIndex, sceneOptions.ToArray());

        // 6. 保存选择
        if (newIndex == 0)
        {
            property.stringValue = ""; // 选了 "- Not Set -"
        }
        else
        {
            // 也就是选中了某个具体的场景名
            // 如果选的是那个 "Missing" 的临时选项，我们不更新值，保持原样
            if (!sceneOptions[newIndex].StartsWith("Missing: "))
            {
                property.stringValue = sceneOptions[newIndex];
            }
        }
    }
}