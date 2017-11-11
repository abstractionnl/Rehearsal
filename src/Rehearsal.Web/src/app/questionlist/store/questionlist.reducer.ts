import * as QuestionListActions from "./questionlist.actions";
import {initialState, QuestionlistEditorState, validateQuestionList} from "./questionlist.state";

import _ from "lodash";

export function questionListReducer(state: QuestionlistEditorState = initialState, action: QuestionListActions.All): QuestionlistEditorState {
    switch (action.type) {
        case QuestionListActions.LOAD_OVERVIEW_SUCCESS:
            return {
                ...state,
                questionListOverview: action.payload
            };

        case QuestionListActions.NEW_LIST:
            return {
                ...state,
                isValid: false,
                isPristine: true,
                list: {
                    id: null,
                    title: 'Nieuwe lijst',
                    questionTitle: '',
                    answerTitle: '',
                    questions: [{
                        question: '',
                        answer: ''
                    }],
                    version: 0
                }
            };

        case QuestionListActions.LOAD_LIST_SUCCESS:
            return {
                ...state,
                isValid: true,
                isPristine: true,
                list: action.payload
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
                isValid: validateQuestionList(action.payload),
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

        default:
            return state;
    }
}
