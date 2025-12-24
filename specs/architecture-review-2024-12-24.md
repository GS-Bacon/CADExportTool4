# CADExportTool4 アーキテクチャレビューレポート

**日付:** 2024-12-24
**対象:** WPF + Prism + MVVM アーキテクチャ

---

## 目次

1. [エグゼクティブサマリー](#1-エグゼクティブサマリー)
2. [プロジェクト構造分析](#2-プロジェクト構造分析)
3. [MVVMパターン実装評価](#3-mvvmパターン実装評価)
4. [Prism使用状況](#4-prism使用状況)
5. [SOLID原則の遵守状況](#5-solid原則の遵守状況)
6. [DRY原則違反](#6-dry原則違反)
7. [WPFベストプラクティス](#7-wpfベストプラクティス)
8. [セキュリティ・安定性の懸念](#8-セキュリティ安定性の懸念)
9. [改善提案](#9-改善提案)
10. [優先度付きアクションアイテム](#10-優先度付きアクションアイテム)

---

## 1. エグゼクティブサマリー

### 総合評価: ⭐⭐⭐☆☆ (3/5)

**強み:**
- ✅ Prism + CommunityToolkit.Mvvm による現代的なMVVM実装
- ✅ Material Design による統一されたUI/UX
- ✅ インターフェース駆動のサービス設計
- ✅ 非同期処理（async/await）の活用
- ✅ Converterパターンによる拡張性

**弱み:**
- ❌ MainViewModelの責任過多（SRP違反）
- ❌ Converterの直接インスタンス化（DIP違反）
- ❌ エラーハンドリングの不十分さ
- ❌ UIスレッドセーフティの問題
- ❌ リソースリークの可能性

---

## 2. プロジェクト構造分析

### 現在の構造

```
CADExportTool.WPF/           # UIレイヤー
├── App.xaml / App.xaml.cs
├── Converters/
├── Helpers/
├── Services/
├── ViewModels/
└── Views/

CADExportTool.Core/          # ドメインレイヤー
├── Enums/
├── Interfaces/
└── Models/

CADExportTool.Services/      # インフラストラクチャレイヤー
├── Converters/
└── Services
```

### 評価

| 観点 | 状態 | コメント |
|------|------|----------|
| レイヤー分離 | ✅ 良好 | Core/Services/WPFの3層構造 |
| 依存方向 | ✅ 良好 | WPF → Services → Core |
| 名前空間の一貫性 | ⚠️ 注意 | Services内にConvertersフォルダがある |
| 再利用性 | ⚠️ 注意 | Coreは再利用可能だがWPF特有の実装が混在 |

---

## 3. MVVMパターン実装評価

### 3.1 ViewModel実装

**MainViewModel の問題点:**

```csharp
// 現状: 22個のObservableProperty、4つの責任
public partial class MainViewModel : ObservableObject, IDropTarget
{
    // 責任1: ファイル管理
    [ObservableProperty] private ObservableCollection<CadFileItem> _fileList;

    // 責任2: エクスポート実行
    [RelayCommand] private async Task ExportAsync() { ... }

    // 責任3: Drag&Drop処理
    public void DragOver(IDropInfo dropInfo) { ... }

    // 責任4: オプション管理
    [ObservableProperty] private ExportOptions _exportOptions;
}
```

**問題:**
- God Object アンチパターン
- テストが困難
- 変更の影響範囲が大きい

### 3.2 View結合

```xaml
<!-- 不整合な設定 -->
<Window prism:ViewModelLocator.AutoWireViewModel="False" />
<UserControl prism:ViewModelLocator.AutoWireViewModel="True" />
```

**問題:** 設定の一貫性がない

### 3.3 データバインディング

```xaml
<!-- 深いパス指定 -->
<CheckBox IsChecked="{Binding ExportOptions.ExportPdf}" />
```

**問題:**
- ネストされたプロパティ変更通知の問題
- UI応答性への影響

---

## 4. Prism使用状況

### 4.1 DIコンテナ設定

```csharp
// App.xaml.cs
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    // Singleton
    containerRegistry.RegisterSingleton<ISolidWorksService, SolidWorksService>();

    // Transient
    containerRegistry.Register<IExportService, ExportService>();
}
```

**問題点:**

| 問題 | 詳細 |
|------|------|
| ライフサイクル不整合 | ExportService(Transient)がSolidWorksService(Singleton)に依存 |
| ファクトリー未使用 | 複雑なオブジェクト生成がない |
| モジュール化なし | 全登録がApp.xaml.csに集中 |

### 4.2 未使用のPrism機能

- ❌ IEventAggregator（ViewModel間通信）
- ❌ IModule（モジュール分割）
- ❌ Navigation Journal（ナビゲーション履歴）
- ❌ Dialog Service（ダイアログ管理）

---

## 5. SOLID原則の遵守状況

### 5.1 単一責任の原則 (SRP) - ❌ 違反

**MainViewModel:**
- ファイル管理
- エクスポート実行
- Drag&Drop処理
- オプション管理
- 進捗管理

**ExportService:**
- エクスポート実行
- 進捗報告
- 統計計算

### 5.2 オープン・クローズドの原則 (OCP) - ⚠️ 部分的

**良い例:** Converterクラス
```csharp
public abstract class BaseConverter : IFileConverter
{
    protected abstract Task<object?> OpenDocumentAsync(...);
}
```

**違反:** ExportService
```csharp
// 新フォーマット追加時にメソッド追加が必要
private async Task ProcessDrawingAsync(...) { ... }
private async Task ProcessPartAsync(...) { ... }
```

### 5.3 リスコフの置換原則 (LSP) - ✅ 準拠

Converter実装は正しくベースクラスの契約を守っている

### 5.4 インターフェース分離の原則 (ISP) - ✅ 準拠

各インターフェースが適切なサイズ:
- `IExportService`
- `ISolidWorksService`
- `IThumbnailService`
- `IZipService`
- `IFileConverter`

### 5.5 依存性逆転の原則 (DIP) - ❌ 違反

```csharp
// ExportService.cs - 具象クラスへの直接依存
public ExportService(...)
{
    _drawingConverter = new DrawingConverter();   // ❌
    _partConverter = new PartConverter();         // ❌
    _assemblyConverter = new AssemblyConverter(); // ❌
}
```

---

## 6. DRY原則違反

### 6.1 エクスポート処理の重複

```csharp
// ExportService.cs
private async Task ProcessDrawingAsync(...) {
    if (options.ExportPdf) await ConvertFileAsync(...);
    if (options.ExportDxf) await ConvertFileAsync(...);
}

private async Task ProcessPartAsync(...) {
    if (options.ExportIgs) await ConvertFileAsync(...);
    if (options.ExportStep) await ConvertFileAsync(...);
    if (options.Export3mf) await ConvertFileAsync(...);
}
```

### 6.2 ファイルタイプ判定の重複

```csharp
// MainViewModel.cs
HasDrawingFiles = FileList.Any(f => f.FileType == CadFileType.Drawing);

// ExportService.cs
var drawingCount = fileList.Count(f => f.FileType == CadFileType.Drawing);
```

### 6.3 XAML RadioButtonの重複

```xaml
<!-- 出力先とZIP先で同じパターン -->
<RadioButton Content="同じフォルダー内に出力"
             IsChecked="{Binding ExportOptions.OutputFolderOption, ...}" />
<RadioButton Content="同じフォルダー"
             IsChecked="{Binding ExportOptions.ZipFolderOption, ...}" />
```

---

## 7. WPFベストプラクティス

### 7.1 リソース管理

| 項目 | 状態 | 問題 |
|------|------|------|
| テーマリソース | ⚠️ | MainView.xaml.Resourcesに限定 |
| スタイル分離 | ❌ | ResourceDictionary未使用 |
| イベントリスナー | ⚠️ | 例外時のクリーンアップ不保証 |

### 7.2 非同期処理

**問題1: UIスレッドへの戻り不保証**
```csharp
var result = await _exportService.ExportAsync(...);
// この後、UIスレッドで実行される保証がない
```

**問題2: Progress<T>のスレッドセーフティ**
```csharp
var progress = new Progress<ExportProgress>(p => {
    ProgressValue = p.Current;  // UIスレッド以外からの呼び出しの可能性
});
```

**問題3: ブロッキングMessageBox**
```csharp
catch (Exception ex) {
    MessageBox.Show(ex.Message, "エラー", ...);  // UIスレッドをブロック
}
```

### 7.3 エラーハンドリング

**問題1: エラー情報の喪失**
```csharp
// SolidWorksService.cs
catch (Exception ex) {
    Debug.WriteLine($"Error: {ex.Message}");
    return null;  // エラー情報が失われる
}
```

**問題2: 例外の詳細が失われる**
```csharp
catch (Exception ex) {
    result.Errors.Add($"エラー: {ex.Message}");
    // StackTrace, InnerException が記録されない
}
```

### 7.4 リソースリークの可能性

| リソース | 問題 |
|----------|------|
| SystemEvents | 例外時に解除されない可能性 |
| Lazy<SldWorks> | Disposeで解放されていない |
| CancellationTokenSource | タイムアウト未設定 |

---

## 8. セキュリティ・安定性の懸念

### 8.1 Path Traversal

```csharp
// ユーザー入力を直接結合
var outputFolder = Path.Combine(baseFolder, options.SelectedSubFolder);
```

**対策:** `Path.GetFullPath()`で検証

### 8.2 COM Interop例外

SolidWorks COM オブジェクトの呼び出しは予測不可能な例外を発生させる可能性

**対策:** すべてのCOM呼び出しをtry-catchで保護

### 8.3 ファイルシステム権限

`Directory.CreateDirectory`、`File.Delete`は権限エラーに対応していない

**対策:** 権限チェック機能の追加

---

## 9. 改善提案

### 9.1 ViewModelの責任分割

```csharp
// 提案構造
public class MainViewModel {
    public FileListViewModel FileListVM { get; }
    public ExportOptionsViewModel OptionsVM { get; }
    public ExportProgressViewModel ProgressVM { get; }
}
```

### 9.2 Converterファクトリーの導入

```csharp
public interface IFileConverterFactory {
    IFileConverter GetConverter(CadFileType fileType);
}

// DIで登録
containerRegistry.Register<IFileConverterFactory, FileConverterFactory>();
```

### 9.3 Result<T>パターンの導入

```csharp
public class Result<T> {
    public bool IsSuccess { get; }
    public T? Data { get; }
    public string? ErrorMessage { get; }
    public Exception? Exception { get; }
}
```

### 9.4 UIスレッド保護

```csharp
var progress = new Progress<ExportProgress>(p => {
    Application.Current?.Dispatcher.Invoke(() => {
        ProgressValue = p.Current;
        StatusMessage = p.StatusMessage;
    });
});
```

### 9.5 モジュール化

```csharp
// Feature-based modules
CADExportTool.Modules.Export/
├── ExportModule.cs (IModule実装)
├── Views/
├── ViewModels/
└── Services/
```

---

## 10. 優先度付きアクションアイテム

### 🔴 高優先度（安定性・バグ防止）

| # | 項目 | 影響 | 工数 |
|---|------|------|------|
| 1 | UIスレッド保護の追加 | クラッシュ防止 | 小 |
| 2 | エラーハンドリング強化 | デバッグ容易化 | 中 |
| 3 | リソースリーク対策 | メモリ安定性 | 小 |
| 4 | Path Traversal対策 | セキュリティ | 小 |

### 🟡 中優先度（設計改善）

| # | 項目 | 影響 | 工数 |
|---|------|------|------|
| 5 | Converterファクトリー導入 | テスト容易性 | 中 |
| 6 | DRY原則違反の解消 | 保守性向上 | 中 |
| 7 | ResourceDictionary分離 | 再利用性向上 | 小 |

### 🟢 低優先度（構造改善）

| # | 項目 | 影響 | 工数 |
|---|------|------|------|
| 8 | ViewModel責任分割 | 保守性向上 | 大 |
| 9 | Prismモジュール化 | スケーラビリティ | 大 |
| 10 | IEventAggregator導入 | 疎結合化 | 中 |

---

## 付録: コード品質メトリクス

| ファイル | 行数 | 複雑度 | 問題数 |
|----------|------|--------|--------|
| MainViewModel.cs | ~350 | 高 | 5 |
| ExportService.cs | ~200 | 中 | 3 |
| SolidWorksService.cs | ~150 | 中 | 2 |
| MainView.xaml | ~400 | 中 | 2 |

---

*このレポートはプロジェクトの現状分析と改善提案を目的としています。*

---

## 11. 実装完了報告 (2024-12-24)

### 完了したタスク

#### 🔴 高優先度（安定性・バグ防止）

| # | 項目 | 状態 | 実装内容 |
|---|------|------|----------|
| 1 | UIスレッド保護の追加 | ✅ 完了 | MainViewModelで既にDispatcher.Invoke実装済み |
| 2 | エラーハンドリング強化 | ✅ 完了 | Result<T>パターンをCore/Models/Result.csに追加 |
| 3 | リソースリーク対策 | ✅ 完了 | ThemeHelper.csにスレッドセーフな監視管理を追加 |
| 4 | Path Traversal対策 | ✅ 完了 | ExportService.csにパス正規化と検証を追加 |

#### 🟡 中優先度（設計改善）

| # | 項目 | 状態 | 実装内容 |
|---|------|------|----------|
| 5 | Converterファクトリー導入 | ✅ 完了 | IFileConverterFactory + FileConverterFactory実装、DIコンテナ登録 |
| 6 | DRY原則違反の解消 | ✅ 完了 | ファクトリーパターン導入により重複解消 |
| 7 | ResourceDictionary分離 | ✅ 完了 | Styles/FileListStyles.xamlに分離 |

### 変更ファイル一覧

```
CADExportTool.Core/
├── Interfaces/
│   └── IFileConverterFactory.cs  (新規)
└── Models/
    └── Result.cs                  (新規)

CADExportTool.Services/
├── FileConverterFactory.cs       (新規)
└── ExportService.cs              (更新: ファクトリー使用 + Path Traversal対策)

CADExportTool.WPF/
├── App.xaml                      (更新: スタイル参照追加)
├── App.xaml.cs                   (更新: 終了時エラー処理)
├── Helpers/
│   └── ThemeHelper.cs            (更新: スレッドセーフ化)
├── Styles/
│   └── FileListStyles.xaml       (新規)
└── Views/
    └── MainView.xaml             (更新: ローカルリソース削除)
```

### ビルド結果

```
ビルドに成功しました。
    0 個の警告
    0 エラー
```
