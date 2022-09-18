using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Onoty3D.Tools.Debug
{
	public class CsvExporterForSceneObjects
	{
		[MenuItem("Onoty3D/Tools/Debug/CsvExporterForSceneObjects")]
		private static void Export()
		{
			//アクティブなシーンの取得と検証
			var scene = EditorSceneManager.GetActiveScene();

			if (scene == null)
			{
				EditorUtility.DisplayDialog(
				"Error",
				"Open the target Scene",
				"Ok");
				return;
			}

			var path = EditorUtility.SaveFilePanel(
			"Save File",
			"Assets",
			$"{scene.name}_Objects_{EditorUserBuildSettings.activeBuildTarget}",
			"csv");

			var builder = new StringBuilder();
			builder.AppendLine("\"ObjectName\",\"Layer\",\"Tag\",\"Enabled\"");

			var gameObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject))
				.Where(x => AssetDatabase.GetAssetOrScenePath(x).Contains(".unity"))
				.Cast<GameObject>()
				.Select(x => new { GameObject = x, SortKey = GetSortKey(x.transform) })
				.OrderBy(x => x.SortKey)
				.Select(x => x.GameObject)
				.ToList();

			foreach (GameObject gameObject in gameObjects)
			{
				builder.Append($"\"{gameObject.name}\",");
				builder.Append($"\"{LayerMask.LayerToName(gameObject.layer)}\",");
				builder.Append($"\"{gameObject.tag}\",");
				builder.Append($"\"{(gameObject.activeSelf ? "True" : "False")}\"");
				builder.AppendLine();
			}

			File.WriteAllText(path, builder.ToString());
		}

		private static string GetSortKey(Transform transform)
		{
			if (transform.parent == null)
			{
				return $"{transform.GetSiblingIndex():_00000}";
			}
			else
			{
				return GetSortKey(transform.parent) + $"{transform.GetSiblingIndex():_00000}";
			}
		}
	}
}

