using System;
using LanguageExt;

namespace Rehearsal.WebApi
{
    public static class OptionExtensions
    {
        public static T ValueOrThrow<T>(this Option<T> option, Func<string> messageFunc) =>
            option.IfNone(() => throw new InvalidOperationException(messageFunc()));
        
        public static T ValueOrThrow<T>(this Option<T> option, string message) =>
            option.IfNone(() => throw new InvalidOperationException(message));
        
        public static T ValueOrThrow<T>(this Option<T> option) =>
            option.IfNone(() => throw new InvalidOperationException());
    }
}