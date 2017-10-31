import * as QuestionListActions from "./questionlist.actions";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;

export function  questionListOverviewReducer(state: QuestionListOverviewModel[] = [], action: QuestionListActions.All) {
    switch (action.type) {
        case QuestionListActions.LOAD_OVERVIEW_SUCCESS:
            return action.payload;

        default:
            return state;
    }
}

export function questionListReducer(state: QuestionListModel = null, action: QuestionListActions.All): QuestionListModel {
    switch (action.type) {
        case QuestionListActions.NEW_LIST:
            return {
                id: null,
                title: 'Nieuwe lijst',
                questionTitle: '',
                answerTitle: '',
                questions: [ {
                    question: '',
                    answer: ''
                }],
                version: 0
            };

        case QuestionListActions.LOAD_LIST_SUCCESS:
            return action.payload;

        case QuestionListActions.SAVE_LIST_SUCCESS:
            return action.payload;

        case QuestionListActions.REMOVE_LIST_SUCCESS:
                return state.id == action.payload.id ? null : state;

        default:
            return state;
    }
}
