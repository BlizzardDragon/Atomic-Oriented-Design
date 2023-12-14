using System;
using JetBrains.Annotations;

namespace Composable
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ComposeAttribute : Attribute
    {
    }
}