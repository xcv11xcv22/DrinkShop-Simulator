# DrinkShop 飲料店模擬器 / DrinkShop Beverage Shop Simulator

一個用 C# 製作的飲料店模擬系統，模擬顧客點飲料、店員製作、排隊與顧客服務。

A beverage shop simulation system built with C#, simulating customer orders, barista drink making, queuing, and customer service.

---

## 功能 Features

- 顧客隨機產生訂單 / Customer random order generation
- 店員製作飲料與經驗升級 / Barista drink making and experience leveling
- 命令模式 / Command Pattern
- 策略模式 / Strategy Pattern
- 狀態模式 / State Pattern
- VIP 等級：加速製作與降低失敗率 / VIP levels with faster making and lower fail rates
- 店員特性系統 / Barista trait system (special skills)
- 機器設備支援（自動販賣機等）/ Machine integration (vending machines etc.)
- 事件總線系統 / Event Bus System
- 紀錄事件至筆記本 / Thread-safe notebook event logging

---

## 快速開始 Quick Start

1. 安裝 .NET 6 或更新版本  
   Install .NET 6 or later.

2. 複製本專案 Clone this repository:

```bash
git clone https://github.com/xcv11xcv22/DrinkShop-Simulator.git
cd drinkshop-simulator
dotnet run
```

> 所有事件記錄將寫入 `event_log.txt`  
> Logs will be written to `event_log.txt`

---


## 版本 Version

**目前版本 / Current Version**：`v0.7` — 多設備支援、VIP 模組、事件總線整合

---

## 未來規劃 Roadmap

- [ ] Unity 或 Blazor 視覺化前端 / GUI frontend (Unity or Blazor)
- [ ] Rx.NET 狀態流整合 / Reactive state architecture
- [ ] 多人併發與壓力測試 / Multi-client & performance testing

---

由 DrinkSim Dev 製作與維護  
Made with ☕ by DrinkSim Dev
