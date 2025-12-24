# CADExportTool4

SolidWorks CADファイルを様々なフォーマットにバッチエクスポートするWindows デスクトップアプリケーション

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows)
![License](https://img.shields.io/badge/License-MIT-green)
[![Release](https://img.shields.io/github/v/release/GS-Bacon/CADExportTool4)](https://github.com/GS-Bacon/CADExportTool4/releases/latest)

## 機能

- **図面ファイル (.SLDDRW)** → PDF, DXF
- **パーツ/アセンブリファイル (.SLDPRT, .SLDASM)** → IGES, STEP, 3MF
- ドラッグ＆ドロップでファイル追加
- バッチ処理（複数ファイルの一括変換）
- カスタム出力フォルダ・サブフォルダ指定
- ZIP圧縮オプション
- OSテーマ連動ダークモード
- 進捗表示とトースト通知

## 動作要件

- Windows 10/11
- .NET 8.0 Runtime
- SolidWorks 2020以降（インストール済み）

## インストール

1. [Releases](https://github.com/GS-Bacon/CADExportTool4/releases/latest)から最新版をダウンロード
2. ZIPを解凍
3. `CADExportTool.WPF.exe`を実行

## 使い方

詳しい使い方は[ドキュメント](docs/wiki/Home.md)を参照してください。

### クイックスタート

1. アプリを起動
2. CADファイルをドラッグ＆ドロップまたは「ファイル追加」ボタンで追加
3. 出力フォーマット（PDF, DXF, IGES, STEP, 3MF）を選択
4. 出力先フォルダを設定
5. 「エクスポート」ボタンをクリック

## プロジェクト構成

```text
CADExportTool4/
├── CADExportTool.WPF/       # WPFアプリケーション (メイン)
├── CADExportTool.Core/      # 共有モデル・インターフェース
├── CADExportTool.Services/  # サービス実装層
└── SolidworksAPIAPI/        # SolidWorks API ラッパー
```

## 開発

### ビルド

```bash
dotnet build CADExportTool.WPF/CADExportTool.WPF.csproj -c Release
```

### テスト

```bash
dotnet test TestProject1/TestProject1.csproj
```

## ライセンス

MIT License

## 更新履歴

変更履歴は[Changelog](.specify/changelog.md)を参照してください。
