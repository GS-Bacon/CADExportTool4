# Issues Report 2024-12-24

> CADExportTool4 | .NET8/WPF+Prism+MD5 | MVVM 3層

## 重大 (P0)

### I-001: ZIP未実装
- 場所: Services/ExportService.cs
- 状況: IZipService注入済,呼出なし
- 影響: ZIPオプション機能しない
- 対応: impl or UI削除

### I-002: COM解放漏れ
- 場所: Services/SolidWorksService.cs:162-180
- 状況: Dispose()でSldWorksのReleaseComObject()なし
- 影響: SWプロセスゾンビ化
- 対応: Marshal.ReleaseComObject追加

## 中 (P1)

### I-003: OpenDoc6 errコード無視
- 場所: SolidWorksService.cs:68-78
- 対応: log出力or例外スロー

### I-004: バリデーション条件誤り
- 場所: MainViewModel.cs:334-354
- 現: `HasDraw&&!hasDFmt && HasPart&&!hasPFmt` (AND)
- 正: `(HasDraw&&!hasDFmt) || (HasPart&&!hasPFmt)` (OR)

### I-005: サブフォルダリスト
- 場所: MainViewModel.cs:310-331
- 問題: 最初のファイルのみからサブフォルダ取得
- 対応: 全ファイル親から収集

### I-006: STA/MTA問題
- 場所: SolidWorksService.cs
- 問題: Task.Run(MTA)内でCOM(STA)呼出
- 対応: Dispatcher経由orSTA専用スレッド

## 低 (P2)

### I-007: Bitmap解放
- 場所: ThumbnailService.cs:30-35
- 対応: using追加

### I-008: 進捗計算
- 問題: ZIP作成タスク含まず
- 対応: ZIP impl時修正

### I-009: ListView選択状態
- 場所: MainView.xaml:60-75
- 問題: IsSelectedバインド未設定
- 対応: SelectionChanged対応

### I-010: レガシーPJ
- 対象: CADExportTool4/,SMZ_CADExportTool/,SolidworksAPIAPI/,TestPJ*5
- 対応: sln除外→archive

## NextAction
1. I-001 ZIP方針決定
2. I-002 Dispose修正
3. I-004 Validate修正
4. テスト実行
