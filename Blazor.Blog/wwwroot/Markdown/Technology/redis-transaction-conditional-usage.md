# Redis Transaction 條件用法

```csharp
private readonly IDatabase redisDb;

/// <summary>
/// key field 是否存在於 cache 中
/// </summary>
/// <param name="key"></param>
/// <param name="field"></param>
/// <returns></returns>
public virtual async Task<bool> IsKeyFieldExistsInCache(string key, string field)
{
    var dataExist = await redisDb.HashExistsAsync(key, field);

    if (dataExist && await redisDb.HashGetAsync(key, field) == true)
        return true;

    var redisTrans = redisDb.CreateTransaction();

    // 1. 如果 key 不存在才執行下一個動作
    redisTrans.AddCondition(Condition.HashNotExists(key, field));

    // 2. 第 1 點條件成立才會執行
    var setValueTask = redisTrans.HashSetAsync(key, field, true);

    // 有完整執行到第 2 點 committed 才會是 true
    var committed = await redisTrans.ExecuteAsync();

    if (!committed && setValueTask.IsCanceled)
        return true;

    if (committed)
        return false;

    logger.LogWarning("{key} {field} committed {committed}. {IsCanceled},{IsCompleted},{IsCompletedSuccessfully},{IsFaulted}",
        key, field, committed, setValueTask.IsCanceled, setValueTask.IsCompleted, setValueTask.IsCompletedSuccessfully, setValueTask.IsFaulted);

    return true;
}
```
