import {
    Directive,
    ElementRef,
    OnInit,
    Renderer2,
    OnDestroy,
    Input
} from "@angular/core";

import { Subject ,  Subscription } from "rxjs";
import {distinctUntilChanged} from "rxjs/operators";
import {FormControlState} from "ngrx-forms/src/state";

@Directive({
    selector: '[formValidationStyle]'
})
export class FormValidationStyleDirective implements OnInit, OnDestroy {
    @Input("formValidationStyle") public set setState(state: FormControlState<any>) {
        this.errorSubject.next(!state.isValid);
    }

    static VALID_STYLE: string = 'has-success';
    static ERROR_STYLE: string = 'has-error';

    private errorSubject: Subject<boolean> = new Subject<boolean>();
    private errorSubjectSubscription: Subscription;

    constructor(private element: ElementRef, private renderer: Renderer2)
    {
    }

    ngOnInit(): void {
        this.errorSubjectSubscription = this.errorSubject
            .pipe(
                distinctUntilChanged()
            )
            .subscribe((hasError) => {
                if (hasError) {
                    this.renderer.addClass(this.element.nativeElement, FormValidationStyleDirective.ERROR_STYLE);
                } else {
                    this.renderer.removeClass(this.element.nativeElement, FormValidationStyleDirective.ERROR_STYLE);
                }
            });
    }

    ngOnDestroy(): void {
        this.errorSubjectSubscription.unsubscribe();
    }
}
