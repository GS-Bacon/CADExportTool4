# テストデータディレクトリ

## 概要

このディレクトリは実際のSolidWorksファイルを使った統合テスト用です。

## ディレクトリ構成

```
testdata/
├── input/          # テスト用CADファイルを配置
│   ├── *.SLDDRW    # 図面ファイル
│   ├── *.SLDPRT    # パーツファイル
│   └── *.SLDASM    # アセンブリファイル
└── output/         # エクスポート結果が出力される
    ├── PDF/
    ├── DXF/
    ├── IGS/
    ├── STEP/
    ├── 3MF/
    └── Thumbnail/
```

## 使い方

### 1. テストファイルの配置

`input/` ディレクトリに以下のファイルを配置:

- `sample.SLDDRW` - 図面ファイル（PDF/DXF変換テスト用）
- `sample.SLDPRT` - パーツファイル（IGS/STEP/3MF変換テスト用）
- `sample.SLDASM` - アセンブリファイル（IGS/STEP/3MF変換テスト用）

### 2. 統合テストの実行

```powershell
# Visual Studioから
# Test Explorer → IntegrationTests を右クリック → Run

# コマンドラインから
dotnet test --filter "Category=Integration"
```

### 3. 結果の確認

`output/` ディレクトリに変換されたファイルが出力されます。

## 注意事項

- **SolidWorksのインストールが必要**: 統合テストにはSolidWorksがインストールされている必要があります
- **単体テストは不要**: `CADExportTool.Tests` の単体テストはモックを使用するためSolidWorks不要
- **ファイルはgit管理外**: このディレクトリ内のCADファイルは `.gitignore` で除外されています

## サンプルファイルの入手

テスト用のSolidWorksファイルがない場合:

1. SolidWorksを起動
2. 新規パーツを作成（簡単な立方体など）
3. `testdata/input/sample.SLDPRT` として保存
4. 図面を作成して `testdata/input/sample.SLDDRW` として保存
