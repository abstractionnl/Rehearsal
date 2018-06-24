/// <reference path="../../../types.ts" />

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;
import * as Joi from "joi-browser";
import {IValidationResult} from "../../../validation";
import {createFeatureSelector, createSelector} from "@ngrx/store";

export interface QuestionlistEditorState {
    questionListOverview: QuestionListOverviewModel[];
    isPristine: boolean,
    isValid: boolean,
    list: QuestionListModel
}

export interface QuestionlistState {
    questionListEditor: QuestionlistEditorState;
}

export const initialState: QuestionlistEditorState = {
    questionListOverview: [],
    list: null,
    isPristine: false,
    isValid: false
};

export const selectFeature = createFeatureSelector<QuestionlistState>('questionlist');

export const selectQuestionListOverview = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.questionListOverview
);

export const selectSelectedQuestionList = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.list
);

export const selectIsValid = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.isValid
);

export const selectIsPristine = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.isPristine
);

export const selectCanSave = createSelector(
    selectFeature,
    (state: QuestionlistState) => !state.questionListEditor.isPristine && state.questionListEditor.list !== null && state.questionListEditor.isValid
);

export const selectCanDelete = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.list  !== null && state.questionListEditor.list.id !== null
);

export const selectCanSwap = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.list !== null
);

export const selectCanCopy = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.list  !== null && state.questionListEditor.list.id !== null && state.questionListEditor.isValid
);

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
