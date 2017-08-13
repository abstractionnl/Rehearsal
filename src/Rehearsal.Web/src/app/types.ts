
declare namespace Rehearsal.Messages {
  interface QuestionList {
    answerTitle: string;
    id: System.Guid;
    questions: Rehearsal.Messages.QuestionList.Item[];
    questionTitle: string;
    title: string;
  }
}
declare namespace Rehearsal.Messages.QuestionList {
  interface Item {
    answer: string;
    question: string;
  }
}
declare namespace System {
  interface Guid {
  }
}
