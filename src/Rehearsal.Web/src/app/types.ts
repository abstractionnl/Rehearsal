
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
  interface AnswerResultModel {
    correctAnswer: string;
    givenAnswer: string;
    isCorrect: boolean;
  }
  interface GiveAnswerRequest {
    answer: string;
    questionId: System.Guid;
  }
  interface OpenRehearsalQuestionModel extends Rehearsal.RehearsalQuestionModel {
    correctAnswer: string;
  }
  interface RehearsalQuestionModel {
    answerTitle: string;
    id: System.Guid;
    question: string;
    questionTitle: string;
    type: string;
  }
  interface RehearsalSessionModel {
    id: System.Guid;
    questions: Rehearsal.RehearsalQuestionModel[];
  }
  interface StartRehearsalRequest {
    questionListId: System.Guid;
  }
}
declare namespace System {
  interface Guid {
  }
}
