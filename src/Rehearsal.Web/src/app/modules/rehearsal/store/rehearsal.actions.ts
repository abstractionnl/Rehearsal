import {Action} from "@ngrx/store";

import {Rehearsal, System} from "../../../types";
import RehearsalSessionModel = Rehearsal.RehearsalSessionModel;
import AnswerResultModel = Rehearsal.AnswerResultModel;
import RehearsalQuestionType = Rehearsal.RehearsalQuestionType;

export const CREATE_REHEARSAL = '[Rehearsal] Create rehearsal';
export const CREATE_REHEARSAL_SUCCESS = '[Rehearsal] Create rehearsal success';
export const CREATE_REHEARSAL_FAILED = '[Rehearsal] Create rehearsal failed';
export const LOAD_REHEARSAL = '[Rehearsal] Load rehearsal';
export const LOAD_REHEARSAL_SUCCESS = '[Rehearsal] Load rehearsal success';
export const LOAD_REHEARSAL_FAILED = '[Rehearsal] Load rehearsal failed';
export const GIVE_ANSWER = '[Rehearsal] Give answer';
export const GIVE_ANSWER_SUCCESS = '[Rehearsal] Give answer success';
export const GIVE_ANSWER_FAILED = '[Rehearsal] Give answer failed';
export const NEXT_QUESTION = '[Rehearsal] Next question';
export const PREVIOUS_QUESTION = '[Rehearsal] Previous question';
export const REPEAT = '[Rehearsal] Repeat';
export const REPEAT_SUCCESS = '[Rehearsal] Repeat success';
export const REPEAT_FAILED = '[Rehearsal] Repeat failed';

export class CreateRehearsal implements Action {
    readonly type = CREATE_REHEARSAL;
    constructor(public payload: { questionListId: System.Guid, questionType: RehearsalQuestionType }) { }
}

export class CreateRehearsalSuccess implements Action {
    readonly type = CREATE_REHEARSAL_SUCCESS;
    constructor(public payload: { rehearsalId: System.Guid }) { }
}

export class CreateRehearsalFailed implements Action {
    readonly type = CREATE_REHEARSAL_FAILED;
    constructor(public payload: any) { }
}

export class LoadRehearsal implements Action {
    readonly type = LOAD_REHEARSAL;
    constructor(public payload: { id: System.Guid }) { }
}

export class LoadRehearsalSuccess implements Action {
    readonly type = LOAD_REHEARSAL_SUCCESS;
    constructor(public payload: RehearsalSessionModel) { }
}

export class LoadRehearsalFailed implements Action {
    readonly type = LOAD_REHEARSAL_FAILED;
    constructor(public payload: any) { }
}

export class GiveAnswer implements Action {
    readonly type = GIVE_ANSWER;
    constructor(public payload: string) { }
}

export class GiveAnswerSuccess implements Action {
    readonly type = GIVE_ANSWER_SUCCESS;
    constructor(public payload: AnswerResultModel) { }
}

export class GiveAnswerFailed implements Action {
    readonly type = GIVE_ANSWER_FAILED;
    constructor(public payload: any) { }
}

export class NextQuestion implements Action {
    readonly type = NEXT_QUESTION;
    constructor() {}
}

export class PreviousQuestion implements Action {
    readonly type = PREVIOUS_QUESTION;
    constructor() {}
}

export class Repeat implements Action {
    readonly type = REPEAT;
}

export class RepeatRehearsalSuccess implements Action {
    readonly type = REPEAT_SUCCESS;
    constructor(public payload: { rehearsalId: System.Guid }) { }
}

export class RepeatRehearsalFailed implements Action {
    readonly type = REPEAT_FAILED;
    constructor(public payload: any) { }
}

export type All =
    CreateRehearsal | CreateRehearsalSuccess | CreateRehearsalFailed |
    LoadRehearsal | LoadRehearsalSuccess | LoadRehearsalFailed |
    GiveAnswer | GiveAnswerSuccess | GiveAnswerFailed | NextQuestion | PreviousQuestion |
    Repeat | RepeatRehearsalSuccess | RepeatRehearsalFailed;
