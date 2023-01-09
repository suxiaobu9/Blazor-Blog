# 關閉開始選單的關機以及休眠按鈕

## 指令

> 需要在系統管理員權限下指令

```ps1
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Start\HideShutDown /t REG_DWORD /v value /d 1 /f
reg add HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Start\HideSleep /t REG_DWORD /v value /d 1 /f
```

- `/t` 指定註冊碼的 data type `[ REG_SZ | REG_MULTI_SZ | REG_DWORD_BIG_ENDIAN | REG_DWORD | REG_BINARY | REG_DWORD_LITTLE_ENDIAN | REG_NONE | REG_EXPAND_SZ ]`
- `/v` 值的名稱
- `/d` 指派給的數據
- `/f` 在沒有提示的狀況下強制覆蓋原有數值

## UI 介面操作

- `win + R` 快捷鍵開啟 `執行`
- 輸入 `regedit` 後執行
- 找到 `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Start\HideShutDown`
- 修改 `value` 的值
