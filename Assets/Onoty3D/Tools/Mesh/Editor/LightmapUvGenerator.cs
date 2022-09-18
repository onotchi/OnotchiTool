using System.IO;
using UnityEditor;
using UnityEngine;

namespace Onoty3D.Tools.Mesh
{
    public class LightmapUvGenerator
    {
        const string DIRECTORY_PATH = "Assets/Onoty3D/Output/LightmapUvGenerator/GeneratedMeshes/";
        const string ASSET_PATH = DIRECTORY_PATH + "{0}_WithLightMapUV.asset";
        const string ASSET_PATH2 = DIRECTORY_PATH + "{0}_WithLightMapUV ({1}).asset";

		[MenuItem("Onoty3D/Tools/Mesh/GenerateLightMapUVs")]
		private static void Generate()
		{
			//何も選択されていなかったら警告
			if (Selection.gameObjects.Length == 0)
			{
				EditorUtility.DisplayDialog(
				"Error",
				"Select the object containing SkinnedMeshRender or Mesh Filter.",
				"Ok");
				return;
			}

			//フォルダの作成
			var path = Application.dataPath + "/../" + DIRECTORY_PATH;
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			//プログレスバー出して出力
			var title = "GenerateLightMapUVs";
			var info = "Generating {0}/{1} ...";
			try
			{
				for (int i = 0; i < Selection.gameObjects.Length; i++)
				{
					EditorUtility.DisplayProgressBar(title, string.Format(info, i + 1, Selection.gameObjects.Length), (float)(i + 1) / (float)Selection.gameObjects.Length);
					var skinnedMeshRenderer = Selection.gameObjects[i].GetComponentInChildren<SkinnedMeshRenderer>();
					if (skinnedMeshRenderer != null)
					{
						var mesh = GenerateMesh(Selection.gameObjects[i].name, skinnedMeshRenderer.sharedMesh);
						skinnedMeshRenderer.sharedMesh = mesh;
						continue;
					}

					var meshFilter = Selection.gameObjects[i].GetComponentInChildren<MeshFilter>();
					if (meshFilter != null)
					{
						var mesh = GenerateMesh(Selection.gameObjects[i].name, meshFilter.sharedMesh);
						meshFilter.sharedMesh = mesh;
						continue;
					}
				}
			}
			finally
			{
				EditorUtility.ClearProgressBar();
			}
		}

		private static UnityEngine.Mesh GenerateMesh(string name, UnityEngine.Mesh source)
		{

			//メッシュのクローンを生成
			var mesh = GameObject.Instantiate(source);

			//LightMap用UV(UV2)生成
			Unwrapping.GenerateSecondaryUVSet(mesh);

			//出力
			var path = string.Format(ASSET_PATH, name);
			if (File.Exists(path))
			{
				var index = 1;
				while (true)
				{
					path = string.Format(ASSET_PATH2, name, index);
					if (File.Exists(path))
					{
						index++;
						continue;
					}
					break;
				}
			}

			AssetDatabase.CreateAsset(mesh, path);

			return mesh;
		}

	}
}
