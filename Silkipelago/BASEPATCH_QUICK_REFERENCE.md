# BasePatch Quick Reference Guide

## 📚 What is BasePatch?

A utility class that eliminates boilerplate error handling code in Harmony patches.

**Location**: `Silkipelago\HarmonyPatches\BasePatch.cs`

---

## 🚀 How to Use

### For Patches That Return bool

```csharp
[HarmonyPatch(...)]
public static class MyPatch
{
    public static bool Prefix(...)
    {
        return BasePatch.SafeExecute(
            () => HandleLogic(...),
            nameof(MyPatch),
            nameof(Prefix)
        );
    }

    private static bool HandleLogic(...)
    {
        BasePatch.Logger.LogInfo("Doing something");
        // Your logic here
        return MethodPrefix.RUN_ORIGINAL_METHOD;
    }
}
```

### For Patches That Return void (Postfix)

```csharp
[HarmonyPatch(...)]
public static class MyPatch
{
    public static void Postfix(...)
    {
        BasePatch.SafeExecuteVoid(
            () => HandleLogic(...),
            nameof(MyPatch),
            nameof(Postfix)
        );
    }

    private static void HandleLogic(...)
    {
        BasePatch.Logger.LogInfo("Doing something");
        // Your logic here
    }
}
```

### Special Case: ref Parameters

Can't use ref parameters in lambdas. Keep error handling inline:

```csharp
public static bool Prefix(ref SomeType value)
{
    try
    {
        // Your logic with 'value' here
        return MethodPrefix.RUN_ORIGINAL_METHOD;
    }
    catch (Exception ex)
    {
        BasePatch.Logger.LogErrorException(nameof(MyPatch), nameof(Prefix), ex);
        return MethodPrefix.RUN_ORIGINAL_METHOD;
    }
}
```

---

## 📝 Logging

Use `BasePatch.Logger` for all logging:

```csharp
BasePatch.Logger.LogInfo("Info message");
BasePatch.Logger.LogDebug("Debug message");
BasePatch.Logger.LogWarning("Warning message");
BasePatch.Logger.LogError("Error message");
BasePatch.Logger.LogErrorException(nameof(Patch), nameof(Method), ex);
```

---

## 🎯 Magic Strings

Use `LocationConstants` instead of hardcoded strings:

```csharp
// ❌ Don't do this:
if (locationChecker.LocationExists("Eva: 0 Slots"))

// ✅ Do this:
if (locationChecker.LocationExists(LocationConstants.EvaUpgradeLocation))
```

**Available Constants**:
- `LocationConstants.EvaUpgradeLocation` = "Eva: 0 Slots"
- `LocationConstants.CheatConsoleLocation` = "Cheat Console"
- `LocationConstants.ServerLocation` = "Server"

---

## ✅ Patch Template

Use this as a starting point for new patches:

```csharp
using HarmonyLib;
using Silkipelago.Constants;

namespace Silkipelago.HarmonyPatches
{
    /// <summary>
    /// Brief description of what this patch does.
    /// </summary>
    [HarmonyPatch(typeof(TargetClass))]
    [HarmonyPatch(nameof(TargetClass.TargetMethod))]
    public static class MyNewPatch
    {
        /// <summary>
        /// Brief description of the prefix/postfix.
        /// </summary>
        public static bool Prefix(...)
        {
            return BasePatch.SafeExecute(
                () => HandleLogic(...),
                nameof(MyNewPatch),
                nameof(Prefix)
            );
        }

        private static bool HandleLogic(...)
        {
            BasePatch.Logger.LogInfo("Starting operation");
            
            // Your logic here
            
            return MethodPrefix.RUN_ORIGINAL_METHOD;
        }
    }
}
```

---

## 🔍 Common Patterns

### Pattern 1: Check and Skip

```csharp
private static bool CheckAndSkip(FullQuestBase quest)
{
    if (ShouldSkip(quest))
    {
        BasePatch.Logger.LogInfo($"Skipping: {quest.name}");
        return MethodPrefix.DONT_RUN_ORIGINAL_METHOD;
    }
    return MethodPrefix.RUN_ORIGINAL_METHOD;
}
```

### Pattern 2: Modify and Continue

```csharp
private static bool ModifyAndContinue(FullQuestBase quest)
{
    if (NeedsModification(quest))
    {
        quest.rewardItem = null;
        BasePatch.Logger.LogInfo($"Modified: {quest.name}");
    }
    return MethodPrefix.RUN_ORIGINAL_METHOD;
}
```

### Pattern 3: Conditional Logic

```csharp
private static bool ConditionalLogic(Item item)
{
    var shouldBlock = item.Type == "Crest" 
        && locationChecker.LocationExists(LocationConstants.EvaUpgradeLocation);
    
    return shouldBlock 
        ? MethodPrefix.DONT_RUN_ORIGINAL_METHOD 
        : MethodPrefix.RUN_ORIGINAL_METHOD;
}
```

---

## 📋 Checklist for New Patches

- [ ] Use `BasePatch.SafeExecute()` or `BasePatch.SafeExecuteVoid()`
- [ ] Use `BasePatch.Logger` for all logging
- [ ] Extract magic strings to `LocationConstants`
- [ ] Add XML documentation (/// <summary>)
- [ ] Create private helper method for logic
- [ ] Test with `run_build`
- [ ] Review error handling

---

## ⚡ Benefits

✅ **No Boilerplate**: Error handling is automatic  
✅ **Consistent**: All patches use the same pattern  
✅ **Maintainable**: Changes to error handling happen in one place  
✅ **Readable**: Patch logic is cleaner and easier to understand  
✅ **Testable**: Logic is separated from error handling  

---

## 📚 Examples in Codebase

These patches already use BasePatch:

- `CrestEquipPatch.cs`
- `GameManagerPatch.cs`
- `SaveSerializePatch.cs`
- `PauseMenuButtonOnSubmitPatch.cs`
- `QuestManagerPatch.cs` (4 classes)
- `GetQuestRewardPatch.cs`

Review these for reference implementations!

---

## 🆘 Troubleshooting

**"SafeExecute does not exist"**
- Add: `using Silkipelago.HarmonyPatches;`

**"Cannot use ref in lambda"**
- Keep error handling inline for ref parameters
- See "Special Case: ref Parameters" above

**Build error with Logger**
- Ensure you're using `BasePatch.Logger` not `ArchipelagoPlugin.App.Logger`

---

## 🔗 Related Files

- `BasePatch.cs` - Main utility class
- `LocationConstants.cs` - Centralized location strings
- `BASEPATCH_APPLICATION_SUMMARY.md` - Implementation summary
- `BASEPATCH_BEFORE_AFTER.md` - Before/after examples
