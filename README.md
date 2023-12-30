# GeospatialRecList
GeospatialのSessionRecordingをリストで管理する機能  

# Geospatialとは
https://developers.google.com/ar/develop/geospatial?hl=ja  

# SessionRecordingとは
 - ARCoreのSessionを動画＋位置情報の形で保存する機能  
 - 後からそれを再生することで、デバッグが簡単になる  
 - 今回は、それらを複数リストで管理できる機能を実装した
 
# 動作イメージ
https://user-images.githubusercontent.com/41198895/233312359-dc4947b4-e989-4708-a473-e54f71987184.mp4

# 使い方
1. Project Settings > XR Pluginを有効にして、ARCoreのチェックボックスをOnにする
2. ARCore Extentions を Package Managerからimport  
  https://github.com/google-ar/arcore-unity-extensions.git  
3. Geospatial Sampleをimport  
  ![alt](Documents/Images/sample-scene.png)  
4. 下記のシーンを開く  
  `Assets/Samples/ARCore Extensions/1.35.0/Geospatial Sample/Scenes/Geospatial.unity`
5. 下記Prefabをシーンに追加する  
  `Assets/SessionPlayback/Prefabs/SessionPlayback.prefab`
6. SessionPlaybackのスクリプトにARSessionを追加
  ![alt](Documents/Images/add-ar-session.png)  
7. APIキーなどを、下記参考情報を参考に設定  

# 参考情報
 - https://developers.google.com/ar/develop/unity-arf/geospatial/enable-android?hl=ja
 - https://zenn.dev/tkada/articles/04b44474149130

# UPMでインポートする方法
- Window > Package Managerで左上の＋ボタンを選択。
- Add Package from git URL ...で下のリンクを入力し、Addボタンでインポートする。

#### https
```git+https://github.com/HoloLabInc/GeospatialRecList.git?path=GeospatialRecList/Assets/jp.co.hololab.geospatial_reclist```

or

#### ssh
```git@github.com:HoloLabInc/GeospatialRecList.git?path=GeospatialRecList/Assets/jp.co.hololab.geospatial_reclist```

# サポート端末
GeospatialRecListは現在、Androidのみ対応としています。  
iOSでもビルドは出来ますが、録画再生ともに動作しません。  
