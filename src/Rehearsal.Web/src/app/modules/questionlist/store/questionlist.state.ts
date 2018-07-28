import {QuestionList} from "../../../types";
import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;

import * as Joi from "joi-browser";
import {IValidationResult} from "../../../validation";

import {createFeatureSelector, createSelector} from "@ngrx/store";
import {FormGroupState} from "ngrx-forms";

export interface QuestionlistEditorState {
    questionListOverview: QuestionListOverviewModel[];
    formState: FormGroupState<QuestionListModel>,
}

export interface QuestionlistState {
    questionListEditor: QuestionlistEditorState;
}

export const initialState: QuestionlistEditorState = {
    questionListOverview: [],
    formState: null,
};

export const selectFeature = createFeatureSelector<QuestionlistState>('questionlist');

export const selectQuestionListOverview = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.questionListOverview
);

export const selectFormState = createSelector(
    selectFeature,
    (state: QuestionlistState) => state.questionListEditor.formState
);

export const selectSelectedQuestionList = createSelector(
    selectFormState,
    (formState: FormGroupState<QuestionListModel>) => formState && formState.value
);

export const selectHasQuestionList = createSelector(
    selectSelectedQuestionList,
    (list: QuestionListModel) => list !== null
);

export const selectSanitizedQuestionList = createSelector(
    selectHasQuestionList,
    selectSelectedQuestionList,
    (hasQuestionList: boolean, list: QuestionListModel) => hasQuestionList ? sanitizeQuestionList(list) : null
);

export const selectHasNewQuestionList = createSelector(
    selectHasQuestionList,
    selectSelectedQuestionList,
    (hasQuestionList: boolean, list: QuestionListModel) => hasQuestionList && list.id === null
);

export const selectIsValid = createSelector(
    selectFormState,
    (formState: FormGroupState<QuestionListModel>) => formState && formState.isValid
);

export const selectIsPristine = createSelector(
    selectHasQuestionList,
    selectFormState,
    // Hacky? No questionlist is also pristine
    (hasQuestionList: boolean, formState) => !hasQuestionList || formState.isPristine
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

function sanitizeQuestionList(list: QuestionListModel): QuestionListModel {
    console.log('sanitize');
    return stripEmptyQuestions(list);
}
