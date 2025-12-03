using UnityEngine;

/// <summary>
/// 使用此特性可以将 string 字段在 Inspector 中变为场景选择下拉框。
/// 注意：只能显示已添加到 Build Settings 中的场景。
/// </summary>
public class SceneNameAttribute : PropertyAttribute
{
    // 可以在这里添加参数，例如是否允许空值等，目前保持最简即可
}