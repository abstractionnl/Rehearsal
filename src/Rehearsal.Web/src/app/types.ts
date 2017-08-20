
declare namespace Rehearsal {
  interface QuestionList {
    answerTitle: string;
    id: System.Guid;
    questions: Rehearsal.QuestionList.Item[];
    questionTitle: string;
    title: string;
  }
  interface QuestionListOverview {
    answerTitle: string;
    id: System.Guid;
    questionCount: number;
    questionTitle: string;
    title: string;
  }
}
declare namespace Rehearsal.QuestionList {
  interface Item {
    answer: string;
    question: string;
  }
}
declare namespace System {
  interface Guid {
  }
}
