/// <reference path="../../types.ts" />

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;
import * as Joi from "joi-browser";
import {IValidationResult} from "../../validation";

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

export function selectCanSave(state: AppState): boolean {
    return !state.questionListEditor.isPristine && state.questionListEditor.list !== null && state.questionListEditor.isValid;
}

export function selectCanDelete(state: AppState): boolean {
    return state.questionListEditor.list  !== null && state.questionListEditor.list.id !== null;
}

export function selectCanSwap(state: AppState): boolean {
    return state.questionListEditor.list !== null;
}

export function selectCanCopy(state: AppState): boolean {
    return state.questionListEditor.list  !== null && state.questionListEditor.list.id !== null && state.questionListEditor.isValid;
}

function stripEmptyQuestions(list: QuestionListModel): QuestionListModel {
    return {
        ...list,
        questions: list.questions.filter(q => q.question || q.answer)
    };
}

const validationSchema = Joi.object().keys({
    'title': Joi.string().trim().required(),
    'questionTitle': Joi.string().trim().required(),
    'answerTitle': Joi.string().trim().required(),
    'questions': Joi.array().items(
        Joi.object().keys({
            'question': Joi.string().trim().required(),
            'answer': Joi.string().trim().required()
        })
    )
}).unknown();

export function sanitizeQuestionList(list: QuestionListModel): IValidationResult<QuestionListModel> {
    return Joi.validate(stripEmptyQuestions(list), validationSchema);
}
