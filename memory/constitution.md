# CADExportTool4 Constitution

> プロジェクトの基本原則と開発ガイドライン

## Version

- **Version**: 1.0.0
- **Last Updated**: 2025-12-17
- **Status**: Active

## Project Identity

**Name**: CADExportTool4
**Purpose**: SolidWorks CADファイルを様々なフォーマットにエクスポートするためのデスクトップアプリケーション

## Core Principles

### 1. シンプルさを優先する

- 複雑な抽象化より、読みやすく直接的なコードを優先する
- 必要になるまで機能を追加しない（YAGNI）
- コードの行数より、理解のしやすさを重視する

### 2. 安定性と信頼性

- SolidWorks APIの呼び出しは適切なエラーハンドリングを行う
- ファイル操作は常に例外処理を含める
- ユーザーのCADファイルを破壊しない

### 3. ユーザー体験

- エクスポート処理の進捗を可視化する
- エラー時は具体的で理解しやすいメッセージを表示する
- キャンセル操作を常にサポートする

### 4. 保守性

- 一貫した命名規則を使用する（C# 命名規則に準拠）
- 各コンバーターは単一責任を持つ
- テスト可能な設計を維持する

## Technical Standards

### Code Style

- **言語**: C# 12.0
- **フレームワーク**: .NET 8.0
- **Null安全性**: 有効
- **命名規則**:
  - クラス/メソッド: PascalCase
  - ローカル変数/パラメータ: camelCase
  - プライベートフィールド: _camelCase

### Architecture

- **UI層**: SMZ_CADExportTool（Windows Forms）
- **API層**: SolidworksAPIAPI（ビジネスロジック）
- **依存関係**: UI → API → SolidWorks Interop

### Error Handling

```csharp
// 推奨パターン
try
{
    // SolidWorks API 呼び出し
}
catch (COMException ex)
{
    // ユーザーフレンドリーなエラーメッセージ
    Logger.Error($"SolidWorks操作に失敗しました: {ex.Message}");
    throw new ExportException("ファイルのエクスポートに失敗しました", ex);
}
```

### Testing Standards

- 新機能には単体テストを含める
- SolidWorks依存のテストは統合テストとして分離する
- テストメソッド名は `[対象]_[条件]_[期待結果]` 形式

## Development Workflow

### Spec-Driven Development

1. **Specify**: 機能要件を `specs/` に文書化
2. **Plan**: 実装計画を作成
3. **Tasks**: タスクに分解
4. **Implement**: コードを実装
5. **Review**: 仕様との整合性を確認

### Git Workflow

- **main/master**: 安定版
- **feature/***: 新機能開発
- **fix/***: バグ修正

### Commit Message Format

```
<type>: <description>

[optional body]
```

Types: feat, fix, docs, refactor, test, chore

## Quality Gates

- [ ] コードがビルドできる
- [ ] 既存のテストがパスする
- [ ] 新機能にはテストが含まれる
- [ ] 仕様書との整合性がある
- [ ] エラーハンドリングが適切

## Prohibited Patterns

- SolidWorks APIの直接呼び出しをUI層から行わない
- ハードコードされたファイルパスを使用しない
- 同期的なUI操作でUIをブロックしない
- 例外を握りつぶさない

## Dependencies Policy

- 必要最小限の外部パッケージのみ使用
- SolidWorks Interopは指定されたバージョンを使用
- NuGetパッケージは定期的に更新を検討

---

*この文書はプロジェクトの指針であり、すべての開発者が遵守すべき原則を定めています。*
