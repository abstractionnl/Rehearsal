import * as RehearsalActions from "./rehearsal.actions";
import {initialState, RehearsalState} from "./rehearsal.state";
import {GIVE_ANSWER_SUCCESS, LOAD_REHEARSAL_SUCCESS, NEXT_QUESTION, PREVIOUS_QUESTION} from "./rehearsal.actions";

export function rehearsalReducer(state: RehearsalState = initialState, action: RehearsalActions.All): RehearsalState {
    switch (action.type) {
        case LOAD_REHEARSAL_SUCCESS:
            let currentQuestion = action.payload.questions.findIndex(q => q.givenAnswer === null);
            currentQuestion = currentQuestion != -1 ? currentQuestion : action.payload.questions.length;

            return {
                ...state,
                session: {
                    id: action.payload.id,
                    questions: action.payload.questions,
                    currentQuestion: currentQuestion
                }
            };

        case GIVE_ANSWER_SUCCESS:
            return {
                ...state,
                session: {
                    ...state.session,
                    questions: state.session.questions.map(q => q.id == action.payload.questionId ? { ...q, givenAnswer: action.payload.givenAnswer, answeredCorrectly: action.payload.isCorrect } : q)
                }
            };

        case NEXT_QUESTION:
            return {
                ...state,
                session: {
                    ...state.session,
                    currentQuestion: state.session.currentQuestion + 1
                }
            };

        case PREVIOUS_QUESTION:
            if (state.session.currentQuestion > 0)
                return {
                    ...state,
                    session: {
                        ...state.session,
                        currentQuestion: state.session.currentQuestion - 1
                    }
                };
            return state;

        default: return state;
    }
}
