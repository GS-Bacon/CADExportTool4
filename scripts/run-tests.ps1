<#
.SYNOPSIS
    CADExportTool4 テスト実行スクリプト

.DESCRIPTION
    単体テストと統合テストを実行します。
    統合テストにはSolidWorksのインストールが必要です。

.PARAMETER Type
    実行するテストの種類
    - Unit: 単体テストのみ（デフォルト）
    - Integration: 統合テストのみ（SolidWorks必要）
    - All: 全テスト

.EXAMPLE
    .\run-tests.ps1
    # 単体テストを実行

.EXAMPLE
    .\run-tests.ps1 -Type Integration
    # 統合テストを実行

.EXAMPLE
    .\run-tests.ps1 -Type All
    # 全テストを実行
#>

param(
    [ValidateSet("Unit", "Integration", "All")]
    [string]$Type = "Unit"
)

$ErrorActionPreference = "Stop"
$ProjectRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)

function Write-Header {
    param([string]$Message)
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host $Message -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
}

function Test-SolidWorksInstalled {
    $swPath = "C:\Program Files\SolidWorks Corp\SolidWorks\SLDWORKS.exe"
    return Test-Path $swPath
}

# プロジェクトルートに移動
Set-Location $ProjectRoot

Write-Header "CADExportTool4 テスト実行"
Write-Host "テストタイプ: $Type"
Write-Host "プロジェクトルート: $ProjectRoot"

# ビルド
Write-Header "ビルド中..."
dotnet build --configuration Release --verbosity minimal
if ($LASTEXITCODE -ne 0) {
    Write-Host "ビルドに失敗しました" -ForegroundColor Red
    exit 1
}

# 単体テスト
if ($Type -eq "Unit" -or $Type -eq "All") {
    Write-Header "単体テスト実行中..."

    dotnet test `
        --configuration Release `
        --no-build `
        --filter "TestCategory!=Integration" `
        --logger "console;verbosity=detailed" `
        --results-directory "$ProjectRoot\TestResults"

    if ($LASTEXITCODE -ne 0) {
        Write-Host "単体テストに失敗しました" -ForegroundColor Red
        exit 1
    }
    Write-Host "単体テスト完了" -ForegroundColor Green
}

# 統合テスト
if ($Type -eq "Integration" -or $Type -eq "All") {
    Write-Header "統合テスト実行中..."

    # SolidWorksの確認
    if (-not (Test-SolidWorksInstalled)) {
        Write-Host "警告: SolidWorksがインストールされていません" -ForegroundColor Yellow
        Write-Host "統合テストをスキップします" -ForegroundColor Yellow

        if ($Type -eq "Integration") {
            exit 0
        }
    }
    else {
        # テストデータの確認
        $testDataInput = Join-Path $ProjectRoot "testdata\input"
        $testFiles = Get-ChildItem -Path $testDataInput -Filter "*.SLD*" -ErrorAction SilentlyContinue

        if ($testFiles.Count -eq 0) {
            Write-Host "警告: テストデータが見つかりません" -ForegroundColor Yellow
            Write-Host "testdata/input/ にSolidWorksファイルを配置してください" -ForegroundColor Yellow
        }
        else {
            Write-Host "テストファイル: $($testFiles.Count) 件" -ForegroundColor Cyan
            $testFiles | ForEach-Object { Write-Host "  - $($_.Name)" }
        }

        # [Ignore] を外して実行する場合は以下を使用
        # dotnet test --configuration Release --no-build --filter "TestCategory=Integration"

        Write-Host ""
        Write-Host "統合テストは [Ignore] 属性がついています。" -ForegroundColor Yellow
        Write-Host "Visual Studio で手動で実行してください。" -ForegroundColor Yellow
    }
}

Write-Header "テスト完了"
Write-Host ""
