# jQuery陣列操作：$map()、$.grep()、$.each()

## 先來點假資料

```js
function PersonalInfo(name, age, height) {
  this.name = name;
  this.age = age;
  this.height = height;
}

var personalInfos = [
  new PersonalInfo("Alma", 12, 150),
  new PersonalInfo("Beth", 21, 180),
  new PersonalInfo("Celia", 24, 179),
  new PersonalInfo("Daryl", 5, 100),
  new PersonalInfo("Eda", 26, 161),
  new PersonalInfo("Faye", 16, 160),
];
```

## $.map() - 取得特定欄位

```js
var nameList = $.map(personalInfos, function (item, index) {
  return item.name;
});

console.log(nameList); //["Alma", "Beth", "Celia", "Daryl", "Eda", "Faye"]
```

### $.grep - 取得特定資料

```js
var adult = $.grep(personalInfos, function (item, index) {
  return item.age >= 18;
});

console.log(adult);
/**
 * 0: PersonalInfo {name: "Beth", age: 21, height: 180}
 * 1: PersonalInfo {name: "Celia", age: 24, height: 179}
 * 2: PersonalInfo {name: "Eda", age: 26, height: 161}
 */
```

### $.each() - 就你參數最特別

```js
$.each(personalInfos, function (index, item) {
  //...
});
```

### 參考資料

- [jQuery 的陣列操作：$.map() 與 $.grep()](https://cythilya.github.io/2016/03/13/jquery-map-grep/)
- [jQuery.grep() | jQuery API Documentation](https://api.jquery.com/jquery.grep/)
- [jQuery.map() | jQuery API Documentation](https://api.jquery.com/jquery.map/)
- [jQuery.each() | jQuery API Documentation](https://api.jquery.com/jquery.each/)
