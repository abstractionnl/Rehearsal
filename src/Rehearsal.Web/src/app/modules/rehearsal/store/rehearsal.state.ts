import {createFeatureSelector, createSelector} from "@ngrx/store";

import {Rehearsal, System} from "../../../types";
import AnswerResultModel = Rehearsal.AnswerResultModel;

export interface RehearsalState {
    session: RehearsalSessionState;
}

export interface RehearsalSessionState {
    id: System.Guid;
    questions: RehearsalSessionStateQuestion[];
    currentQuestion: number;
}

export interface RehearsalSessionStateQuestion {
    question: Rehearsal.RehearsalQuestionModel;
    result: AnswerResultModel;
}

export const initialState: RehearsalState = {
    session: null
};

export const selectFeature = createFeatureSelector<RehearsalState>('rehearsal');

export const selectCurrentSession = createSelector(
    selectFeature,
    (state: RehearsalState) => state.session
);

export const selectIsFinished = createSelector(
    selectCurrentSession,
    session => session && session.currentQuestion >= session.questions.length
);

export const selectCurrentQuestion = createSelector(
    selectCurrentSession,
    selectIsFinished,
    (session, isFinished) => (session && !isFinished) ? session.questions[session.currentQuestion].question : undefined
);

export const selectCurrentResult = createSelector(
    selectCurrentSession,
    selectIsFinished,
    (session, isFinished) => (session && !isFinished) ? session.questions[session.currentQuestion].result : undefined
);

export const selectQuestionCount = createSelector(
    selectCurrentSession,
    session => session.questions.length
);

export const selectAnsweredQuestionCount = createSelector(
    selectCurrentSession,
    session => session.questions.filter(q => q.result).length
);

export const selectIncorrectAnsweredQuestions = createSelector(
    selectCurrentSession,
    session => session.questions.filter(q => q.result && !q.result.isCorrect)
);
