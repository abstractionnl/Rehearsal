/// <reference path="../../types.ts" />

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;

export interface QuestionlistEditorState {
    questionListOverview: QuestionListOverviewModel[];
    selectedQuestionList: QuestionListModel;
}

export function selectQuestionListOverview(state: QuestionlistEditorState): QuestionListOverviewModel[]  {
    return state.questionListOverview;
}

export function selectSelectedQuestionList(state: QuestionlistEditorState): QuestionListModel {
    return state.selectedQuestionList;
}
