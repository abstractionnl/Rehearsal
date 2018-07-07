
export namespace Authorization {
  export interface TokenRequestModel {
    userName: string;
  }
  export interface UserModel {
    id: System.Guid;
    userName: string;
  }
}
export namespace QuestionList {
  export interface QuestionListModel {
    answerTitle: string;
    id: System.Guid;
    questions: QuestionList.QuestionModel[];
    questionTitle: string;
    title: string;
    version: number;
  }
  export interface QuestionListOverviewModel {
    answerTitle: string;
    id: System.Guid;
    isDeleted: boolean;
    questionsCount: number;
    questionTitle: string;
    title: string;
  }
  export interface QuestionModel {
    answer: string;
    question: string;
  }
}
export namespace Rehearsal {
  export enum RehearsalQuestionType {
    Open = "Open",
    MultipleChoice = "MultipleChoice"
  }
  export interface AnswerResultModel {
    correctAnswers: string[];
    givenAnswer: string;
    isCorrect: boolean;
    questionId: System.Guid;
  }
  export interface GiveAnswerRequest {
    answer: string;
    questionId: System.Guid;
  }
  export interface MultipleChoiceQuestionModel extends Rehearsal.RehearsalQuestionModel {
    availableAnswers: string[];
    correctAnswer: number;
  }
  export interface OpenRehearsalQuestionModel extends Rehearsal.RehearsalQuestionModel {
    correctAnswers: string[];
  }
  export interface RehearsalQuestionModel {
    answerTitle: string;
    id: System.Guid;
    question: string;
    questionTitle: string;
    type: string;
  }
  export interface RehearsalSessionModel {
    id: System.Guid;
    questions: Rehearsal.RehearsalQuestionModel[];
  }
  export interface StartRehearsalRequest {
    questionListId: System.Guid;
    questionType: Rehearsal.RehearsalQuestionType;
  }
}
export namespace System {
  export interface Guid {
  }
}
