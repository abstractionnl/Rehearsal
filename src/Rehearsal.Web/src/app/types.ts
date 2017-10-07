
declare namespace Rehearsal {
  interface QuestionList {
    answerTitle: string;
    id: System.Guid;
    questions: Rehearsal.QuestionList.Item[];
    questionTitle: string;
    title: string;
    version: number;
  }
  interface QuestionListOverview {
    answerTitle: string;
    id: System.Guid;
    isDeleted: boolean;
    questionsCount: number;
    questionTitle: string;
    title: string;
  }
  interface StartRehearsalRequest {
    questionListId: System.Guid;
  }
  interface TokenRequest {
    userName: string;
  }
  interface User {
    id: System.Guid;
    userName: string;
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
