# CADExportTool4 - Project Guide

## Project Overview

CADExportTool4はSolidWorks APIを使用してCADファイルを様々なフォーマットにエクスポートするためのWindows Formsアプリケーションです。

## Technology Stack

- **Framework**: .NET 8.0 (Windows)
- **UI**: Windows Forms
- **Language**: C#
- **CAD Integration**: SolidWorks API (Interop)
- **IDE**: Visual Studio

## Project Structure

```
CADExportTool4/
├── SMZ_CADExportTool/      # Main Windows Forms application
├── SolidworksAPIAPI/       # SolidWorks API wrapper library
│   └── Converter/          # File conversion logic
├── TestProject1/           # Test project
├── TestProject2/           # Test project
├── UnitTestProject1-3/     # Unit test projects
├── specs/                  # Feature specifications (spec-kit)
├── memory/                 # Project constitution and memory
└── .claude/commands/       # Claude Code slash commands
```

## Key Files

- `SMZ_CADExportTool/CADExportTool.cs` - Main form logic
- `SMZ_CADExportTool/ExportFile.cs` - Export file handling
- `SolidworksAPIAPI/Converter/` - Conversion implementations

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

### Testing

- 単体テストは各UnitTestProjectで実行
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
