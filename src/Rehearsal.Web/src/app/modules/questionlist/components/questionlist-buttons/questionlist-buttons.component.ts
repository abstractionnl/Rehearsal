import {ChangeDetectionStrategy, Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
    selector: 'questionlist-buttons',
    templateUrl: './questionlist-buttons.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class QuestionlistButtonsComponent {
    @Output() public onSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() public onDelete: EventEmitter<any> = new EventEmitter<any>();
    @Output() public onSwap: EventEmitter<any> = new EventEmitter<any>();
    @Output() public onCopy: EventEmitter<any> = new EventEmitter<any>();

    @Input() public canSave: boolean;
    @Input() public canDelete: boolean;
    @Input() public canSwap: boolean;
    @Input() public canCopy: boolean;

    save($event) {
        if (this.canSave) {
            this.onSave.emit();
        }
        $event.preventDefault();
    }

    delete($event) {
        if (this.canDelete) {
            this.onDelete.emit();
        }
        $event.preventDefault();
    }

    swap($event) {
        if (this.canSwap) {
            this.onSwap.emit();
        }
        $event.preventDefault();
    }

    copy($event) {
        if (this.canCopy) {
            this.onCopy.emit();
        }
        $event.preventDefault();
    }
}
