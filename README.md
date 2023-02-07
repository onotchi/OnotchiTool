# OnotchiTool
Unity向けのスクリプト群です。(大体VRChat絡みだったりするので、Unityバージョンは2019.4.31f1で確認しています。)

## LightmapUVGenerator
https://github.com/onotchi/OnotchiTool/releases/download/LightmapUVGenerator/LightmapUVGenerator.unitypackage
*Assets/Onoty3D/Tools/Mesh/Editor/LightmapUVGenerator.cs*  
FBXのインスペクタなどにある「Generating Lightmap UVs」が利用できない、メッシュ単体のアセットなどに対し  
ライトマップ用のUV2が生成できるスクリプトです。  

公式のUnwrapping.GenerateSecondaryUVSetメソッドを使ってるんで、"Generating Lightmap UVs"を使ったときと同じアルゴリズムでUV2が生成されるんじゃないかな(多分)。  
  
## CsvExporterForSceneObjects
https://github.com/onotchi/OnotchiTool/releases/download/CsvExporterForSceneObjects/CsvExporterForSceneObjects.unitypackage
*Assets/Onoty3D/Tools/Debug/Editor/CsvExporterForSceneObjects.cs*  
シーン内にあるオブジェクトの一覧をHierarchyの順番でCSV形式で出力します。

