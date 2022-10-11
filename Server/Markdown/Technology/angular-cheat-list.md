# Angular - Cheat List

## 環境

1. Windows 10
1. Google Chrome or Edge
1. git
1. nodejs (npm)
1. vs code

## 裝一些東西

1. Angular 開發工具

   ```cmd
   npm i -g @angular/cli
   ```

2. 保哥的包的 angular 擴充包

   ```cmd
   code --install-extension "doggy8088.angular-extension-pack"
   ```

---

## 看過但忘了

1. #tKeyword (變數名稱，可以自己取，代表 html arrtibute 的 DOM)

   ```html
   <input type="text" (keyup)="textKeyup($event)" #tKeyword />
   <label>{{ tKeyword.value }}</label>
   ```

   - 一定要綁定事件 ( keyup 之類的)
   - binding 的 keyup、click、change 什麼時候被觸發，tKeyword 就什麼時候更新
   - 只能在 template 或 html 裡面用

---

## 資料繫結方式

1. 內嵌繫結 `{{title}}`
1. 屬性繫結 `[href]='your href'`
1. 事件繫結 `(click)='clickMethod($event)'` = `on-click="clickMethod()"`
1. 雙向繫結 `[(ngModel)]='data'`

   ```ts
      // *.module.ts
      ...
      import { FormsModule } from "@angular/forms";

      @ngModule({
         ...
         imports:[
            ...
            FormsModule,
            ...
         ]
         ...
      })
   ```

---

## 一些指令

1. 執行指令 `npm run start`

   [![image](https://user-images.githubusercontent.com/37999690/125931164-20092dba-7945-4c0d-9979-f59049245319.png "image")](https://user-images.githubusercontent.com/37999690/125931164-20092dba-7945-4c0d-9979-f59049245319.png)

2. 建立新專案 `ng new {project-name}`
   - project-name 輸入英文、數字、dash(減號)
3. 建立新 component ng generate component {component name}
   - 會在目前的目錄下新增該 component 的資料夾
   - 縮寫 `ng g c {component name}`
4. 建立新 module ng generate module {module name}
   - 會在目前的目錄下新增該 module 的資料夾
   - 縮寫 `ng g m {module name}`
5. 其他的自己下 `ng g -help` 去找
6. 升級 angular
   - `ng update`

> 想註冊進指定的 module `ng generate xxxxx -m {module name}`

---

## 加入靜態檔案

- 可以直接扔資料夾路徑給他

  [![image](https://user-images.githubusercontent.com/37999690/125929605-a73dccf8-3acf-4aa2-ae39-7a75c81739e1.png "image")](https://user-images.githubusercontent.com/37999690/125929605-a73dccf8-3acf-4aa2-ae39-7a75c81739e1.png)

---

## 處理一些東西

1. Can't bind to 'data-title' since it isn't a known property of 'img'

   [![image](https://user-images.githubusercontent.com/37999690/125964504-4c129bd5-db05-41a7-b3b5-73403a1c93e9.png "image")](https://user-images.githubusercontent.com/37999690/125964504-4c129bd5-db05-41a7-b3b5-73403a1c93e9.png)

   [![image](https://user-images.githubusercontent.com/37999690/125964733-6de25fe3-6f0a-489d-92b6-789fa7ca084a.png "image")](https://user-images.githubusercontent.com/37999690/125964733-6de25fe3-6f0a-489d-92b6-789fa7ca084a.png)

## [ngStyle]

1. ```html
   <input type="text" (keyup)="textKeyup($event)" #tKeyword />
   <label [ngStyle]="{ 'font-size.px': true ? 900 : 50 }">
     {{ tKeyword.value }}
   </label>
   ```

1. ```html
   <input type="text" (keyup)="textKeyup($event)" #tKeyword />
   <label [ngStyle]="{ 'font-size.px': 10 }"> {{ tKeyword.value }} </label>
   ```

1. ```html
   <input type="text" (keyup)="textKeyup($event)" #tKeyword />
   <label [style.font-size.px]="30">{{ tKeyword.value }}</label>
   ```

## [ngClass]

1. ```html
   <input type="text" (keyup)="textKeyup($event)" #tKeyword />
   <label [ngClass]="{ myClass: true }"> {{ tKeyword.value }} </label>
   ```

1. ```html
   <input type="text" (keyup)="textKeyup($event)" #tKeyword />
   <label [ngClass]="['myClass1','myClass2']"> {{ tKeyword.value }} </label>
   ```

## \*ngIf

- ```html
  <input type="text" (keyup)="textKeyup($event)" #tKeyword />
  <label *ngIf="tKeyword.value.length % 2 === 0">{{ tKeyword.value }}</label>
  ```

## [ngSwitch]

- ```html
  <label [ngSwitch]="1 === 1">
    <p *ngSwitchCase="true">1</p>
    <p *ngSwitchCase="false">2</p>
    <p *ngSwitchDefault>3</p>
    {{ tKeyword.value }}
  </label>
  ```

## \*ngFor

- ```html
  <input type="text" (keyup)="textKeyup($event)" #tKeyword />
  <label *ngFor="let item of ['1', '2', '3']; let idx = index">
    {{ idx }} {{ tKeyword.value }}
  </label>
  ```

## [innerHTML]

- ```html
  <div [innerHTML="<label> this is html </label>" ]></div>
  ```
