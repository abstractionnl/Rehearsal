/// <reference path="../../../types.ts" />

import {Action} from "@ngrx/store";

export const CREATE_REHEARSAL = '[Rehearsal] Create rehearsal';
export const CREATE_REHEARSAL_SUCCESS = '[Rehearsal] Create rehearsal success';

export class CreateRehearsal implements Action {
    readonly type = CREATE_REHEARSAL;
    constructor(public payload: { questionListId: System.Guid }) { }
}

export class CreateRehearsalSuccess implements Action {
    readonly type = CREATE_REHEARSAL_SUCCESS;
    constructor(public payload: { rehearsalId: System.Guid }) { }
}
