# KeyboardSounder

ASIO再生を使って、キーボードを叩いたタイミングで低遅延で任意の音を鳴らすツールです。  
キー音なし音ゲーをプレイするときに、ゲーム側のSEをオフにしてこのツールを起動すれば、擬似的に低遅延な状態で音ゲーをプレイすることが出来ます。  

※使用には対応デバイスの所持が必要です。  
※PCの音量調整は機能しません。ツール内の音量調整を使用してください。  
※PCを経由せずに音を出力するため、OBSなどでキャプチャーすることは出来ません。  
※違うフォーマット(サンプルレート、ステレオモノラル)のwavが混ざっていると再生出来ない場合があります。  
　This means same sample rate, same bit depth and same number of channels (can't mix mono and stereo).  
※このツールを使ったいかなる損害にも責任は取れません  


## 使い方
1. ASIO対応デバイスの一覧から音声を出力するデバイスを選択する
2. 使いたいキーにチェックを入れる
3. 鳴らしたいSEを設定する
4. キーを押すとその音が鳴ることを確認する
5. 任意の音ゲーを起動する。その音ゲー内のSE音量は0にする。
6. BGMはゲーム側から、SEはこのツールから出力される状態でプレイが出来る。

※出力先デバイスの横のボタンを押すと、現在の設定を再ロードします  
※動かなくなった場合は、setting.iniを一旦削除すると動くかもしれません  

制作: @fuyuuutuly
