# 出力設定

## 出力フォーマット

### 図面ファイル用

| フォーマット | 説明 |
|--------------|------|
| PDF | Portable Document Format。印刷・共有向け |
| DXF | Drawing Exchange Format。他CADソフトとの互換性 |

### パーツ/アセンブリファイル用

| フォーマット | 説明 |
|--------------|------|
| IGES | Initial Graphics Exchange Specification。汎用3D形式 |
| STEP | Standard for Exchange of Product Data。標準3D形式 |
| 3MF | 3D Manufacturing Format。3Dプリント向け |

## 出力先オプション

### 元ファイルと同じ場所

CADファイルと同じディレクトリにエクスポートファイルを作成します。

```
例:
C:\CAD\部品A.SLDPRT → C:\CAD\部品A.step
```

### サブフォルダ

CADファイルのフォルダ内にあるサブフォルダを指定できます。

```
例:
C:\CAD\部品A.SLDPRT → C:\CAD\Export\部品A.step
```

### カスタムフォルダ

任意のフォルダを出力先として指定できます。

## ZIP圧縮

エクスポートしたファイルをZIPアーカイブにまとめることができます。

### ZIP出力オプション

- **ZIPを作成しない**: 個別ファイルのみ出力
- **元ファイルと同じ場所にZIP作成**
- **サブフォルダにZIP作成**
- **カスタムフォルダにZIP作成**

## テーマ

アプリはOSのテーマ設定に自動的に追従します。

- Windows設定でダークモードを有効にするとアプリもダークモードに
- ライトモードにするとアプリもライトモードに
