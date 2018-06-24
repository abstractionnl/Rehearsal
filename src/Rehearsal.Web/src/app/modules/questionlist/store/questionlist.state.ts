/// <reference path="../../../types.ts" />

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;
import * as Joi from "joi-browser";
import {IValidationResult} from "../../../validation";
import {createFeatureSelector, createSelector} from "@ngrx/store";

export interface QuestionlistEditorState {
    questionListOverview: QuestionListOverviewModel[];
    isPristine: boolean,
    list: QuestionListModel
}

export interface QuestionlistState {
    questionListEditor: QuestionlistEditorState;
}

export const initialState: QuestionlistEditorState = {
    questionListOverview: [],
    list: null,
    isPristine: false
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

export const selectSanitizedQuestionList = createSelector(
    selectSelectedQuestionList,
    (list: QuestionListModel) => list !== null ? sanitizeQuestionList(list) : null
);

export const selectHasQuestionList = createSelector(
    selectSelectedQuestionList,
    (list: QuestionListModel) => list !== null
);

export const selectHasNewQuestionList = createSelector(
    selectSelectedQuestionList,
    (list: QuestionListModel) => list !== null && list.id === null
);

export const selectIsValid = createSelector(
    selectSanitizedQuestionList,
    (validationResult: IValidationResult<QuestionListModel>) => validationResult !== null && validationResult.error === null
);

export const selectIsPristine = createSelector(
    selectHasQuestionList,
    selectFeature,
    // Hacky? No questionlist is also pristine
    (hasQuestionList: boolean, state: QuestionlistState) => !hasQuestionList || state.questionListEditor.isPristine
);

export const selectCanSave = createSelector(
    selectIsPristine,
    selectIsValid,
    (pristine: boolean, valid: boolean) => !pristine && valid
);

export const selectCanDelete = createSelector(
    selectHasQuestionList,
    selectHasNewQuestionList,
    (hasQuestionList: boolean, hasNewQuestionList: boolean) => hasQuestionList && !hasNewQuestionList
);

export const selectCanSwap = createSelector(
    selectHasQuestionList,
    (hasQuestionList: boolean) => hasQuestionList
);

export const selectCanCopy = createSelector(
    selectHasQuestionList,
    selectHasNewQuestionList,
    selectIsValid,
    (hasQuestionList: boolean, hasNewQuestionList: boolean, isValid: boolean) => hasQuestionList && !hasNewQuestionList && isValid
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

function sanitizeQuestionList(list: QuestionListModel): IValidationResult<QuestionListModel> {
    console.log('sanitize');
    return Joi.validate(stripEmptyQuestions(list), validationSchema);
}
