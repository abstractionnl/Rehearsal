import {Component, EventEmitter} from "@angular/core";
import {BsModalRef} from "ngx-bootstrap";

export interface ResultAction {
    action: string;
    label: string;
    btnType: string;
}

@Component({
    selector: 'modal-content',
    template: `
        <div class="modal-header">
            <h4 class="modal-title pull-left">{{title}}</h4>
            <button type="button" class="close pull-right" aria-label="Close" (click)="cancel()" *ngIf="canCancel()">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="modal-body">
            {{body}}
        </div>
        <div class="modal-footer">
            <ng-container *ngFor="let action of actions; let i = index">
                <button type="button" class="btn btn-{{action.btnType}}" (click)="select(action)">{{action.label}}</button>
            </ng-container>
        </div>
    `
})
export class ConfirmSaveQuestionListComponent {
    title: string = "Wil je de wijzigingen opslaan?";
    body: string = "Je hebt nog onopgeslagen wijzigingen, wil je deze nog opslaan?";
    actions: ResultAction[];
    selected = new EventEmitter<ResultAction>();

    constructor(public bsModalRef: BsModalRef) {
        this.actions = [ {
            action: 'cancel',
            label: 'Annuleren',
            btnType: 'default',
        },{
            action: 'continue',
            label: 'Niet opslaan',
            btnType: 'danger',
        },{
            action: 'save',
            label: 'Opslaan',
            btnType: 'primary',
        }];
    }

    select(action: ResultAction) {
        this.selected.emit(action);
        this.bsModalRef.hide();
    }

    cancel(): void {
        this.select(this.actions.filter(x => x.action === 'cancel')[0]);
    }

    canCancel(): boolean {
        return this.actions.filter(x => x.action === 'cancel').length >= 1;
    }
}
