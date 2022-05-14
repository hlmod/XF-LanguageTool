// This file is a part of XenForo Language Tool.
// All rights reserved.
//
// Developed for HLmod Community.

namespace HLmod.XenForo.LanguageTool.Extensions;

internal static class TaskExtension
{
    public static Task<T> AsStarted<T>(this Task<T> task)
    {
        task.Start();
        return task;
    }

    public static Task AsStarted(this Task task)
    {
        task.Start();
        return task;
    }
}
