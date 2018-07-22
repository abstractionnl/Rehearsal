using System;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Rehearsal
{
    public static class RehearsalFactoryExtensions
    {
        public static RehearsalFactory SetQuestionType(this RehearsalFactory factory, RehearsalQuestionType type)
        {
            switch (type)
            {
                case RehearsalQuestionType.Open:
                    return factory.UseOpenQuestions();
                case RehearsalQuestionType.MultipleChoice:
                    return factory.UseMultipleChoiceQuestions(4);
            }
            
            throw new InvalidOperationException("Unsupported RehearsalQuestionType");
        }
    }
}