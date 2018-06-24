/// <reference path="../../../../types.ts" />

import {Component, EventEmitter, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {Store} from "@ngrx/store";
import {Observable} from "rxjs";
import {first, switchMap} from "rxjs/operators";

import {BsModalService} from "ngx-bootstrap";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import Guid = System.Guid;
import QuestionListModel = QuestionList.QuestionListModel;

import {
    QuestionlistState,
    selectCanCopy,
    selectCanDelete,
    selectCanSave,
    selectCanSwap,
    selectQuestionListOverview,
    selectSelectedQuestionList
} from "../../store/questionlist.state";
import {
    CopyQuestionList, QuestionListEdited, RemoveQuestionList, SaveQuestionList,
    SwapQuestionList
} from "../../store/questionlist.actions";

import {ConfirmCopyQuestionlistComponent, ModalResult} from "../../components/confirm-copy-questionlist/confirm-copy-questionlist.component";
import {ConfirmRemoveQuestionlistComponent} from "../../components/confirm-remove-questionlist/confirm-remove-questionlist.component";

@Component({
    templateUrl: 'questionlist-editor.page.html'
})
export class QuestionlistEditorPage implements OnInit {

    public questionLists$: Observable<QuestionListOverviewModel[]>;
    public selectedList$: Observable<QuestionListModel>;

    public canSave$: Observable<boolean>;
    public canDelete$: Observable<boolean>;
    public canSwap$: Observable<boolean>;
    public canCopy$: Observable<boolean>;

    constructor(
        private store: Store<QuestionlistState>,
        private router: Router,
        private modalService: BsModalService)
    {
        this.questionLists$ = this.store.select(selectQuestionListOverview);
        this.selectedList$ = this.store.select(selectSelectedQuestionList);
        this.canSave$ = this.store.select(selectCanSave);
        this.canDelete$ = this.store.select(selectCanDelete);
        this.canSwap$ = this.store.select(selectCanSwap);
        this.canCopy$ = this.store.select(selectCanCopy);
    }

    ngOnInit(): void {
    }

    selectList(id: Guid) {
        this.router.navigate(['/questionlists', id])
    }

    new() {
        this.router.navigate(['/questionlists', 'new']);
    }

    save() {
        this.store.dispatch(new SaveQuestionList());
    }

    changed(questionList: QuestionListModel) {
        this.store.dispatch(new QuestionListEdited(questionList));
    }

    delete() {
        let ref = this.modalService.show(ConfirmRemoveQuestionlistComponent);

        (<EventEmitter<ModalResult>>(ref.content.selected))
            .pipe(
                first()
            )
            .subscribe(result => {
                if (result.action == 'remove') {
                    this.store.dispatch(new RemoveQuestionList());
                }
            });
    }

    swap() {
        this.store.dispatch(new SwapQuestionList())
    }

    copy() {
        this.selectedList$.pipe(
            first(),
            switchMap(list => {
                let ref = this.modalService.show(ConfirmCopyQuestionlistComponent);
                ref.content.newTitle = 'Kopie ' + list.title;

                return (<EventEmitter<ModalResult>>(ref.content.selected));
            }),
            first()
        )
        .subscribe((modalResult: ModalResult) => {
            if (modalResult.action == 'confirm') {
                this.store.dispatch(new CopyQuestionList({newTitle: modalResult.newTitle}));
                this.store.dispatch(new SaveQuestionList())
            }
        });
    }
}
