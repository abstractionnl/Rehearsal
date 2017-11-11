/// <reference path="../../types.ts" />

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;
import * as Joi from "joi";

export interface QuestionlistEditorState {
    questionListOverview: QuestionListOverviewModel[];
    isPristine: boolean,
    isValid: boolean,
    list: QuestionListModel
}

export interface AppState {
    questionListEditor: QuestionlistEditorState;
}

export const initialState: QuestionlistEditorState = {
    questionListOverview: [],
    list: null,
    isPristine: false,
    isValid: false
};

export function selectQuestionListOverview(state: AppState): QuestionListOverviewModel[]  {
    return state.questionListEditor.questionListOverview;
}

export function selectSelectedQuestionList(state: AppState): QuestionListModel {
    return state.questionListEditor.list;
}

export function selectIsValid(state: AppState): boolean {
    return state.questionListEditor.isValid;
}

export function selectIsPristine(state: AppState): boolean {
    return state.questionListEditor.isPristine;
}

export function stripEmptyQuestions(list: QuestionListModel): QuestionListModel {
    return {
        ...list,
        questions: list.questions.filter(q => q.question || q.answer)
    };
}

const validationSchema = Joi.object().keys({
    'title': Joi.string().required(),
    'questionTitle': Joi.string().required(),
    'answerTitle': Joi.string().required(),
    'questions': Joi.array().items(
        Joi.object().keys({
            'question': Joi.string().required(),
            'answer': Joi.string().required()
        })
    )
});

export function validateQuestionList(list: QuestionListModel) {
    const {error, value} = Joi.validate(stripEmptyQuestions(list), validationSchema);

    return error === null;
}
