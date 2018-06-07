using System;
using System.Collections.Generic;
using LanguageExt;
using NFluent;
using NFluent.Extensibility;

namespace Rehearsal.Xunit
{
    public static class NFluentExtensions
    {
        public static void PerformAssertions<T>(this ICheck<T> check, params Action<T>[] assertions)
        {
            var checker  = ExtensibilityHelper.ExtractChecker(check);

            var value = checker.Value;
            
            foreach (var assertion in assertions)
            {
                assertion(value);
            }
        }

        public static void Satisfies<T>(this ICheck<T> check, params Action<ICheck<T>>[] assertions)
        {
            foreach (var assertion in assertions)
            {
                assertion(check);
            }
        }
        
        public static ICheck<TOut> Selecting<T, TOut>(this ICheck<T> check, Func<T, TOut> selector)
        {
            var checker  = ExtensibilityHelper.ExtractChecker(check);
            var newValue = selector(checker.Value);

            return Check.That(newValue);
        }

        public static ICheckLink<ICheck<TType>> IsInstanceOf<T, TType>(this ICheck<T> check)
            where TType: class, T
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);
            var valueAsTType = checker.Value as TType;
            
            checker.ExecuteNotChainableCheck(
                () =>
                {
                    if (valueAsTType == null)
                    {
                        var message = checker.BuildMessage($"The {{0}} is not of type {typeof(TType).Name}.")
                            .ExpectedType(typeof(TType));
                        throw new FluentCheckException(message.ToString());
                    }

                    //return Check.That(valueAsTType);
                },
                checker.BuildMessage($"The {{0}} is of type {typeof(TType).Name}, whereas it must not.").ExpectedType(typeof(TType)).ToString()
            );

            var newcheck = Check.That(valueAsTType);
            return ExtensibilityHelper.ExtractChecker(newcheck).BuildChainingObject();
        }

        public static ICheckLinkWhich<ICheck<IEnumerable<T>>, ICheck<TInstance>> ContainsInstanceOf<T, TInstance>(this ICheck<IEnumerable<T>> check)
            where TInstance: T
        {
            var checker = ExtensibilityHelper.ExtractChecker(check);
            return checker.ExecuteCheckAndProvideSubItem(
                () =>
                {
                    using (var scan = checker.Value.GetEnumerator())
                    {
                        int? index = null;
                        for (var i = 0; scan.MoveNext(); i++)
                        {
                            if (scan.Current is TInstance)
                            {
                                index = i;
                                break;
                            }
                        }

                        if (!index.HasValue)
                        {
                            var message = checker.BuildMessage($"The {{0}} does not contain any element that is of type {typeof(TInstance).Name}.").ExpectedType(typeof(TInstance));
                            throw new FluentCheckException(message.ToString());
                        }

                        var itemCheck = Check.That((TInstance) scan.Current);
                        var subChecker = ExtensibilityHelper.ExtractChecker(itemCheck);
                        subChecker.SetSutLabel($"element #{index}");
                        return itemCheck;
                    }
                },
                checker.BuildMessage($"The {{0}} does not contain any element that is of type {typeof(TInstance).Name}, whereas it must not.").ExpectedType(typeof(TInstance)).ToString()    
            );
        }
        
        public static ICheckLinkWhich<ICheck<Option<T>>, ICheck<T>> IsSome<T>(this ICheck<Option<T>> check, string message = null)
        {
            var checker  = ExtensibilityHelper.ExtractChecker(check);

            return checker.ExecuteCheckAndProvideSubItem(() =>
            {
                if (checker.Value.IsNone)
                {
                    message = message ?? checker.BuildMessage("The option has no value").ToString();
                    throw new FluentCheckException(message);
                }

                return Check.That(checker.Value.IfNone(() => default(T)));
            }, checker.BuildMessage("The option has a value").ToString());
        }
    }
}