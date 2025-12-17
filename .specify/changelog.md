# CADExportTool4 Changelog

> 変更履歴と実装記録

## [2025-12-17] WPF + Prism + MVVM 移行

### 概要
Windows FormsアプリをWPF + Prism (DryIoc) + Material Design UIに完全移行

### 新規プロジェクト構造
```
CADExportTool4/
├── CADExportTool.WPF/           # WPFアプリ (スタートアップ)
│   ├── App.xaml                 # Material Designテーマ設定
│   ├── Helpers/                 # ヘルパークラス
│   ├── Services/                # WPF固有サービス
│   ├── Views/                   # XAML Views
│   └── ViewModels/              # ViewModels
│
├── CADExportTool.Core/          # 共有コア
│   ├── Enums/                   # CadFileType, ExportFormat, FolderOption
│   ├── Models/                  # CadFileItem, ExportOptions, ExportResult
│   └── Interfaces/              # サービスインターフェース
│
├── CADExportTool.Services/      # サービス実装
│   ├── SolidWorksService.cs     # シングルトン接続管理
│   ├── ExportService.cs         # エクスポートオーケストレーション
│   └── Converters/              # Drawing/Part/Assembly Converter
│
└── (既存プロジェクト)
```

### NuGetパッケージ
- `Prism.DryIoc` 8.1.97
- `MaterialDesignThemes` 5.1.0
- `MaterialDesignColors` 3.1.0
- `CommunityToolkit.Mvvm` 8.2.2
- `GongSolutions.WPF.DragDrop` 3.2.1

### 主な改善点
1. **SolidWorks接続**: 毎回新規作成 → シングルトン管理
2. **非同期処理**: IProgress<T> + CancellationToken完全対応
3. **DI/IoC**: Prism.DryIocによる依存性注入
4. **テスト容易性**: インターフェース分離
5. **UI**: Material Design モダンUI

---

## [2025-12-17] ダークモード・トースト通知・進捗表示

### 実装内容

#### 1. OSテーマ連動ダークモード
- **ファイル**: `CADExportTool.WPF/Helpers/ThemeHelper.cs`
- **機能**:
  - Windowsレジストリ `AppsUseLightTheme` を監視
  - `SystemEvents.UserPreferenceChanged` でOSテーマ変更をリアルタイム検知
  - Material Designの `PaletteHelper` でライト/ダークテーマを切替

#### 2. トースト通知
- **ファイル**: `CADExportTool.WPF/Services/ToastNotificationService.cs`
- **機能**:
  - Material DesignのSnackbarを使用
  - 変換完了時に成功/失敗メッセージを表示
  - `ShowSuccess`, `ShowError`, `ShowInfo` の3種類を提供

#### 3. プログレスバーにタスク数表示
- **ファイル**: `MainView.xaml`, `MainViewModel.cs`
- **機能**:
  - 「X / Y」形式で現在のタスク数/総タスク数を表示
  - `ProgressText` プロパティでバインディング

### 関連コミット
- `b20da02` feat: ダークモード、トースト通知、進捗数表示を追加
