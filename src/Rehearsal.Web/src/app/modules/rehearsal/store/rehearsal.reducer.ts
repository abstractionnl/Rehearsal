import * as RehearsalActions from "./rehearsal.actions";
import {initialState, RehearsalSessionStateQuestion, RehearsalState} from "./rehearsal.state";
import {GIVE_ANSWER_SUCCESS, LOAD_REHEARSAL_SUCCESS, NEXT_QUESTION, PREVIOUS_QUESTION} from "./rehearsal.actions";

export function rehearsalReducer(state: RehearsalState = initialState, action: RehearsalActions.All): RehearsalState {
    switch (action.type) {
        case LOAD_REHEARSAL_SUCCESS:
            return {
                ...state,
                session: {
                    id: action.payload.id,
                    questions: action.payload.questions.map<RehearsalSessionStateQuestion>(
                        q => ({
                            question: q,
                            result: null
                        })
                    ),
                    currentQuestion: 0
                }
            };

        case GIVE_ANSWER_SUCCESS:
            return {
                ...state,
                session: {
                    ...state.session,
                    questions: state.session.questions.map(q => q.question.id == action.payload.questionId ? { ...q, result: action.payload } : q)
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
