# 🎹 簡易電子琴 (BeepPlayer)

> 一個以 C# Windows Forms 開發的簡易電子琴應用程式，透過系統 Beep 音效模擬鋼琴八個音階。

---

## 📋 專案資訊

| 項目 | 內容 |
|------|------|
| 專案名稱 | 簡易電子琴 (BeepPlayer) |
| 開發語言 | C# (.NET Windows Forms) |
| 班級作業編號 | 1131501 |
| 作者 | 林昱綸 |

---

## 🎵 功能介紹

### 核心功能
- **八音階演奏**：提供 Do、Re、Mi、Fa、Sol、La、Si、高音Do 共 8 個音階按鈕
- **Beep 音效輸出**：使用 Windows 核心 API `kernel32.dll` 的 `Beep()` 函式發出對應頻率音效
- **視窗等比縮放**：拖曳調整視窗大小時，所有控制項自動等比例重新排列

### 音階對照表

| 按鈕 | 音階 | 頻率 (Hz) |
|------|------|-----------|
| btn1 | Do (中央C) | 523 |
| btn2 | Re | 587 |
| btn3 | Mi | 659 |
| btn4 | Fa | 698 |
| btn5 | Sol | 784 |
| btn6 | La | 880 |
| btn7 | Si | 988 |
| btn8 | 高音Do | 1046 |

---

## 🏗️ 程式架構

```
frmBeepPlayer (主視窗)
├── InitializeComponent()       // 初始化 UI 元件
├── InitializeButton()          // 綁定 btn2~btn8 共用點擊事件
├── btn1_Click()                // 共用按鈕事件：播放對應頻率 Beep 音
├── frmBeepPlayer_Load()        // 記錄初始控制項位置與尺寸
├── frmBeepPlayer_SizeChanged() // 視窗縮放時等比例調整控制項
└── frmBeepPlayer_FormClosing() // 關閉前彈出確認對話框
```

---

## 🔧 技術細節

### Windows API 呼叫
```csharp
[DllImport("kernel32.dll")]
public static extern bool Beep(int frequency, int duration);
```
透過 P/Invoke 直接呼叫 Windows 核心 API，傳入**頻率 (Hz)** 與**持續時間 (ms)** 發出蜂鳴音。

### 視窗等比縮放機制
1. `Load` 事件觸發時，記錄 `palMain` 內所有控制項的初始位置與大小
2. `SizeChanged` 事件觸發時，計算新舊尺寸比例
3. 依比例重新設定每個控制項的 `Left`、`Top`、`Width`、`Height`
4. 使用 `isLoaded` 旗標防止 `SizeChanged` 在 `Load` 完成前提早執行

### 關閉確認對話框
關閉視窗時彈出 Yes/No 確認框，選擇「否」則取消關閉動作（`e.Cancel = true`）。

---

## 🚀 執行環境需求

- **作業系統**：Windows（需支援 `kernel32.dll`）
- **開發框架**：.NET Framework（Windows Forms）
- **IDE**：Visual Studio 2019 / 2022 建議

---

## 📁 主要檔案

```
1131501_林昱綸_簡易電子琴/
├── frmBeepPlayer.cs       // 主視窗邏輯
├── frmBeepPlayer.Designer.cs  // UI 設計器自動產生程式碼
├── Program.cs             // 程式進入點
└── README.md              // 本說明文件
```

---

## 📝 使用方式

1. 以 Visual Studio 開啟專案
2. 按下 `F5` 執行程式
3. 點擊畫面上的 **Do ~ 高音Do** 按鈕即可播放對應音階
4. 可自由拖曳調整視窗大小，按鈕會自動等比縮放
5. 關閉視窗時會跳出確認對話框

---

## ⚠️ 注意事項

- 本程式使用 Windows 系統蜂鳴器（`Beep` API），在部分現代電腦或虛擬機上可能無聲音輸出
- 需在 Windows 環境下執行，不支援 macOS / Linux# 1131501_林昱綸_簡易電子琴
