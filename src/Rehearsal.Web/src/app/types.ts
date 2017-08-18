
declare namespace Rehearsal {
  export interface QuestionList {
    answerTitle: string;
    id: System.Guid;
    questions: Rehearsal.QuestionList.Item[];
    questionTitle: string;
    title: string;
  }
}
declare namespace Rehearsal.QuestionList {
  export interface Item {
    answer: string;
    question: string;
  }
}
declare namespace System {
  export interface Guid {
  }
}
