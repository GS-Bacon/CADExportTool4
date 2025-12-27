# CADExportTool4 改善計画書

> 作成日: 2024-12-27
> ステータス: Active
> 基準バージョン: v1.1.0

## 背景

コードレビューにより以下の問題が特定された:

| 問題 | 重大度 | 影響 |
|------|--------|------|
| MainViewModel God Object | 高 | 保守性・テスタビリティ低下 |
| テストカバレッジ ~10% | 高 | 変更リスク増大 |
| レガシープロジェクト残存 | 中 | ビルド時間・複雑性増加 |
| ThumbnailService リソースリーク | 中 | メモリリーク可能性 |
| ZipService 未統合 | 低 | 機能不完全 |

## 改善方針

### フェーズ1: 技術的負債の解消（即座に実行）

**1.1 レガシープロジェクト削除**
```
削除対象:
├── SMZ_CADExportTool/      # Windows Forms UI (1700+ LOC)
├── SolidworksAPIAPI/       # 旧API wrapper
├── CADExportTool4/         # 空プロジェクト
├── UnitTestProject1/       # 空テスト
├── UnitTestProject2/       # 空テスト
└── UnitTestProject3/       # 空テスト
```

理由:
- 現在使用されていない
- ビルド時間を無駄にしている
- 新規参入者を混乱させる

**1.2 リソースリーク修正**

ThumbnailService.cs の Bitmap を `using` で囲む

---

### フェーズ2: アーキテクチャ改善

**2.1 MainViewModel 分割**

現在（God Object）:
```
MainViewModel (400 LOC)
├── ファイル管理
├── エクスポート実行
├── D&D処理
├── オプション管理
├── 進捗追跡
└── エラー報告
```

分割後（単一責任）:
```
ViewModels/
├── MainViewModel.cs        # 全体調整のみ (~100 LOC)
├── FileListViewModel.cs    # ファイル管理・D&D (~120 LOC)
├── ExportOptionsViewModel.cs # オプション管理 (~80 LOC)
└── ExportProgressViewModel.cs # 進捗・エラー (~100 LOC)
```

実装アプローチ:
- Prism の `CompositeCommand` でViewModel間連携
- `IEventAggregator` でイベント通信
- 段階的に抽出（一気にやらない）

---

### フェーズ3: テスト戦略

**3.1 テスト優先度**

| 対象 | 優先度 | 理由 |
|------|--------|------|
| ExportService | P0 | コアビジネスロジック |
| Converters | P1 | ファイル変換の正確性 |
| FileConverterFactory | P1 | 正しいConverter選択 |
| UpdateService | P2 | 既存テストあり、拡充 |
| ThumbnailService | P3 | 副次的機能 |

**3.2 テスト方針**

```
TestProject1/              # 単体テスト (モック使用)
├── Services/
│   ├── ExportServiceTests.cs
│   ├── FileConverterFactoryTests.cs
│   └── ZipServiceTests.cs
├── Converters/
│   ├── DrawingConverterTests.cs
│   ├── PartConverterTests.cs
│   └── AssemblyConverterTests.cs
└── ViewModels/
    └── (分割後に追加)

TestProject2/              # 統合テスト (SolidWorks必要)
└── Integration/
    └── SolidWorksExportTests.cs
```

**3.3 目標カバレッジ**
- Services層: 80%以上
- Converters: 70%以上
- ViewModels: 50%以上
- 全体: 60%以上

---

### フェーズ4: 機能完成

**4.1 ZipService 統合**

ExportService に ZIP 作成ロジックを統合:
```csharp
if (options.CreateZip)
{
    await _zipService.CreateZipAsync(outputFiles, options.ZipPath);
}
```

**4.2 サブフォルダ改善**

現在: 最初のファイルのディレクトリのみ検索
改善: 全ファイルの共通親ディレクトリを基準に検索

---

## 実行計画

### Week 1: クリーンアップ
- [x] レガシープロジェクト削除
- [x] リソースリーク修正
- [ ] ソリューション構成整理

### Week 2: テスト基盤
- [ ] ExportService テスト作成
- [ ] Converter テスト作成
- [ ] CI でテスト自動実行設定

### Week 3: ViewModel分割
- [ ] FileListViewModel 抽出
- [ ] ExportOptionsViewModel 抽出
- [ ] ExportProgressViewModel 抽出
- [ ] MainViewModel スリム化

### Week 4: 機能完成
- [ ] ZipService 統合
- [ ] サブフォルダロジック改善
- [ ] E2E テスト追加

---

## 成功指標

| 指標 | 現状 | 目標 |
|------|------|------|
| テストカバレッジ | ~10% | 60% |
| MainViewModel行数 | 400 | <150 |
| プロジェクト数 | 11 | 5 |
| ビルド時間 | - | 30%削減 |

---

## リスクと対策

| リスク | 対策 |
|--------|------|
| 分割時の回帰バグ | 分割前にテスト追加 |
| レガシー削除で依存切れ | 事前に参照確認 |
| テスト作成で開発遅延 | 優先度付けで最重要のみ先行 |

---

## プロジェクト進め方

### 開発ワークフロー

1. **Issue駆動**
   - 各改善項目をGitHub Issueで管理
   - ラベル: `refactor`, `test`, `cleanup`

2. **ブランチ戦略**
   ```
   main
   ├── refactor/remove-legacy-projects
   ├── refactor/split-mainviewmodel
   ├── test/export-service
   └── feature/zip-integration
   ```

3. **レビュー基準**
   - テストがある（新規コード）
   - 既存テストがパス
   - ドキュメント更新

### spec-kit 活用

大きな変更は spec-kit ワークフローで実行:
```
/speckit.specify  # 要件定義
/speckit.plan     # 実装計画
/speckit.tasks    # タスク分解
/speckit.implement # 実装
```

---

*この計画は状況に応じて柔軟に調整する*
