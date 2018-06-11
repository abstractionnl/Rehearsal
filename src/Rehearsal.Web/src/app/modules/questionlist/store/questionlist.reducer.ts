import * as QuestionListActions from "./questionlist.actions";
import {
    initialState, QuestionlistEditorState, sanitizeQuestionList
} from "./questionlist.state";

import _ from "lodash";

export function questionListReducer(state: QuestionlistEditorState = initialState, action: QuestionListActions.All): QuestionlistEditorState {
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
                isValid: false,
                isPristine: true,
                list: newList
            };

        case QuestionListActions.LOAD_LIST_SUCCESS:
            return {
                ...state,
                isValid: true,
                isPristine: true,
                list: action.payload
            };

        case QuestionListActions.LOAD_LIST_FAILED:
            return {
                ...state,
                isValid: false,
                isPristine: false,
                list: null
            };

        case QuestionListActions.SAVE_LIST_SUCCESS:
            return {
                ...state,
                isValid: true,
                isPristine: true,
                list: action.payload
            };

        case QuestionListActions.LIST_EDITED:
            return {
                ...state,
                isValid: sanitizeQuestionList(action.payload).error === null,
                isPristine: false,
                list: _.extend({}, state.list, action.payload)
            };

        case QuestionListActions.REMOVE_LIST_SUCCESS:
            return {
                ...state,
                isValid: false,
                isPristine: true,
                list: state.list.id == action.payload.id ? null : state.list
            };

        case QuestionListActions.SWAP_LIST:
            let swappedList = {
                ...state.list,
                questionTitle: state.list.answerTitle,
                answerTitle: state.list.questionTitle,
                questions: state.list.questions.map(q => ({
                    ...q,
                    question: q.answer,
                    answer: q.question
                }))
            };

            return {
                ...state,
                isValid: sanitizeQuestionList(swappedList).error === null,
                isPristine: false,
                list: swappedList
            };

        case QuestionListActions.COPY_LIST:
            let copiedList = {
                ...state.list,
                id: null,
                title: action.payload.newTitle
            };

            return {
                ...state,
                isValid: sanitizeQuestionList(swappedList).error === null,
                isPristine: false,
                list: copiedList
            };

        default:
            return state;
    }
}
