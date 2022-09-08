# jQuery checkbox 模糊匹配

## 1.[att=value] 匹配包含給定屬性的元素

```js
$("[name=checkbox3]").click();
```

[![image](https://user-images.githubusercontent.com/37999690/125259976-707c5b00-e332-11eb-9903-76792306e7d3.png "image")](https://user-images.githubusercontent.com/37999690/125259976-707c5b00-e332-11eb-9903-76792306e7d3.png)

## 2.[att*=value] 模糊匹配

```js
$("[name*=x1]").click();
```

[![image](https://user-images.githubusercontent.com/37999690/125260100-90ac1a00-e332-11eb-94fe-45867f5231f4.png "image")](https://user-images.githubusercontent.com/37999690/125260100-90ac1a00-e332-11eb-94fe-45867f5231f4.png)

## 3.[att!=value] 不能是這個值

```js
$("[name!=checkbox3]").click();
```

[![image](https://user-images.githubusercontent.com/37999690/125260227-b5a08d00-e332-11eb-9faa-debe3906ae74.png "image")](https://user-images.githubusercontent.com/37999690/125260227-b5a08d00-e332-11eb-9faa-debe3906ae74.png)

## 4.[att$=value] 結尾是這個值

```js
$("[name$=test]").click();
```

[![image](https://user-images.githubusercontent.com/37999690/125260372-dc5ec380-e332-11eb-927a-5ec8c99442f7.png "image")](https://user-images.githubusercontent.com/37999690/125260372-dc5ec380-e332-11eb-927a-5ec8c99442f7.png)

## 5.[att^=value] 開頭是這個值

```js
$("[name^=checkbox]").click();
```

[![image](https://user-images.githubusercontent.com/37999690/125260489-f26c8400-e332-11eb-92d7-0b9d90ce8e42.png "image")](https://user-images.githubusercontent.com/37999690/125260489-f26c8400-e332-11eb-92d7-0b9d90ce8e42.png)
