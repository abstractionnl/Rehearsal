import * as QuestionListActions from "./questionlist.actions";
import {
    initialState, QuestionlistEditorState, selectSelectedQuestionList
} from "./questionlist.state";

import {
    addArrayControl,
    createFormGroupState,
    FormArrayState,
    formGroupReducer, markAsDirty,
    removeArrayControl, setErrors, updateArray, updateArrayWithFilter, updateGroup,
    validate
} from "ngrx-forms";
import {required} from "ngrx-forms/validation";

import {QuestionList} from "../../../types";
import QuestionListModel = QuestionList.QuestionListModel;
import QuestionModel = QuestionList.QuestionModel;

const validateQuestionModel = updateGroup<QuestionModel>({
    question: validate(required),
    answer: validate(required)
});

const noValidateQuestionModel = updateGroup<QuestionModel>({
    question: setErrors({}),
    answer: setErrors({})
});

export const validateAndUpdateForm = updateGroup<QuestionListModel>({
    title: validate(required),
    questionTitle: validate(required),
    answerTitle: validate(required),
    questions: updateArray<QuestionModel>(state => {
        if (!state.value.answer && !state.value.question) {
            return noValidateQuestionModel(state);
        }

        return validateQuestionModel(state);
    })
});

export function questionListReducer(state: QuestionlistEditorState = initialState, action: QuestionListActions.All): QuestionlistEditorState {
    if (state.formState != null) {
        const formState = formGroupReducer(state.formState, action);
        if (formState !== state.formState) {
            state = { ...state, formState: validateAndUpdateForm(formState) };
        }
    }

    switch (action.type) {
        case QuestionListActions.LOAD_OVERVIEW_SUCCESS:
            return {
                ...state,
                questionListOverview: action.payload
            };

        case QuestionListActions.LOAD_OVERVIEW_FAILED:
            return {
                ...state,
                questionListOverview: []
            };

        case QuestionListActions.NEW_LIST:
            let newList = {
                id: null,
                title: 'Nieuwe lijst',
                questionTitle: '',
                answerTitle: '',
                questions: [{
                    question: '',
                    answer: ''
                }],
                version: 0
            };

            return {
                ...state,
                formState: createFormGroupState<QuestionListModel>("formid", newList)
            };

        case QuestionListActions.LOAD_LIST_SUCCESS:
            return {
                ...state,
                formState: createFormGroupState<QuestionListModel>("formid", action.payload)
            };

        case QuestionListActions.LOAD_LIST_FAILED:
            return {
                ...state,
                formState: null
            };

        case QuestionListActions.SAVE_LIST_SUCCESS:
            return {
                ...state,
                formState: createFormGroupState<QuestionListModel>("formid", action.payload)
            };

        case QuestionListActions.REMOVE_LIST_SUCCESS:
            return {
                ...state,
                formState: null
            };

        case QuestionListActions.SWAP_LIST:
            const list = state.formState.value;
            let swappedList = {
                ...list,
                questionTitle: list.answerTitle,
                answerTitle: list.questionTitle,
                questions: list.questions.map(q => ({
                    ...q,
                    question: q.answer,
                    answer: q.question
                }))
            };

            return {
                ...state,
                formState: markAsDirty(createFormGroupState<QuestionListModel>("formid", swappedList))
            };

        case QuestionListActions.COPY_LIST:
            const list2 = state.formState.value;
            let copiedList = {
                ...list2,
                id: null,
                title: action.payload.newTitle
            };

            return {
                ...state,
                formState: markAsDirty(createFormGroupState<QuestionListModel>("formid", copiedList))
            };

        case QuestionListActions.ADD_NEW_LINE:
            return {
                ...state,
                formState: {
                    ...state.formState,
                    controls: {
                        ...state.formState.controls,
                        questions: addArrayControl(<FormArrayState>(state.formState.controls.questions), {question: '', answer: ''})
                    }
                }
            };

        case QuestionListActions.REMOVE_LINE:
            return {
                ...state,
                formState: {
                    ...state.formState,
                    controls: {
                        ...state.formState.controls,
                        questions: removeArrayControl(<FormArrayState>(state.formState.controls.questions), action.index)
                    }
                }
            };

        default:
            return state;
    }
}
