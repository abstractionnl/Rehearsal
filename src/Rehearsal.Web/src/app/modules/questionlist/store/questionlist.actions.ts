/// <reference path="../../../types.ts" />

import {Action} from "@ngrx/store";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;

export const LOAD_OVERVIEW = '[QuestionList] Load overview';
export const LOAD_OVERVIEW_SUCCESS = '[QuestionList] Load overview success';
export const LOAD_OVERVIEW_FAILED = '[QuestionList] Load overview failed';
export const NEW_LIST = '[QuestionList] New list';
export const LOAD_LIST = '[QuestionList] Load List';
export const LOAD_LIST_SUCCESS = '[QuestionList] Load list success';
export const LOAD_LIST_FAILED = '[QuestionList] Load list failed';
export const LIST_EDITED = '[QuestionList] List Edited';
export const SAVE_LIST = '[QuestionList] Save list';
export const SAVE_LIST_SUCCESS = '[QuestionList] Save list success';
export const SAVE_LIST_FAILED = '[QuestionList] Save list failed';
export const REMOVE_LIST = '[QuestionList] Remove list';
export const REMOVE_LIST_SUCCESS = '[QuestionList] Remove list success';
export const REMOVE_LIST_FAILED = '[QuestionList] Remove list failed';
export const SWAP_LIST = '[QuestionList] Swap list';
export const COPY_LIST = '[QuestionList] Copy list';

export class LoadQuestionListOverview implements Action {
    readonly type = LOAD_OVERVIEW;
    constructor() { }
}

export class LoadQuestionListOverviewSuccess implements Action {
    readonly type = LOAD_OVERVIEW_SUCCESS;
    constructor(public payload: QuestionListOverviewModel[]) { }
}

export class LoadQuestionListOverviewFailed implements Action {
    readonly type = LOAD_OVERVIEW_FAILED;
    constructor(public payload: any) { }
}

export class NewQuestionList implements Action {
    readonly type = NEW_LIST;
    constructor() { }
}

export class LoadQuestionList implements Action {
    readonly type = LOAD_LIST;
    constructor(public payload: { id: System.Guid }) { }
}

export class LoadQuestionListSuccess implements Action {
    readonly type = LOAD_LIST_SUCCESS;
    constructor(public payload: QuestionListModel) { }
}

export class LoadQuestionListFailed implements Action {
    readonly type = LOAD_LIST_FAILED;
    constructor(public payload: any) { }
}

export class QuestionListEdited implements Action {
    readonly type = LIST_EDITED;
    constructor(public payload: QuestionListModel) { }
}

export class SaveQuestionList implements Action {
    readonly type = SAVE_LIST;
    constructor() { }
}

export class SaveQuestionListSuccess implements Action {
    readonly type = SAVE_LIST_SUCCESS;
    constructor(public payload: QuestionListModel) { }
}

export class SaveQuestionListFailed implements Action {
    readonly type = SAVE_LIST_FAILED;
    constructor(public payload: any) { }
}

export class RemoveQuestionList implements Action {
    readonly type = REMOVE_LIST;
    constructor() { }
}

export class RemoveQuestionListSuccess implements Action {
    readonly type = REMOVE_LIST_SUCCESS;
    constructor(public payload: any) { }
}

export class RemoveQuestionListFailed implements Action {
    readonly type = REMOVE_LIST_FAILED;
    constructor(public payload: any) { }
}

export class SwapQuestionList implements Action {
    readonly type = SWAP_LIST;
    constructor() { }
}

export class CopyQuestionList implements Action {
    readonly type = COPY_LIST;
    constructor(public payload: { newTitle: string }) { }
}

export type All =
    LoadQuestionListOverview | LoadQuestionListOverviewSuccess | LoadQuestionListOverviewFailed |
    NewQuestionList | QuestionListEdited |
    LoadQuestionList | LoadQuestionListSuccess | LoadQuestionListFailed |
    SaveQuestionList | SaveQuestionListSuccess | SaveQuestionListFailed |
    RemoveQuestionList | RemoveQuestionListSuccess | RemoveQuestionListFailed |
    SwapQuestionList | CopyQuestionList;
