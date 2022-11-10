# 權限運算 - Enum 二元邏輯運算符

- 先宣告一個帶 `[Flags]` 的 Enum 型別，且值必須為 2<sup>n</sup>

```csharp

[Flags]
enum Permission
{
    None = 1,
    Read = 2,
    Create = 4,
    Edit = 8,
    Delete = 16,
    All = Read | Create | Edit | Delete
}

```

## 給 User 權限 ( Logical OR operator `|` )

```csharp
var userPermission = Permission.Read | Permission.Create | Permission.Edit;

Console.WriteLine(nameof(userPermission) + " : " + userPermission);
Console.WriteLine(nameof(userPermission) + " 轉 int" + " : " + (int)userPermission);

// userPermission : Read, Create, Edit
// userPermission 轉 int : 14

```

## 判斷 User 是否有權限 ( Logical AND operator `&` )

```csharp
var userPermission = Permission.Read | Permission.Create | Permission.Edit;

var hasPermission = Permission.Delete == (userPermission & Permission.Delete);

Console.WriteLine(nameof(userPermission) + "是否有 Delete 權限 : " + hasPermission);

// userPermission 是否有 Delete 權限 : False

```

```csharp
var userPermission = Permission.Read | Permission.Create | Permission.Edit;

var targetPermission = Permission.Edit | Permission.Create;

var hasPermission = targetPermission == (userPermission & targetPermission);

Console.WriteLine(nameof(userPermission) + " 是否有 Create 與 Edit 權限 : " + hasPermission);

// userPermission 是否有 Create 與 Edit 權限 : True

```

## Logical XOR operator `^`

- XOR 是把沒有的加上去，如果有的話就移除

```csharp
var userPermission = Permission.Read | Permission.Create | Permission.Edit;

userPermission = userPermission ^ Permission.Read;

Console.WriteLine(nameof(userPermission) + " : " + userPermission);

// userPermission : Create, Edit

userPermission = userPermission ^ Permission.Delete;

Console.WriteLine(nameof(userPermission) + " : " + userPermission);

// userPermission : Create, Edit, Delete

```

## 移除 User 權限

```csharp
var userPermission = Permission.Read | Permission.Create | Permission.Edit;

userPermission = userPermission & (Permission.All ^ Permission.Read);

Console.WriteLine(nameof(userPermission) + " : " + userPermission);

// userPermission : Create, Edit

userPermission = Permission.Read | Permission.Create | Permission.Edit;

// ~ 可以 Google :  Bitwise complement operator
userPermission = userPermission & ~Permission.Read;

Console.WriteLine(nameof(userPermission) + " : " + userPermission);

// userPermission : Create, Edit

```
