import {Component, EventEmitter, Input, Output} from "@angular/core";
import {BsModalRef} from "ngx-bootstrap";

export interface ResultAction {
    action: string;
    label: string;
    btnType: string;
}

export interface ModalResult {
    action: string;
    newTitle: string;
}

@Component({
    template: `
        <div class="modal-header">
            <h4 class="modal-title pull-left">{{title}}</h4>
            <button type="button" class="close pull-right" aria-label="Close" (click)="cancel()" *ngIf="canCancel()">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
        <div class="modal-body">
            <div class="form-group">
                <label for="newName" class="control-label">Titel voor kopie:</label>
                <input type="text" class="form-control" name="newTitle" [(ngModel)]="newTitle" />
            </div>
        </div>
        <div class="modal-footer">
            <ng-container *ngFor="let action of actions; let i = index">
                <button type="button" class="btn btn-{{action.btnType}}" (click)="select(action)">{{action.label}}</button>
            </ng-container>
        </div>
    `
})
export class ConfirmCopyQuestionlistComponent {
    title: string = "Woordenlijst kopiëren";
    @Input() newTitle: string;
    actions: ResultAction[];
    @Output() selected = new EventEmitter<ModalResult>();

    constructor(public bsModalRef: BsModalRef) {
        this.actions = [ {
            action: 'cancel',
            label: 'Annuleren',
            btnType: 'default',
        },{
            action: 'confirm',
            label: 'Opslaan',
            btnType: 'primary',
        }];
    }

    select(action: ResultAction) {
        this.selected.emit({
            action: action.action,
            newTitle: this.newTitle
        });
        this.bsModalRef.hide();
    }

    cancel(): void {
        this.select(this.actions.filter(x => x.action === 'cancel')[0]);
    }

    canCancel(): boolean {
        return this.actions.filter(x => x.action === 'cancel').length >= 1;
    }
}
