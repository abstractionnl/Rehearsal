
declare namespace Rehearsal {
  interface CreateQuestionListRequest {
    answerTitle: string;
    questions: Rehearsal.CreateQuestionListRequest.Item[];
    questionTitle: string;
    title: string;
  }
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
    questionsCount: number;
    questionTitle: string;
    title: string;
  }
  interface UpdateQuestionListRequest {
    answerTitle: string;
    questions: Rehearsal.UpdateQuestionListRequest.Item[];
    questionTitle: string;
    title: string;
  }
}
declare namespace Rehearsal.CreateQuestionListRequest {
  interface Item {
    answer: string;
    question: string;
  }
}
declare namespace Rehearsal.QuestionList {
  interface Item {
    answer: string;
    question: string;
  }
}
declare namespace Rehearsal.UpdateQuestionListRequest {
  interface Item {
    answer: string;
    question: string;
  }
}
declare namespace System {
  interface Guid {
  }
}
