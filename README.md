# arrow-world
## 1. 游戏背景

形成斑驳的光影。微风轻拂，树枝沙沙作响，周围一片宁静，唯有鸟鸣和远处溪流的流水声打破沉寂。靶场位于空旷的草地上，靶子分布在不同距离处，地面铺满了柔软的绿草，四周被古老的石墙环绕，墙面上覆盖着青苔，透露着岁月的痕迹。

这片靶场不仅是常规射箭练习的场地，更是为了特殊训练而设置。新的追踪箭功能加入了挑战，箭头在射出后会发出微弱的光辉，自动调整方向，精准地追踪移动的靶子。靶子不仅有静止的木制靶标，还有悬挂在树上的动态目标，随着风的变化轻轻摆动，增加了射箭的难度。最远处的靶子甚至会时不时地突然消失，考验玩家的反应和精准度。

玩家站在靶场的起点，手持弓和追踪箭，准备射向目标。在射出每一支追踪箭时，箭头的光芒闪烁，随着靶子的动态，它们会调整轨迹，确保命中靶心。这种神奇的力量让射箭变得既神秘又富有挑战性，增加了游戏的趣味和刺激。

通过这片森林中的靶场，玩家不仅能够体验精准射箭的技巧，还能借助追踪箭挑战更远、更复杂的目标，享受弓箭手的荣耀与乐趣。

## 2. uml图

<img src = "UML.jpg" alt = "uml图">

## 3. 关键细节

### 3-1 关于未发生碰撞提早结束游戏的问题

实现code:
```
if (!isChecking)
{
    isChecking = true;
    Invoke("ResetArrowSpawnFlag", 1.8f);
    CheckGameStatus();
}
void ResetArrowSpawnFlag()
{
    isChecking = false;
}

```
下面是对这段代码的详细解释：

1. **条件判断 `if (!isChecking)`**：
   - 这行判断 `isChecking` 是否为 `false`，即当前没有正在进行“检查”操作。如果 `isChecking` 为 `false`，代码会执行大括号内的内容。如果 `isChecking` 为 `true`，说明当前正在进行某些检查操作，因此不会执行大括号内的代码。

2. **`isChecking = true;`**：
   - 将 `isChecking` 设置为 `true`，表示开始进行检查。这样做的目的是防止在检查过程中再次触发检查，确保只有一次检查在进行。

3. **`Invoke("ResetArrowSpawnFlag", 1.8f);`**：
   - `Invoke` 是 Unity 中的一种方法，用于延迟调用某个函数。这里的 `"ResetArrowSpawnFlag"` 是要调用的函数的名称，`1.8f` 是延迟的时间（单位为秒）。在 1.8 秒后，`ResetArrowSpawnFlag` 函数会被自动调用。

4. **`CheckGameStatus();`**：
   - 调用 `CheckGameStatus()` 函数，通常这个函数用来检查游戏的某个状态，比如是否可以生成箭，或者是否满足某个条件来触发某个事件。具体内容取决于 `CheckGameStatus()` 函数的实现。

5. **`void ResetArrowSpawnFlag()`**：
   - 这是一个定义了 `ResetArrowSpawnFlag` 函数的部分，该函数没有参数且返回类型为 `void`，表示没有返回值。

6. **`isChecking = false;`**：
   - 在 `ResetArrowSpawnFlag` 函数中，将 `isChecking` 设置为 `false`，表示检查操作已经完成，可以允许下一次检查操作的触发。

### 总结：

- **功能**：这段代码的目的是防止在检查期间重复触发某个操作，利用 `isChecking` 来确保只有一个检查操作正在进行。通过 `Invoke` 方法，延迟 1.8 秒后调用 `ResetArrowSpawnFlag` 函数，将 `isChecking` 设置为 `false`，允许下一次操作的开始。
  
- **流程**：
  - 当 `isChecking` 为 `false` 时，开始执行检查操作并将 `isChecking` 设置为 `true`。
  - 调用 `CheckGameStatus()` 来检查游戏状态。
  - 延迟 1.8 秒后，调用 `ResetArrowSpawnFlag()`，将 `isChecking` 设置为 `false`，以便后续操作可以继续进行。

这个机制通常用于控制某个频繁操作（如生成箭、游戏状态检查等）在一定时间内只能执行一次，避免重复执行造成的逻辑错误或性能问题。

### 3-2 跟踪箭的实现


