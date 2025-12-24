using System.Diagnostics;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Serialization;
using CADExportTool.Core.Interfaces;

namespace CADExportTool.Services;

/// <summary>
/// GitHub Releaseを使用した自動更新サービス
/// </summary>
public class UpdateService : IUpdateService
{
    private const string GitHubApiUrl = "https://api.github.com/repos/GS-Bacon/CADExportTool4/releases/latest";
    private const string UserAgent = "CADExportTool4";

    private readonly HttpClient _httpClient;

    public UpdateService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
    }

    public string GetCurrentVersion()
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
        var version = assembly.GetName().Version;
        return version != null ? $"v{version.Major}.{version.Minor}.{version.Build}" : "v0.0.0";
    }

    public async Task<UpdateCheckResult> CheckForUpdateAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var release = await _httpClient.GetFromJsonAsync<GitHubRelease>(GitHubApiUrl, cancellationToken);

            if (release == null)
            {
                return new UpdateCheckResult(false, null, GetCurrentVersion(), null, null);
            }

            var currentVersion = GetCurrentVersion();
            var latestVersion = release.TagName;

            var isUpdateAvailable = CompareVersions(latestVersion, currentVersion) > 0;

            // ZIPアセットのダウンロードURLを探す
            var downloadUrl = release.Assets?
                .FirstOrDefault(a => a.Name?.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) == true)
                ?.BrowserDownloadUrl;

            return new UpdateCheckResult(
                isUpdateAvailable,
                latestVersion,
                currentVersion,
                downloadUrl,
                release.Body);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Update check failed: {ex.Message}");
            return new UpdateCheckResult(false, null, GetCurrentVersion(), null, null);
        }
    }

    public async Task<bool> DownloadAndInstallUpdateAsync(
        string downloadUrl,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var tempPath = Path.Combine(Path.GetTempPath(), "CADExportTool4_update.zip");
            var extractPath = Path.Combine(Path.GetTempPath(), "CADExportTool4_update");

            // ダウンロード
            using (var response = await _httpClient.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var downloadedBytes = 0L;

                await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                await using var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                var buffer = new byte[8192];
                int bytesRead;

                while ((bytesRead = await contentStream.ReadAsync(buffer, cancellationToken)) > 0)
                {
                    await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                    downloadedBytes += bytesRead;

                    if (totalBytes > 0)
                    {
                        var percentage = (int)((downloadedBytes * 100) / totalBytes);
                        progress?.Report(percentage);
                    }
                }
            }

            // 解凍と更新スクリプトの作成
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(tempPath, extractPath);

            // 更新用バッチファイルを作成
            var currentExePath = Process.GetCurrentProcess().MainModule?.FileName;
            if (string.IsNullOrEmpty(currentExePath))
            {
                return false;
            }

            var currentDir = Path.GetDirectoryName(currentExePath)!;
            var batchPath = Path.Combine(Path.GetTempPath(), "CADExportTool4_update.bat");

            var batchContent = $"""
                @echo off
                echo Updating CADExportTool4...
                timeout /t 2 /nobreak > nul
                xcopy /E /Y "{extractPath}\*" "{currentDir}\"
                start "" "{currentExePath}"
                del "%~f0"
                """;

            await File.WriteAllTextAsync(batchPath, batchContent, cancellationToken);

            // バッチファイルを実行してアプリを終了
            Process.Start(new ProcessStartInfo
            {
                FileName = batchPath,
                CreateNoWindow = true,
                UseShellExecute = true
            });

            // アプリケーションを終了
            Environment.Exit(0);

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Update failed: {ex.Message}");
            return false;
        }
    }

    private static int CompareVersions(string? version1, string? version2)
    {
        if (string.IsNullOrEmpty(version1) || string.IsNullOrEmpty(version2))
        {
            return 0;
        }

        // "v1.2.3" → "1.2.3"
        var v1 = version1.TrimStart('v', 'V');
        var v2 = version2.TrimStart('v', 'V');

        var parts1 = v1.Split('.').Select(p => int.TryParse(p, out var n) ? n : 0).ToArray();
        var parts2 = v2.Split('.').Select(p => int.TryParse(p, out var n) ? n : 0).ToArray();

        var maxLength = Math.Max(parts1.Length, parts2.Length);

        for (int i = 0; i < maxLength; i++)
        {
            var p1 = i < parts1.Length ? parts1[i] : 0;
            var p2 = i < parts2.Length ? parts2[i] : 0;

            if (p1 > p2) return 1;
            if (p1 < p2) return -1;
        }

        return 0;
    }

    private class GitHubRelease
    {
        [JsonPropertyName("tag_name")]
        public string? TagName { get; set; }

        [JsonPropertyName("body")]
        public string? Body { get; set; }

        [JsonPropertyName("assets")]
        public List<GitHubAsset>? Assets { get; set; }
    }

    private class GitHubAsset
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("browser_download_url")]
        public string? BrowserDownloadUrl { get; set; }
    }
}
