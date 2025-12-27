# CADExportTool4 - Project Guide

## Project Overview

CADExportTool4はSolidWorks APIを使用してCADファイルを様々なフォーマットにエクスポートするためのWPFアプリケーションです。

## Technology Stack

- **Framework**: .NET 8.0 (Windows)
- **UI**: WPF + Prism + Material Design
- **MVVM**: CommunityToolkit.Mvvm
- **DI**: Prism.DryIoc
- **Language**: C#
- **CAD Integration**: SolidWorks API (Interop)

## Project Structure

```
CADExportTool4/
├── CADExportTool.WPF/      # WPFアプリ (スタートアップ)
│   ├── Views/              # XAML Views
│   ├── ViewModels/         # MVVM ViewModels
│   ├── Helpers/            # ThemeHelper等
│   └── Services/           # WPF固有サービス
├── CADExportTool.Core/     # 共有コア
│   ├── Enums/              # CadFileType, ExportFormat等
│   ├── Interfaces/         # サービスインターフェース
│   └── Models/             # ドメインモデル
├── CADExportTool.Services/ # サービス実装
│   ├── Converters/         # Drawing/Part/Assembly Converter
│   └── *.cs                # SolidWorks, Export, Update等
├── CADExportTool.Tests/    # 単体テスト
├── specs/                  # Feature specifications (spec-kit)
├── memory/                 # Project constitution and memory
└── .claude/commands/       # Claude Code slash commands
```

## Key Files

- `CADExportTool.WPF/ViewModels/MainViewModel.cs` - メインViewModel
- `CADExportTool.Services/ExportService.cs` - エクスポートオーケストレーション
- `CADExportTool.Services/SolidWorksService.cs` - SolidWorks接続管理
- `CADExportTool.Services/Converters/` - ファイル変換実装

## Development Guidelines

### Spec-Driven Development

このプロジェクトはspec-kit（仕様駆動開発）を使用しています。新機能の開発は以下のワークフローに従います:

1. `/speckit.specify` - 機能要件を定義
2. `/speckit.plan` - 実装計画を作成
3. `/speckit.tasks` - タスクリストを生成
4. `/speckit.implement` - 実装を実行

### Code Standards

- Null安全性を有効化 (`<Nullable>enable</Nullable>`)
- 暗黙的なusingを使用 (`<ImplicitUsings>enable</ImplicitUsings>`)
- 日本語のコメントを使用可

### Architecture Principles

- **層分離**: WPF → Services → Core
- **DI**: すべてのサービスはインターフェース経由で注入
- **非同期**: async/await + CancellationToken + IProgress<T>

### Testing

- 単体テストは `CADExportTool.Tests` で実行
- SolidWorksが必要なテストは手動で実行

## Commands

使用可能なスラッシュコマンド:

- `/speckit.specify <feature>` - 新機能の仕様を作成
- `/speckit.plan` - 実装計画を作成
- `/speckit.tasks` - タスクリストを生成
- `/speckit.implement` - 実装を開始

## References

- [SolidWorks API Documentation](https://help.solidworks.com/2024/english/api/SWHelp_Welcome.htm)
- [spec-kit Documentation](https://github.com/github/spec-kit)
- [Prism Library](https://prismlibrary.com/)
- [Material Design In XAML](https://materialdesigninxaml.net/)
