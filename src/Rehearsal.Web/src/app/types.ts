
declare namespace Authorization {
  interface TokenRequestModel {
    userName: string;
  }
  interface UserModel {
    id: System.Guid;
    userName: string;
  }
}
declare namespace QuestionList {
  interface QuestionListModel {
    answerTitle: string;
    id: System.Guid;
    questions: QuestionList.QuestionModel[];
    questionTitle: string;
    title: string;
    version: number;
  }
  interface QuestionListOverviewModel {
    answerTitle: string;
    id: System.Guid;
    isDeleted: boolean;
    questionsCount: number;
    questionTitle: string;
    title: string;
  }
  interface QuestionModel {
    answer: string;
    question: string;
  }
}
declare namespace Rehearsal {
  interface StartRehearsalRequest {
    questionListId: System.Guid;
  }
}
declare namespace System {
  interface Guid {
  }
}
